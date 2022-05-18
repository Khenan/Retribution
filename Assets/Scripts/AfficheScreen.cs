using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfficheScreen : MonoBehaviour
{

    [SerializeField] private string path;

    [SerializeField] [Range(1, 5)] private int size = 1;

// Update is called once per frame
void Update()
{
    if (Input.GetKeyDown(KeyCode.K))
    {
        print("ok");
        path += "screenshot";
        print("ok2");
        path += System.Guid.NewGuid().ToString() + ".png";
        print("ok3");
        ScreenCapture.CaptureScreenshot(path, size);
    }
}
}

