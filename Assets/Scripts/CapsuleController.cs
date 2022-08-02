using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 5.0f;
    public float gravity = 20.8f;
    private Vector3 moveDirection;
    private CharacterController chCtrl;

    // cameras
    public Camera dollyCamera;
    public Camera fixedCamera;
    public Camera _1raPersonCamera;


    // Start is called before the first frame update
    void Start()
    {
        chCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (chCtrl.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= (gravity * Time.deltaTime);
        chCtrl.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Test"))
        {

            SwitchOfCamera();
        }
        if (other.gameObject.CompareTag("ChangeCamera"))
        {
            Debug.Log("change 1ra person");
            SwitchOfCamera1raPerson();
        }
    }

    private void SwitchOfCamera1raPerson()
    {
        if (dollyCamera.enabled)
        {
            Debug.Log("change Camera of 1ra Person");
            // fixed camera
            _1raPersonCamera.enabled = true;
            _1raPersonCamera.GetComponent<AudioListener>().enabled = true;

            // dolly camera
            dollyCamera.enabled = false;
            dollyCamera.GetComponent<AudioListener>().enabled = false;

        }

        else if (_1raPersonCamera.enabled)
        {
            Debug.Log("change Dolly Camera");

            // dolly camera
            dollyCamera.enabled = true;
            dollyCamera.GetComponent<AudioListener>().enabled = true;

            // fixed camera
            _1raPersonCamera.enabled = false;
            _1raPersonCamera.GetComponent<AudioListener>().enabled = false;

        }

    }

    private void SwitchOfCamera()
    {
        if (dollyCamera.enabled)
        {
            Debug.Log("change Fixed Camera");
            // fixed camera
            fixedCamera.enabled = true;
            fixedCamera.GetComponent<AudioListener>().enabled = true;
           
            // dolly camera
            dollyCamera.enabled = false;
            dollyCamera.GetComponent<AudioListener>().enabled = false;

        }

        else if (fixedCamera.enabled)
        {
            Debug.Log("change Dolly Camera");

            // dolly camera
            dollyCamera.enabled = true;
            dollyCamera.GetComponent<AudioListener>().enabled = true;

            // fixed camera
            fixedCamera.enabled = false;
            fixedCamera.GetComponent<AudioListener>().enabled = false;

        }


    }

}
