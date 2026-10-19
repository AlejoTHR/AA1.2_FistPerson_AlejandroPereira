using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // GET INPUT SYSTEM
    public InputSystem_Actions _input;

    [Header("MOVEMENT")]
    private Vector3 MousePos;
    public float WalkSpeed;



    private void Start()
    {
        // ENABLE INPUT SYSTEM
        _input = new InputSystem_Actions();
        _input.Enable();
    }

    private void Update()
    {
        // GET MOUSE POSITION
        MousePos = Mouse.current.delta.ReadValue();
    }

    private void FixedUpdate()
    {
        if (_input.Player.MoveForward.IsPressed())
        {
            transform.position += transform.forward * WalkSpeed * Time.deltaTime;
        }
        if (_input.Player.MoveBackwards.IsPressed())
        {
            transform.position -= transform.forward * WalkSpeed * Time.deltaTime;
        }
        if (_input.Player.MoveRight.IsPressed())
        {
            transform.position += transform.right * WalkSpeed * Time.deltaTime;
        }
        if (_input.Player.MoveLeft.IsPressed())
        {
            transform.position -= transform.right * WalkSpeed * Time.deltaTime;
        }
    }


    private void LateUpdate()
    {
        // CAMERA ROTATION RELATIVE TO "MousePos"
        transform.Rotate(new Vector3(0, MousePos.x, 0));
    }


}
