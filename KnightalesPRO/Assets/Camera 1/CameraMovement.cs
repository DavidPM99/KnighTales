using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Transform mageTarget;
    public Vector3 cameraOffset;
    public float speed;
    Vector3 velocity = Vector3.zero;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        maxPosition = CameraValues.getMaxPos();
        minPosition = CameraValues.getMinPos();
        
    }

    private void FixedUpdate()
    {
        if (target.gameObject.active == false)
        {
            float xClamp = Mathf.Clamp(mageTarget.position.x, minPosition.x, maxPosition.x);
            float yClamp = Mathf.Clamp(mageTarget.position.y, minPosition.y, maxPosition.y);
            Vector3 magePos = mageTarget.position + cameraOffset;
            Vector3 clampedPos = new Vector3(Mathf.Clamp(magePos.x, minPosition.x, maxPosition.x), Mathf.Clamp(magePos.y, minPosition.y, maxPosition.y), -1);
            Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos, ref velocity, speed * Time.deltaTime);
            transform.position = smoothPos;

        }else{
            float xClamp = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
            float yClamp = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);
            Vector3 targetPos = target.position + cameraOffset;
            Vector3 clampedPos = new Vector3(Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x), Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y), -1);
            Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos, ref velocity, speed * Time.deltaTime);
            transform.position = smoothPos;
        }
        
        
        

    }
}
