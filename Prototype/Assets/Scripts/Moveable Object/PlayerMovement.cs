using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;

    public float jumpSpeed = 2.0f;
    public float chargeSpeed = 2.0f;
    public float slideSpeed = 2.0f;

    public BoxCollider2D chargeTrigger;
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
        TouchEvents.OnSwipeDown += Slide;
    }

    private void OnDisable()
    {
        TouchEvents.OnSwipeUp -= Jump;
        TouchEvents.OnSingleTouch -= SpecialAction;
        TouchEvents.OnSwipeDown -= Slide;
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
        chargeTrigger.enabled = true;
        //_rb.AddForce(new Vector3(2.0f, 0.0f, 0.0f) * chargeSpeed, ForceMode2D.Impulse);
    }

    private void Slide()
    {
        _anim.SetTrigger("Slide");
        //_rb.AddForce(new Vector3(2.0f, 0.0f, 0.0f) * slideSpeed, ForceMode2D.Impulse);
    }
}
