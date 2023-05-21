using System.Collections;
using System.Collections.Generic;
using System.Text;
using TofAr.V0.Color;
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

        string text = "Width: " + rp.width + "\r\nHeight: " + rp.height + "\r\nFramerate: " + mgr.FrameRate;
        GetComponent<Text>().text = text;
    }
}
