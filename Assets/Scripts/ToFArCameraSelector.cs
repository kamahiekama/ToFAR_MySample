using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TofAr.V0.Color;
using TofAr.V0.Tof;
using UnityEngine;

public class ToFArCameraSelector : MonoBehaviour
{
    public bool UseFrontCamera = false;

    public int DesiredWidth = 1280;

    public int DesiredHeight = 720;

    public int DesiredFramerate = 30;

    public bool UseDepth = false;

    TofArColorManager mgr;

    /// <summary>
    /// 実効フレームレート
    /// </summary>
    public float frameRate = 0;

    /// <summary>
    /// 実際に取得されたフレームの解像度（横幅）
    /// </summary>
    public int width;

    /// <summary>
    /// 実際に取得されたフレーム解像度（縦幅）
    /// </summary>
    public int height;

    void Awake(){
        mgr = GetComponent<TofArColorManager>();

        // disable auto start
        mgr.autoStart = false;

        TofArColorManager.OnFrameArrived += OnFrameArrived;
    }

    private void OnFrameArrived(object sender)
    {
        ResolutionProperty rp = mgr.GetProperty<ResolutionProperty>();
        frameRate = mgr.FrameRate;
        width = rp.width;
        height = rp.height;
    }

    // Start is called before the first frame update
    void Start()
    {
        StringBuilder sb = new StringBuilder();

        AvailableResolutionsProperty properties = mgr.GetProperty<AvailableResolutionsProperty>();

        ResolutionProperty selectedResolutionProperty = null;

        int minDiff = 10000;
        int selectedIndex = 0;
        for(int i = 0; i < properties.resolutions.Length; i++){
            ResolutionProperty rp = properties.resolutions[i];
            sb.Append("[" + i + "]: " + rp + "\r\n");

            if ((rp.lensFacing == 0) != UseFrontCamera){
                continue;
            }

#if UNITY_ANDROID
            rp.frameRate = DesiredFramerate;
#endif

            int diff = Mathf.Abs(rp.width - DesiredWidth) + Mathf.Abs(rp.height - DesiredHeight);
            if (diff < minDiff){
                selectedIndex = i;
                selectedResolutionProperty = rp;
                minDiff = diff;
            }
        }

        Debug.Log(sb.ToString());
        Debug.Log("selected: [" + selectedIndex + "] : " + selectedResolutionProperty);

        TofArTofManager tmgr = TofArTofManager.Instance;
        CameraConfigurationProperty selectedTofConfig = null;
        if (UseDepth && tmgr)
        {
            CameraConfigurationsProperty tofProperties = tmgr.GetProperty<CameraConfigurationsProperty>();

            // seach same aspect configuration
            float colorAspect = (float)selectedResolutionProperty.width / selectedResolutionProperty.height;
            foreach(CameraConfigurationProperty config in tofProperties.configurations)
            {
                float aspect = (float)config.width / config.height;
                if (Mathf.Abs(aspect - colorAspect) > 0.001f)
                {
                    continue;
                }
                selectedTofConfig = config;
            }
        }

        if (UseDepth && tmgr && selectedTofConfig != null)
        {
            tmgr.StartStreamWithColor(selectedTofConfig, selectedResolutionProperty, true, true);
        }
        else
        {
            mgr.StartStream(selectedResolutionProperty);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
