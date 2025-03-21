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
    [SerializeField] float sensitivity;
    [Space]
    [Header("Magic")]
    [SerializeField] GameObject magicCircle;


    #region _Movement_

    private void LateralMovement()
    {
        //the addative movement values need to change based on the direction im facing
        //woah thats kinda pog math
        float currentAngle = Mathf.Asin(transform.rotation.y) * 2;
        float wAngleFlip = (transform.rotation.w < 0 ? -1 : 1);

        float newXPos = transform.position.x + ((velocity.x * Time.fixedDeltaTime) * Mathf.Cos(currentAngle * -wAngleFlip)) + ((velocity.y * Time.fixedDeltaTime) * Mathf.Sin(currentAngle * wAngleFlip));
        float newZPos = transform.position.z + ((velocity.x * Time.fixedDeltaTime) * Mathf.Sin(currentAngle * -wAngleFlip)) + ((velocity.y * Time.fixedDeltaTime) * Mathf.Cos(currentAngle * wAngleFlip));
        transform.position = new Vector3(newXPos, transform.position.y, newZPos);
    }

    private void Gravity()
    {
        if (!grounded)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (-gravity * Time.fixedDeltaTime), transform.position.z);
            gravity += 9.8f * Time.fixedDeltaTime;
        }
        else
            gravity = 2.45f;
    }

    #endregion

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
        transform.Rotate(new Vector3(0, value.Get<Vector2>().x * Time.fixedDeltaTime * 20f * sensitivity, 0));

        //https://discussions.unity.com/t/please-explain-quaternions/96863/3

        //if the X component of the quarternion is greater than 0.70710678118 bad
        //if the X component of the quarternion is less than -0.70710678118 bad


        Quaternion angle = new Quaternion( Mathf.Clamp(cam.transform.localRotation.x + -value.Get<Vector2>().y * Time.fixedDeltaTime / 3 * sensitivity, -0.25f, 0.6f), 0, 0, cam.transform.localRotation.w).normalized;
        cam.transform.localRotation = angle;

        cam.transform.localPosition = new Vector3(XOffset, (Mathf.Sin(cam.transform.eulerAngles.x * Mathf.Deg2Rad) * followDistance) + YOffset, -Mathf.Cos(cam.transform.eulerAngles.x * Mathf.Deg2Rad) * followDistance);
    }


    public void OnCastSpell()
    {
        Debug.Log("woah magic");
        MagicCircle mc = Instantiate(magicCircle, this.transform.position + new Vector3(Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) * 2f, 0, Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) * 2f), transform.rotation, this.transform).GetComponent<MagicCircle>();
        mc.ExecuteMagic();
        //so when the player clicks, they will cast the spell (for now)
        //im trying to decide whether or not the magic circles will be scriptable objects, i dont think so.
        //i think they will just be noremal objects because they will have a visible form, direction/transform and may have differnet colours and sizes
    }

    #endregion



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        LateralMovement();
        Gravity();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.transform.position.y + (collision.gameObject.transform.localScale.y * 0.45f) < transform.position.y - (transform.localScale.y * 0.45f)) // if we are above the ground
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
        if (collision.gameObject.transform.position.y + (collision.gameObject.transform.localScale.y * 0.45f) < transform.position.y - (transform.localScale.y * 0.45f)) // if we are above the ground
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
