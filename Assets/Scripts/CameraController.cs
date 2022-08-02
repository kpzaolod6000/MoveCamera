using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //camera will follow this target
    public Transform target;
    //camera transform
    public Transform camTransform;
    //offset between camera and target
    private Vector3 offset;
    private Vector3 oldCameraPosition;
    private Vector3 oldTargetPosition;
    private Vector3 oldoffset;

    public Vector2 positionlimTargetX;
    public Vector2 positionlimTargetZ;
    //change this value to get desired smoothness
    public float smoothTime = 0.3f;
    // this value will change at the runtime depending on target movement 
    private Vector3 velocity = Vector3.zero;


    // translate camera
    private bool cameraColision = false;
    private bool cameraMove = true;

    public float rotateSpeed = 20.0f;
    public float angleMax = 30.0f;

    private Vector3 initialVector = Vector3.forward;


    // Start is called before the first frame update
    void Start()
    {
        offset = camTransform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //change rotation
        
        if (cameraColision)
        {
            
            float rotateDegrees = 0f;
            rotateDegrees -= rotateSpeed * Time.deltaTime;
            Vector3 currentVector = transform.position - target.position;
            currentVector.y = 0;
            float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).y > 0 ? 1 : -1);
            float newAngle = Mathf.Clamp(angleBetween + rotateDegrees, -angleMax, angleMax);
            rotateDegrees = newAngle - angleBetween;
            Debug.Log(rotateDegrees);
            transform.RotateAround(target.position, Vector3.up, rotateDegrees* Time.deltaTime);
            if (rotateDegrees < 100)
            {
                cameraColision = false;
                cameraMove = false;
                offset = camTransform.position - target.position;
            }
        }
        else
        {
            if (cameraMove)
            {
                Vector3 targetPosition = target.position + offset;
                positionlimTargetX = new Vector2(target.position.x - 2.0f, target.position.x + 2.0f);
                positionlimTargetZ = new Vector2(0.0f, target.position.x + 2.0f);
               

                camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
                oldCameraPosition = camTransform.position;
                oldTargetPosition = target.position;
                oldoffset = offset;
            }
            
            else
            {
                
                if((target.position.x >= positionlimTargetX.y || target.position.x <= positionlimTargetX.x) || target.position.z >= positionlimTargetZ.y)
                {
                    Debug.Log("retornando posiiton de camara");
                    Vector3 l = oldCameraPosition - oldTargetPosition; //new offset

                    camTransform.position = target.position + l;

                    cameraMove = true;
                }
            }
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("TowerCollader"))
        {
            Debug.Log("camara chocando");
            cameraColision = true;
            
        }
        
    }
}
