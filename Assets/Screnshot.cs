using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screnshot : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
