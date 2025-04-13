using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
// https://medium.com/eincode/create-third-person-controller-in-unity-838809d1a4da guide i used
    [SerializeField] float rotationSmoothTime; 
float currentAngle; 
float currentAngleVelocity;
CharacterController controller;
Camera cam;
[SerializeField] float speed = 10f;
[SerializeField] float velocityY = 0f;
[SerializeField] float gravity = 9.8f;
[SerializeField] float gravityMultiplier = 1;
[SerializeField] float groundedGravity = -0.5f;
[SerializeField] float jumpHeight = 10f;

    void Awake() 
{      
     controller = GetComponent<CharacterController>(); 
         cam = Camera.main;
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
        if (controller.isGrounded && Input.GetKey(KeyCode.Space))
    {
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
        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        controller.Move(rotatedMovement * speed * Time.deltaTime);
    }
}
}