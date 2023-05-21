using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TofAr.V0.Color;
using UnityEngine;

public class ToFArCameraSelector : MonoBehaviour
{
    public bool UseFrontCamera = false;

    public int DesiredWidth = 1280;

    public int DesiredHeight = 720;

    public int DesiredFramerate = 30;

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

            rp.frameRate = DesiredFramerate;

            int diff = Mathf.Abs(rp.width - DesiredWidth) + Mathf.Abs(rp.height - DesiredHeight);
            if (diff < minDiff){
                selectedIndex = i;
                selectedResolutionProperty = rp;
                minDiff = diff;
            }
        }

        Debug.Log(sb.ToString());
        Debug.Log("selected: [" + selectedIndex + "] : " + selectedResolutionProperty);

        mgr.SetProperty<ResolutionProperty>(selectedResolutionProperty);

        mgr.StartStream(selectedResolutionProperty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
