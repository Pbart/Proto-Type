using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvents : MonoBehaviour
{
    #region Public Variables
    public delegate void TouchEvent(Touch t);
    public delegate void SwipeEvent();
    public delegate void OrientationEvent();

    //Touch Based events
    public static event TouchEvent OnSingleTouch;
    public static event TouchEvent OnSingleTouchHeld;
    public static event TouchEvent OnSingleTouchReleased;
    public static event TouchEvent OnTouchDrag;
    public static event TouchEvent OnDoubleTap;

    //Swipe based events MAKE THIS A SEPARATE CLASS
    public static event SwipeEvent OnSwipeUp;
    public static event SwipeEvent OnSwipeDown;
    public static event SwipeEvent OnSwipeLeft;
    public static event SwipeEvent OnSwipeRight;

    //Device Orientation Events MAKE THIS A SEPARATE CLASS
    public static event OrientationEvent OnOrientationPortrait;
    public static event OrientationEvent OnOrientationLandscape;

    //Values to change on the fly
    public float minimumDistance = 100f;
    public float topLeftAngle = 135f;
    public float topRightAngle = 45f;
    public float bottomLeftAngle = -135f;
    public float bottomRightAngle = -45f;
    public float touchTime = 0.3f;
    #endregion

    #region Private Variables
    private float distanceBetweenTouchPositions;
    private float angleBetweenTouchPositions;

    private Touch initialPlayerTouch;

    private Vector3 initialTouchPosition;
    private Vector3 finalTouchPosition;

    private SwipeDirection direction;

    private float touchTimer;
    private float tapTimer;
    private float tapTime = 0.2f;

    #endregion

    private void Update()
    {
        //Check Device Orientation Maybe use DougMcFarlane Device Change class from https://forum.unity.com/threads/device-screen-rotation-event.118638/
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            //Run all methods subscribed to event
            OnOrientationPortrait?.Invoke();
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            OnOrientationLandscape?.Invoke();
        }

        //check if there is a touch found
        if (Input.touchCount > 0)
        {
            initialPlayerTouch = Input.GetTouch(0);
            //Test to see if this will cause any issues.
            //OnTouchDrag?.Invoke(initialPlayerTouch);
            touchTimer += Time.deltaTime;

            //check if we are touching a UI element and if we are dont do any input commands !!!MOVE INTO TOUCHPHASE.ENDED IF
            if (IsTouchOverUIElement(initialPlayerTouch) == false)
            {
                if (initialPlayerTouch.phase == TouchPhase.Began)
                {
                    initialTouchPosition = initialPlayerTouch.position;
                }
                else if (initialPlayerTouch.phase == TouchPhase.Moved)
                {
                    OnTouchDrag?.Invoke(initialPlayerTouch);
                }
                else if (initialPlayerTouch.phase == TouchPhase.Ended)
                {
                    finalTouchPosition = initialPlayerTouch.position;
                    touchTimer = touchTimer;

                    direction = DetectSwipeDirection(initialTouchPosition, finalTouchPosition);
                    if (direction == SwipeDirection.Left)
                    {
                        OnSwipeLeft?.Invoke();
                    }
                    else if (direction == SwipeDirection.Right)
                    {
                        OnSwipeRight?.Invoke();
                    }
                    else if (direction == SwipeDirection.Up)
                    {
                        OnSwipeUp?.Invoke();
                    }
                    else if (direction == SwipeDirection.Down)
                    {
                        OnSwipeDown?.Invoke();
                    }
                    else if (direction == SwipeDirection.None)
                    {
                        if (touchTimer < tapTime)
                        {
                            StartCoroutine("DoubleTap");
                        }
                        else
                        {
                            OnSingleTouchHeld?.Invoke(initialPlayerTouch);
                        }
                    }
                    touchTimer = 0f;
                }
            }
        }
    }

    private SwipeDirection DetectSwipeDirection(Vector3 initialPos, Vector3 finalPos)
    {
        if (CalculateDistanceBetweenPoint(initialPos, finalPos) > minimumDistance)
        {
            float angle = CalculateAngleBetweenPoints(initialPos, finalPos);
            //Debug.Log("Angle:" + angle);
            
            if (angle > topRightAngle && angle <= topLeftAngle)
            {
                return SwipeDirection.Up;
            }
            else if (angle > -45f && angle <= 45f)
            {
                return SwipeDirection.Right;
            }
            else if (angle > -135f && angle <= 45f) //This need to be fixed at somepoint
            {
                return SwipeDirection.Down;
            }
            else if (angle > topLeftAngle || angle <= bottomLeftAngle)
            {
                return SwipeDirection.Left;
            }
            else
            {
                Debug.LogWarning("Something weird happened");
                return SwipeDirection.None;
            }
        }
        else
        {
            return SwipeDirection.None;
        }
    }
    private float CalculateAngleBetweenPoints(Vector3 initialTouchPosition, Vector3 finalTouchPosition)
    {
        float angleBetweenPoints;
        float temp = CalculateDistanceBetweenPoint(initialTouchPosition, finalTouchPosition);
        //Debug.Log("Distance between Points " + temp);
        if (Mathf.Abs(finalTouchPosition.y - initialTouchPosition.y) > minimumDistance || Mathf.Abs(finalTouchPosition.x - initialTouchPosition.x) > minimumDistance)
        {
            angleBetweenPoints = Mathf.Atan2(finalTouchPosition.y - initialTouchPosition.y, finalTouchPosition.x - initialTouchPosition.x) * Mathf.Rad2Deg;
            //Debug.Log("Angle: " + angleBetweenPoints);
            return angleBetweenPoints;
        }
        else
        {
            return 0;
        }

    }
    private float CalculateDistanceBetweenPoint(Vector3 initialPoint, Vector3 finalPoint)
    {
        float distance = Mathf.Abs(Vector3.Distance(initialPoint, finalPoint));
        return distance;
    }

    private IEnumerator DoubleTap()
    {
        yield return new WaitForSeconds(0.2f);
        if (initialPlayerTouch.tapCount == 1)
        {
            Debug.Log("THIS IS A SINGLE TAP");
            OnSingleTouch?.Invoke(initialPlayerTouch);
        }
        else if (initialPlayerTouch.tapCount == 2)
        {
            OnDoubleTap?.Invoke(initialPlayerTouch);
            StopCoroutine("DoubleTap");
            Debug.Log("THIS IS A DOUBLE TAP");
        }
    }

    private bool IsTouchOverUIElement(Touch t)
    {
        PointerEventData pointerDataCurrentPosition = new PointerEventData(EventSystem.current);
        pointerDataCurrentPosition.position = new Vector2(t.position.x, t.position.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerDataCurrentPosition, results);
        return results.Count > 0;
    }
}
