using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapRedirection : MonoBehaviour
{
    public Button mapButton;
    public string url;
    void Start()
    {
        mapButton=GetComponentInChildren<Button>();
        mapButton.onClick.AddListener(OpenMap);
    }
    public void OpenMap()
    {

        //  Application.OpenURL("http://maps.google.com/maps?q=VPS+Lakeshore+Hospital");
       // Application.OpenURL("https://www.google.co.in/maps/place/VPS+Lakeshore+Hospital/@9.9163082,76.3155479,16z/data=!4m5!3m4!1s0x3b0873b2953388a7:0xce77188e8376bcfa!8m2!3d9.9190543!4d76.3189326");
        Application.OpenURL(url);
    }

}
