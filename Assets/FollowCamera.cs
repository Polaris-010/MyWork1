using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;//拖入玩家物件
    public Vector3 offset = new Vector3(0f, 5f, -7f);
    public float smoothSpeed = 10f;

    private void LateUpdate()
    {
        Vector3 desiredPoaition = target.position + target.rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPoaition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
