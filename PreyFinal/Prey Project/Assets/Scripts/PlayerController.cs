using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 2;
    public float runSpeed = 6;

    public float timer;
    public int timerValue = 5;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public int movementRadius = 3000;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    public Animation anim;
    Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform;
        anim.enabled = false;
        //anim.GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //timer += Time.deltaTime;
            //Time.timeScale = 0;
            //if (timer >= timerValue) {
                //Time.timeScale = 1;
                respawn();
                //timer = 0;
            //}
            
        }
        if (col.gameObject.CompareTag("EnemyLOS"))
        {
            //timer += Time.deltaTime;
            //Time.timeScale = 0;
            //if (timer >= timerValue)
            //{
                //Time.timeScale = 1;
                respawn();
                //timer = 0;
            //}
        }
    }

    void Update()
    {

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 inputDir = input.normalized;
        anim.enabled = false;

        if (inputDir != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            anim.enabled = true;

            //anim.SetBool("isWalking", true);
            //set anim.SetBool("isWalking", false); where this occurs. 
            //add anim.SetBool("isRunning", true); where occurs
            //add anim.Bool("isRunning", false); where happen
        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
        //animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

    }

    void respawn()
    {
        Vector3 randomDirection = new Vector3(3,1,0);
        transform.position = randomDirection;        
        
    }
}