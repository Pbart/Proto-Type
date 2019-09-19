using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouchEvents : MonoBehaviour
{
    public delegate void MultiTouchEvent (Touch[] touches);

    public static event MultiTouchEvent OnPinchIn;
    public static event MultiTouchEvent OnPinchOut;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
