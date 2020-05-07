using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceManager : MonoBehaviour
{
    public Text deviceOutput;

    private string deviceType;
    private string deviceVersion;
    private string deviceScreenResolution;

    private float batteryLevel;
    private float deviceScreenWidth;
    private float deviceScreenHeight;  
    private float deviceAspectRatio;


    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            deviceType = "Android";
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                deviceVersion = version.GetStatic<int>("SDK_INT").ToString();
            }
            batteryLevel = SystemInfo.batteryLevel;
            deviceScreenHeight = Display.main.systemHeight;
            deviceScreenWidth = Display.main.systemWidth;
            deviceAspectRatio = deviceScreenWidth / deviceScreenHeight;
            deviceScreenResolution = Screen.currentResolution.ToString();
        }

        deviceOutput.text = "Device OS: " + deviceType + "\n" + "OS Version: " + deviceVersion + "\n" + "Device Resolution: " + deviceScreenResolution + "\n" + "Battery Level: " + batteryLevel.ToString()
            + "\n" + "Screen Width: " + deviceScreenWidth.ToString() + "\n" + "Screen Height" + deviceScreenHeight + "\n" + "Device Aspect Ratio: " + deviceAspectRatio + "\n";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
