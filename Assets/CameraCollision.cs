using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target; //玩家
    public float cameraDistance = 4.0f;
    public float smoothSpeed = 10f;
    public float collisionOffset = 0.2f;//與牆壁的最小距離
    public LayerMask collisionLayers;//撞到那些圖層會判定為牆壁
    private Vector3 currentVelocity;

    void LateUpdate()
    {
        Vector3 origin = target.position + Vector3.up * 1.5f;
        //調整射線起點往上，稍微避開地面。
        Vector3 desiredCameraPos = target.position - (transform.forward * cameraDistance);
        //攝影機理想位置，從玩家向後退cameraDistance的距離
        Vector3 direction = (transform.position - origin).normalized;
        //計算方向，從玩家到攝影機的方向向量，並用.normalized把他轉成單位向量，方便做運算。
        float distance = Vector3.Distance(origin, desiredCameraPos);
        //計算距離


        RaycastHit hit;
        //定義一個變數來存儲射線的結果

        if (Physics.Raycast(origin, direction, out hit, cameraDistance, collisionLayers))
            //射線偵測
            //從玩家位置發出，達到最遠的cameraDistance，被牆壁等圖層擋住代表有東西擋住了。

            desiredCameraPos = hit.point + direction * collisionOffset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredCameraPos, ref currentVelocity, 0.05f);

        Debug.DrawRay(target.position, direction * cameraDistance, Color.red);
    }
}

