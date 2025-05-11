using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class playermove : MonoBehaviour
{
// https://medium.com/eincode/create-third-person-controller-in-unity-838809d1a4da guide i used
    [SerializeField] float rotationSmoothTime; 
float currentAngle; 
float currentAngleVelocity;
CharacterController controller;
Camera cam;
Animator animator;
[SerializeField] float speed = 10f;
[SerializeField] float velocityY = 0f;
[SerializeField] float gravity = 9.8f;
[SerializeField] float gravityMultiplier = 1;
[SerializeField] float groundedGravity = -0.5f;
[SerializeField] float jumpHeight = 10f;
private PlayerInput playerInput;
private Transform cameraTransform;
private InputAction shootAction;
[SerializeField] private GameObject lazerPrefab;
[SerializeField] private Transform barrelTransform;
[SerializeField] private Transform lazerParent;
[SerializeField] private float LaserHitMissDistance = 100f;
    void Awake() 
{     
      animator = GetComponentInChildren<Animator>();
     controller = GetComponent<CharacterController>(); 
     playerInput = GetComponent<PlayerInput>();
         cam = Camera.main;
         cameraTransform = Camera.main.transform;
         shootAction = playerInput.actions["Shoot"];
     
}
private void OnEnable(){
    shootAction.performed += _ => ShootLaser();
}
private void OnDisable(){
    shootAction.performed += _ => ShootLaser();
}
 private void ShootLaser(){
    RaycastHit hit;
    GameObject Lazer = GameObject.Instantiate(lazerPrefab, barrelTransform.position, Quaternion.identity, lazerParent);
    LazerController lazerController = Lazer.GetComponent<LazerController>();
if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
{
lazerController.target = cameraTransform.position + cameraTransform.forward * LaserHitMissDistance;
lazerController.hit = true;
}

 }

void Update()
    {
        HandleMovement();
        HandleGravityAndJump();
    }

    void HandleGravityAndJump()
{
    if (controller.isGrounded && velocityY < 0f)
        velocityY = groundedGravity;
        animator.SetBool("Grounded", true);
        if (controller.isGrounded && Input.GetKey(KeyCode.Space))
    {
            animator.SetBool("Grounded", false);
            velocityY = Mathf.Sqrt(jumpHeight * 2f * gravity);
       
    }
        velocityY -= gravity * gravityMultiplier * Time.deltaTime;
    controller.Move(Vector3.up * velocityY * Time.deltaTime);
}

    

   private void HandleMovement()
{
    Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    if (movement.magnitude >= 0.1f)
    {
        animator.SetBool("Moving", true);
        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        controller.Move(rotatedMovement * speed * Time.deltaTime);
    }
    else
    {
     animator.SetBool("Moving", false);
    }
}
}