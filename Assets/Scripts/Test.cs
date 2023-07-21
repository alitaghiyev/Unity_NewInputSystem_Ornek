using UnityEngine;

public class Test : MonoBehaviour
{
    [field: SerializeField] public InputReader InputReader { get; private set; }

    private void OnEnable()
    {
        InputReader.JumpEvent += Jump;
        InputReader.CrouchEvent += Crouch;
    }
    private void OnDisable()
    {
        InputReader.JumpEvent -= Jump;
        InputReader.CrouchEvent -= Crouch;
    }

    private void Update()
    {
        Movement();
        CameraLook();
        Attact();
    }
    private void Movement()
    {
        if (InputReader.MovementValue != Vector2.zero)
        {
            Debug.Log("Movement value: " + InputReader.MovementValue);
        }
    }
    private void CameraLook()
    {
        if (InputReader.LookValue != Vector2.zero)
        {
            Debug.Log("Mouse value: " + InputReader.LookValue);
        }
    }

    private void Jump()
    {
        Debug.Log("Jump");
    }

    private void Crouch(bool isCrouch){
        Debug.Log("is crouching: " + isCrouch);
    }

    private void Attact()
    {
        //if(InputReader.IsAttacking == false) return;
        //if(!InputReader.IsAttacking) return;
        //ve ya
        if (InputReader.IsAttacking == true)
        {
            Debug.Log("Attack");
        }
    }

}
