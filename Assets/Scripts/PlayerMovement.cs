using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // GET INPUT SYSTEM
    public InputSystem_Actions _input;

    [Header("MOVEMENT")]
    private Vector3 MousePos;
    public float WalkSpeed;
    public float InitialWalkSpeed = 5f;
    public float JumpForce;
    public float BackWalkSpeed = 0.5f;
    public float SideWalkSpeed = 0.75f;

    

    private Rigidbody _rb;

    [Header("JUMP")]
    public float GroundDetectionRadius;
    public float GroundDetectionDistance;
    public LayerMask GroundDetectionLayers;
    public bool Grounded = false;
    public Vector3 GroundDetectionDistanceOffset;


    /*
        Vector2 dir = _input.Player.Move.Readvalue<Vector2>(); 

        transform.position += dir.y * transform.forward * Speed * Time.deltaTime;
        transform.position += dir.x * transform.right * Speed * Time.deltaTime;

        transform.TransformDirection(); USAR ESTO
     
     */

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        // ENABLE INPUT SYSTEM
        _input = new InputSystem_Actions();
        _input.Enable();
    }

    private void Update()
    {
        // GET MOUSE POSITION
        MousePos = Mouse.current.delta.ReadValue();


        //PLAYER JUMP
        if (_input.Player.Jump.WasPerformedThisFrame() && Grounded)
        {
            _rb.AddForce(Vector3.up * JumpForce);
        }

    }


    private void FixedUpdate()
    {
        // PLAYER MOVEMENT
        Vector3 inputDir = _input.Player.Move.ReadValue<Vector2>();
       // READ "z" AS "y"
        inputDir.z = inputDir.y;
        // "y" = 0
        inputDir.y = 0;

        // SPEED EQUAL TO SPEED
        WalkSpeed = InitialWalkSpeed;

        //SIDEWARDS | SPEED EQUALS TO 75%
        if (inputDir.x != 0){
            WalkSpeed = InitialWalkSpeed * SideWalkSpeed;

        }
        // BACKWARDS | SPEED EQUALS TO 50%
        if (inputDir.z < 0)
        {
            WalkSpeed = InitialWalkSpeed * BackWalkSpeed;
        }


        // ALTER TRANSFORM FROM WORLD TO LOCAL
        inputDir = transform.TransformDirection(inputDir);

        // APLIES INPUT DIR TO TRANSFORM
        transform.position += inputDir.normalized * WalkSpeed * Time.deltaTime;


        // GROUND DETECTION
        RaycastHit hit;
        Physics.SphereCast(transform.position + GroundDetectionDistanceOffset, GroundDetectionRadius, Vector3.down, out hit, GroundDetectionDistance, GroundDetectionLayers);

        if (Physics.SphereCast(transform.position + GroundDetectionDistanceOffset, GroundDetectionRadius, Vector3.down, out hit, GroundDetectionDistance, GroundDetectionLayers))
        {

            Grounded = true;
        }
        else
        {
            Grounded = false;
        }




    }


    private void LateUpdate()
    {

        // CAMERA ROTATION RELATIVE TO "MousePos"
        transform.Rotate(new Vector3(0, MousePos.x, 0));
    
    }


    private void OnDrawGizmos()
    {
        //PLAYER JUMP SPHERECAST
        Gizmos.DrawWireSphere(transform.position + GroundDetectionDistanceOffset + Vector3.down * GroundDetectionDistance, GroundDetectionRadius);

    }


}
