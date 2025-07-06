using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public float walkSpeed = 5f;//移動速度
    public float runSpeed = 9f;//奔跑速度
    public float jumpHeight = 8f;//jumpHeight 跳躍高度
    public float gravity = -100f;//重力
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;//判斷是否站在地面
    private PlayerStats stats;
    private bool isRunning = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
    }
    // Update is called once per frame
    void Update()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed ;
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // 角度轉換
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //在Update 中加入測試傷害用的暫時段落
        if (Input.GetKeyDown(KeyCode.H))//H受到傷害
        {
            stats.TakeDamage(10);
        }
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //if(direction.magnitude　>= 0.1f)
        //{
        //取得相機方向

        //    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothTime);

        //    transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //    controller.Move(moveDir.normalized * MoveSpeed * Time.deltaTime);
        //}
        //isGrounded = controller.isGrounded;
        //if(isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}
        //float movex = Input.GetAxis("Horizontal");//A D移動

        //float movez = Input.GetAxis("Vertical");//w s 移動

        //Vector3 move = transform.right * movex + transform.forward * movez;//腳色的方向計算移動向量

        //controller.Move(moveDir.normalized * MoveSpeed * Time.deltaTime);//角色移動，只處理水平移動，垂直由重力與跳躍控制
    }
}
