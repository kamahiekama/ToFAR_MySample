using System.Collections;
using System.Collections.Generic;
using System.Text;
using TofAr.V0.Color;
using TofAr.V0.Tof;
using UnityEngine;
using UnityEngine.UI;

public class GetCameraInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TofArColorManager mgr = TofArColorManager.Instance;
        ResolutionProperty rp = mgr.GetProperty<ResolutionProperty>();
        string text = "Color Width: " + rp.width + "\r\nColor Height: " + rp.height + "\r\nColor Framerate: " + mgr.FrameRate;

        TofArTofManager tmgr = TofArTofManager.Instance;
        if (tmgr)
        {
            CameraConfigurationProperty trp = tmgr.GetProperty<CameraConfigurationProperty>();
            text += "\r\nToF Width: " + trp.width + "\r\nToF Height: " + trp.height + "\r\nToF Framerate: " + tmgr.FrameRate;
        }

        GetComponent<Text>().text = text;
    }
}
