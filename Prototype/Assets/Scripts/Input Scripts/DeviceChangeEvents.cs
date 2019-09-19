using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceChangeEvents : MonoBehaviour
{
    public delegate void OrientationEvent();
    public delegate void DeviceStateEvent();

    public static event OrientationEvent OnOrientationPortrait;
    public static event OrientationEvent OnOrientationLandscape;

    public static event DeviceStateEvent OnDeviceSleep;
    public static event DeviceStateEvent OnDevicePaused;
    //public static event DeviceStateEvent OnResolutionChanged;

    public static event Action<Vector2> OnResolutionChanged;


    public static float CheckDelay = 0.5f;
    static Vector2 resolution;                    // Current Resolution
    static DeviceOrientation orientation;        // Current Device Orientation
    static bool isAlive = true;                    // Keep this script running?


    void Start()
    {
        StartCoroutine(CheckForChange());
    }

    IEnumerator CheckForChange()
    {
        resolution = new Vector2(Screen.width, Screen.height);
        orientation = Input.deviceOrientation;

        while (isAlive)
        {

            // Check for a Resolution Change
            if (resolution.x != Screen.width || resolution.y != Screen.height)
            {
                resolution = new Vector2(Screen.width, Screen.height);
                if (OnResolutionChanged != null) OnResolutionChanged(resolution);
            }

            // Check for an Orientation Change
            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Unknown:            // Ignore
                case DeviceOrientation.FaceUp:            // Ignore
                case DeviceOrientation.FaceDown:        // Ignore
                    break;
                case DeviceOrientation.Portrait:
                    OnOrientationPortrait?.Invoke();
                    break;
                case DeviceOrientation.LandscapeLeft:
                    OnOrientationLandscape?.Invoke();
                    break;
                case DeviceOrientation.LandscapeRight:
                    OnOrientationLandscape?.Invoke();
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(CheckDelay);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            OnDevicePaused?.Invoke();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            OnDeviceSleep?.Invoke();
        }
    }

    void OnDestroy()
    {
        isAlive = false;
    }
}
