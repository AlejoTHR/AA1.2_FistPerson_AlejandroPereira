using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // GET INPUT SYSTEM
    public InputSystem_Actions _input;

    private Rigidbody _rb;

    [Header("MOVEMENT")]
    public Vector2 _Direction;
    public float WalkSpeed;


    private void Start()
    {
        // ENABLE INPUT SYSTEM
        _input = new InputSystem_Actions();
        _input.Enable();

        _rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        _Direction = _input.Player.Move.ReadValue<Vector2>().normalized;
    }

    private void FixedUpdate()
    {

        transform.position += new Vector3(_Direction.x, 0 ,_Direction.y) * WalkSpeed * Time.fixedDeltaTime;


    }


}
