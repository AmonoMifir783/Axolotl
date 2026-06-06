using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private CharacterController characterController;

    public float speedMove = 1f;
    public float speedRotate = 1f;
    public float jumpForce = 1f;
    public float Gravity = -1f;


    private float rotationY;
    private float vectorVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 movementVector)
    {
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move = move * speedMove * Time.deltaTime;
        characterController.Move(move);


        vectorVelocity = vectorVelocity + Gravity * Time.deltaTime;
        characterController.Move(new Vector3(0, vectorVelocity, 0) * Time.deltaTime);
    }

    public void Rotate(Vector2 rotationVector)
    {
        rotationY += rotationVector.x * speedRotate * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            vectorVelocity = jumpForce;
        }
    }
}
