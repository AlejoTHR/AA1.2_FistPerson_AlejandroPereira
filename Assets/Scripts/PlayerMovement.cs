using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // GET INPUT SYSTEM
    public InputSystem_Actions _input;

    [Header("MOVEMENT")]
    private Vector3 MousePos;
    public float WalkSpeed;
    public float JumpForce;
    

    private Rigidbody _rb;

    [Header("JUMP")]
    public float GroundDetectionRadius;
    public float GroundDetectionDistance;
    public LayerMask GroundDetectionLayers;
    public int Jumps = 0;
    public int JumpsMax = 2;
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
        if (_input.Player.Jump.WasPerformedThisFrame() && Jumps < JumpsMax)
        {
            Jumps++;
            _rb.AddForce(Vector3.up * JumpForce);
        }
    }


    private void FixedUpdate()
    {
        // PLAYER MOVEMENT
        Vector3 inputDir = _input.Player.Move.ReadValue<Vector2>();
        inputDir.z = inputDir.y;
        inputDir.y = 0;

        // ALTER TRANSFORM FROM WORLD TO LOCAL
        inputDir = transform.TransformDirection(inputDir);

        // INPUTS DIR TO TRANSFORM
        transform.position += inputDir.normalized * WalkSpeed * Time.deltaTime;


        RaycastHit hit;
        Physics.SphereCast(transform.position + GroundDetectionDistanceOffset, GroundDetectionRadius, Vector3.down, out hit, GroundDetectionDistance, GroundDetectionLayers);

        if (Physics.SphereCast(transform.position + GroundDetectionDistanceOffset, GroundDetectionRadius, Vector3.down, out hit, GroundDetectionDistance, GroundDetectionLayers))
        {
            if(Jumps == JumpsMax)
            {
                Jumps = 0;
            }

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
        Gizmos.DrawWireSphere(transform.position+ GroundDetectionDistanceOffset + Vector3.down * GroundDetectionDistance, GroundDetectionRadius);

    }


}
