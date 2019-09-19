using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A testing class for touch input system and device orientation
public class TouchIdentifier : MonoBehaviour
{
    private bool hasTouch;

    // Start is called before the first frame update
    void Start()
    {
        //Adding fuctions to an event does not require the parenthesis'
        TouchEvents.OnSingleTouchHeld += PrintHeld;
        //TouchEvents.OnSingleTouch += MovetoTouchPosition;
        TouchEvents.OnSwipeLeft += TestSwipe;
        TouchEvents.OnTouchDrag += PrintDrag;

        TouchEvents.OnOrientationPortrait += PrintPortrait;
        TouchEvents.OnOrientationLandscape += PrintLandscape;
    }

    private void OnDisable()
    {
        TouchEvents.OnSingleTouch -= PrintTap;
    }

    void TestSwipe()
    {
        Debug.Log("Demoing");
    }

    void PrintTap(Touch t)
    {
        Debug.Log("TAP. Event received. The touch position is at " + t.position);
        hasTouch = true;
    }

    void PrintDoubleTap(Touch t)
    {
        Debug.Log("DOUBLE TAP. Event received. The touch position is at " + t.position);
    }

    void PrintHeld(Touch t)
    {
        Debug.Log("HELD. Event received. The touch position is at " + t.position);
    }

    void PrintDrag(Touch t)
    {
        Debug.Log("DRAG. Event received. The touch postion is at " + t.position);
    }

    void PrintPortrait()
    {
        Debug.Log("Device orientation is Portrait");
    }

    void PrintLandscape()
    {
        Debug.Log("Device orientation is Landscape");
    }

    void CastRayFromPoint(Touch t)
    {
        Debug.Log("Casting a ray from the touch position");
        if(hasTouch == true)
        Debug.DrawRay(Camera.main.transform.position, Vector3.forward * 30, Color.blue);

        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Vector3.forward, out hitInfo, 30))
        {
            Debug.Log("Drawing Ray");
            Debug.DrawRay(Camera.main.transform.position, Vector3.forward * 30, Color.blue);
            Debug.Log(hitInfo.collider.gameObject.name);
        }
    }

    void MovetoTouchPosition(Touch t)
    {
        Ray ray = Camera.main.ScreenPointToRay(t.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.Log("We hit something. The object we hit is a " + hitInfo.collider.name);
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(t.position);
            Vector3 hitPos = hitInfo.point;
            Debug.Log("The touch's world position is " + touchWorldPosition.ToString());
            GameObject obj = GameObject.FindGameObjectWithTag("Test Cube");
            //obj.transform.position = hitPos;
            //dfcccnjmwhile(obj.transform.position != hitPos)
            obj.transform.position = Vector3.Lerp(obj.transform.position, hitPos, 0.3f); 
        }
    }
}