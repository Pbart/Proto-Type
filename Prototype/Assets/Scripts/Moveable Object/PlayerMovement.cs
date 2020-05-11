using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;

    private float gravity;
    private float jumpVelocity;

    public float chargeSpeed = 2.0f;
    public float slideSpeed = 2.0f;
    public float movementSpeed = 0.03f;
    public float jumpSpeed = 20.0f;

    public float jumpHeight;
    public float timeToReachJumpApex;
    public Vector3 playerVelocity;

    public bool isAutoRunning;
    public bool testing;
    public bool isAirborne;

    //add a raycast to trigger isAirborne as well

    public BoxCollider2D chargeTrigger;
    // Start is called before the first frame update
    void Start()
    {
        _anim = this.GetComponent<Animator>();
        _rb = this.GetComponent<Rigidbody2D>();
        chargeTrigger.enabled = false;

        gravity = -(2 * jumpHeight) / (Mathf.Pow(timeToReachJumpApex, 2f));
        jumpVelocity = Mathf.Abs(gravity * timeToReachJumpApex);
    }

    private void Update()
    {
        Debug.Log(_rb.velocity.ToString());
        if (testing)
        {
            if (isAirborne == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                
            }
            if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
            {
                this.transform.position += new Vector3(movementSpeed, 0, 0);
            }
        }
        
    }

    private void FixedUpdate()
    {
        
        if (isAutoRunning)
        {
            this.transform.position += new Vector3(movementSpeed, 0, 0);
            //if (_rb != null)
            //{
            //    _rb.velocity += new Vector2(movementSpeed, 0);
            //}
        } 
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
        //Vector3 temp = this.transform.position;
        //temp.y += jumpVelocity;
        //this.transform.position = temp;
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

    public void TurnOffChargerTrigger()
    {
        chargeTrigger.enabled = false;
    }
}
