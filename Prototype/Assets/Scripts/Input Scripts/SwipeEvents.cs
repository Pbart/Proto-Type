using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEvents : MonoBehaviour
{
    #region Public Variables
    public delegate void SwipeEvent();

    public static event SwipeEvent OnSwipeUp;
    public static event SwipeEvent OnSwipeDown;
    public static event SwipeEvent OnSwipeLeft;
    public static event SwipeEvent OnSwipeRight;

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

    private Touch playerTouch;

    private Vector3 initialTouchPosition;
    private Vector3 finalTouchPosition;

    private SwipeDirection direction;
    #endregion

    // Update is called once per frame
    void Update()
    {
        //check if there is a touch found
        if (Input.touchCount > 0)
        {
            playerTouch = Input.GetTouch(0);           

            if (playerTouch.phase == TouchPhase.Began)
            {
                initialTouchPosition = playerTouch.position;
            }
            else if (playerTouch.phase == TouchPhase.Ended || playerTouch.phase == TouchPhase.Canceled)
            {
                finalTouchPosition = playerTouch.position;               
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
}
