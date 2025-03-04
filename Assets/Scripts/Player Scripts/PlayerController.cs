using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Vector2 velocity;
    [SerializeField] float speedModifier;

    [SerializeField] float gravity;
    [SerializeField] float jumpPower; //sure idk

    [SerializeField] bool grounded;
    [Space]
    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] float followDistance;
    [SerializeField] float XOffset;
    [SerializeField] float YOffset;

    private void LateralMovement()
    {
        transform.position = new Vector3(transform.position.x + (velocity.x * Time.deltaTime), transform.position.y, transform.position.z + (velocity.y * Time.deltaTime));
    }

    private void Gravity()
    {
        if (!grounded)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (-gravity * Time.deltaTime), transform.position.z);
            gravity += 9.8f * Time.deltaTime;
        }
        else
        {
            gravity = 2.45f;
        }
            
    }

    #region _Control_Methods_
    public void OnJump()
    {
        grounded = false;
        gravity = -jumpPower;
    }

    public void OnMove(InputValue value)
    {
        velocity = value.Get<Vector2>().normalized * speedModifier;
    }

    public void OnLook(InputValue value)
    {
        transform.Rotate(new Vector3(0, value.Get<Vector2>().x * Time.deltaTime * 10f, 0));
        //Debug.Log(value.Get<Vector2>());

        //camera value.Get<Vector2>().y * Time.deltaTime
        //cam.transform.RotateAround(transform.right, value.Get<Vector2>().y * Time.deltaTime * 10f);

        Debug.Log(cam.transform.rotation);

        //https://discussions.unity.com/t/please-explain-quaternions/96863/3
        //if we are past the good angles and the value is bad, dont let it happen
        //if (cam.transform.eulerAngles.x > 90 && value.Get<Vector2>().y > 0)
        //{
        //    //bad stinky
        //    Debug.Log("too far down");
        //}
        //else if (cam.transform.eulerAngles.x < 0 && value.Get<Vector2>().y < 0)
        //{
        //    Debug.Log("too far up");
        //}


        cam.transform.Rotate(value.Get<Vector2>().y * Time.deltaTime * -10f, 0, 0);

        //cam.transform.eulerAngles = new Vector3(90, 0, 0);

        //unit circle stuff
        //when angle is 0, z is maximum, when angle is 90, y is maximum
        //cos 0 = 1, sin 90 = 1;

        cam.transform.localPosition = new Vector3(XOffset, (Mathf.Sin(cam.transform.eulerAngles.x * Mathf.Deg2Rad) * followDistance) + YOffset, -Mathf.Cos(cam.transform.eulerAngles.x * Mathf.Deg2Rad) * followDistance);
    }


    #endregion



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        LateralMovement();
        Gravity();
        //Debug.Log(transform.rotation);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.transform.position.y < transform.position.y - (transform.localScale.y * 0.9f)) // if we are above the ground
        {
            grounded = true;
        }

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) //check if we collide with the ground
        //{
        //    if (collision.gameObject.transform.position.y < transform.position.y - (transform.localScale.y * 0.9f)) // if we are above the ground
        //    {
        //        grounded = true;
        //    }
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform.position.y < transform.position.y - (transform.localScale.y * 0.9f)) // if we are above the ground
        {
            grounded = false;
        }

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) //check if we collide with the ground
        //{
        //    if (collision.gameObject.transform.position.y < transform.position.y - (transform.localScale.y * 0.9f)) // if we are above the ground
        //    {
        //        grounded = false;
        //    }
        //}
    }


}
