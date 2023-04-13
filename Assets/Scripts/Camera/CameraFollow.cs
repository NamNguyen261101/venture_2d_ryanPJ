using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetObj;


    [SerializeField] private float smoothTime = 0.25f; //  = 0.25f
    private Vector3 velocity;

    [Header("Axis Limitation")]
    [SerializeField] private Vector3 minValuesCamera, maxValuesCamera;

    [SerializeField] private float yOffset;
    [SerializeField] private float cameraFollowSpeed;
    private void FixedUpdate()
    {     
        FollowToTarget();
    }

    private void FollowToTarget()
    {
        #region Camera conntroll
        Vector3 newPos = new Vector3(targetObj.position.x, targetObj.position.y + yOffset, -8f);
        Vector3 boundPosition = new Vector3
                        (
                            (Mathf.Clamp(targetObj.position.x, minValuesCamera.x, maxValuesCamera.x)),
                            (Mathf.Clamp(targetObj.position.y, minValuesCamera.y, maxValuesCamera.y)),
                            transform.position.z
                        );

        // transform.position = Vector3.Lerp(transform.position, newPos, cameraFollowSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref boundPosition, smoothTime * Time.fixedDeltaTime); // ref boundPosition
        #endregion
    }
}
