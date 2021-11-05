using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRScanner : MonoBehaviour
{
    WebCamTexture webcamTexture;
    string QrCode = string.Empty;
    [SerializeField] GameObject deliveryPointDetails;
    [SerializeField] GameObject InvoiceScreen;
    //  var renderer;
    Texture materialTex;
    void Start()
    {
        var renderer = GetComponent<RawImage>();
        webcamTexture = new WebCamTexture(512, 512);
        materialTex = renderer.material.mainTexture;
        renderer.material.mainTexture = webcamTexture;
        StartCoroutine(GetQRCode());
    }

    IEnumerator GetQRCode()
    {
        IBarcodeReader barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(webcamTexture.GetPixels32());
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QrCode = Result.Text;
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        Debug.Log("DECODED TEXT FROM QR: " + QrCode);
                        Debug.Log(QrCode.ToString());
                         if(QrCode.ToString()=="Edappally")
                        {
                            print("sdfd");
                            InvoiceGenerator();

                        }
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }
      //  var renderer = GetComponent<RawImage>();
      //  renderer.material.mainTexture = materialTex;
      //  webcamTexture.Stop();
       
    }

    void InvoiceGenerator()
    {
        WindowNavigation.navigation.NavigationSwapper(InvoiceScreen, deliveryPointDetails);
        WindowNavigation.navigation.RoutesButtonClicked(deliveryPointDetails, InvoiceScreen);
       

    }
     /*   private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        string text =QrCode;
        GUI.Label(rect, text, style);
    }
     */
}
