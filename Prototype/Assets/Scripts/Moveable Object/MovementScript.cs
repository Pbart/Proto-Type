using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody rb;
    public float jumpSpeed;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        TouchEvents.OnSingleTouch += MoveToTouchPosition;
        TouchEvents.OnSingleTouchHeld += Jump;
        //TouchEvents.OnTouchDrag += Follow;
    }

    private void OnDisable()
    {
        TouchEvents.OnSingleTouch -= MoveToTouchPosition;
        TouchEvents.OnSingleTouchHeld -= Jump;
        //TouchEvents.OnTouchDrag -= Follow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Jump(Touch t)
    {
        rb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * jumpSpeed, ForceMode.Impulse);
    }

    private void MoveToTouchPosition(Touch t)
    {
        Ray ray = Camera.main.ScreenPointToRay(t.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.Log("We hit something. The object we hit is a " + hitInfo.collider.name);
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(t.position);
            Vector3 hitPos = hitInfo.point;

            StartCoroutine(Move(hitInfo.point));
        }
    }

    private void Follow(Touch t)
    {
        Ray ray = Camera.main.ScreenPointToRay(t.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.Log("We hit something. The object we hit is a " + hitInfo.collider.name);
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(t.position);
            Vector3 hitPos = hitInfo.point;
                   
            this.transform.position = Vector3.Lerp(this.transform.position, hitPos, 0.3f);
        }
    }

    IEnumerator Move(Vector3 pos)
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        while (this.transform.position != pos)
        {
            transform.position = Vector3.Lerp(transform.position, pos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        transform.position = pos;
        yield return null;
    }
}
