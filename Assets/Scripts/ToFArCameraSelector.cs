using System.Collections;
using System.Collections.Generic;
using System.Text;
using TofAr.V0.Color;
using UnityEngine;

public class ToFArCameraSelector : MonoBehaviour
{
    public int width = 1280;

    public int fps = 30;

    TofArColorManager mgr;

    void Awake(){
        mgr = GetComponent<TofArColorManager>();

        // disable auto start
        mgr.autoStart = false;
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

            // ignore 0 fps mode
            if (rp.frameRate == 0){
                continue;    
            }

            int diff = Mathf.Abs(rp.width - width);
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
