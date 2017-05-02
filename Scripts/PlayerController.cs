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

    Animator animator;
    Transform mainCamera;

    void Start()
    {
        //animator = GetComponent<Animator>();
        mainCamera = Camera.main.transform;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ai")
        {
            //timer += Time.deltaTime;
            //Time.timeScale = 0;
            //if (timer >= timerValue) {
                //Time.timeScale = 1;
                respawn();
                //timer = 0;
            //}
            
        }
        if (col.gameObject.name == "Ai LineOfSight")
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

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
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
        Vector3 randomDirection = Random.insideUnitSphere * movementRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, movementRadius, 1);
        Vector3 finalPosition = hit.position;
        transform.position = finalPosition;
    }
}