using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdpPersonCamera : MonoBehaviour
{
    public Transform target;//玩家或cameraTarget
    public Vector3 offset = new Vector3(0, 2, -4);//高度
    public float sensitivity = 200f;//靈敏度
    public float distance = 23f;//攝像頭和玩家之間的水平距離
    public float height = 0.5f;//攝像頭比玩家高多少
    public float collisionOffset = 0.2f;//碰撞偏移輛
    public LayerMask collisionLayers;//圖層碰撞

    private float yaw = 0f;
    private float pitch = 10f;

    private float currentRotationY;//當前旋轉y軸

    private void LateUpdate()
    {
        Vector3 targetPos = target.position;//攝影機目標點

        yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        //滑鼠的移動水平
        pitch = Mathf.Clamp(pitch, -30f, 60f);//調整他的流暢度

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = targetPos - rotation * Vector3.forward * distance + Vector3.up * height;//理想中攝影機的方向

        RaycastHit hit;//打出射線
        Vector3 direction = (desiredPosition - targetPos).normalized;
        float maxDistance = distance;

        if (Physics.Raycast(targetPos, direction, out hit, distance, collisionLayers))
        {
            maxDistance = hit.distance - collisionOffset;
            maxDistance = Mathf.Clamp(maxDistance, 0.5f, distance); // 防止太靠近或負值
        }

        Vector3 finalPosition = targetPos - rotation * Vector3.forward * maxDistance;

        transform.position = finalPosition;
        transform.rotation = rotation;

        // Debug
        Debug.DrawRay(targetPos, direction * maxDistance, Color.red);
    }
}
