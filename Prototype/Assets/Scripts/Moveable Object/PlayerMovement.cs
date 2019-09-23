using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;

    public float jumpSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        _anim = this.GetComponent<Animator>();
        _rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        TouchEvents.OnSwipeUp += Jump;
        TouchEvents.OnSingleTouch += SpecialAction;
    }

    // Update is called once per frame
    private void Jump()
    {
        _anim.SetTrigger("Jump");
        _rb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * jumpSpeed, ForceMode2D.Impulse);
    }

    private void SpecialAction(Touch t)
    {
        _anim.SetTrigger("Charge");
        _rb.AddForce(new Vector3(2.0f, 0.0f, 0.0f) * jumpSpeed, ForceMode2D.Impulse);
    }
}
