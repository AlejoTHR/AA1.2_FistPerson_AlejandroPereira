using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    // GET INPUT SYSTEM
    public InputSystem_Actions _input;

    public Image _FUEL;

    [Header("MOVEMENT")]
    private Vector3 MousePos;
    public float WalkSpeed;
    public float JumpForce;
    public float JetpackForce;
    public float JetpackFuel;
    public float JetpackFuelMax;
    public float JetpackEmptyingMax;
    public float JetpackFillingTime;
    public float JetpackEmptyCount;
    public float JetpackEmptyCountMax;
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

        // STARTS WITH FULL FUEL
        JetpackFuel = JetpackFuelMax;
    }

    private void Update()
    {
        // GET MOUSE POSITION
        MousePos = Mouse.current.delta.ReadValue();


        // ALTER UI LIVE
        _FUEL.fillAmount = JetpackFuel;


        //PLAYER JUMP
        if (Grounded && _input.Player.Jump.WasCompletedThisFrame())
        {
            _rb.AddForce(Vector3.up * JumpForce);
          
        }

        if (_input.Player.Jump.IsPressed() && !Grounded )
        {
            JetpackFuel -= Time.deltaTime;

            if (JetpackFuel > 0)
            {
                _rb.AddForce(Vector3.up * JetpackForce);
            }
            else{
                JetpackFuel = 0;
            }
            

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

        //SIDEWARDS | SPEED EQUALS TO 75%
        if (inputDir.x != 0){
            inputDir *= SideWalkSpeed;
        }
        // BACKWARDS | SPEED EQUALS TO 50%
        if (inputDir.z < 0)
        {
            inputDir *= BackWalkSpeed;
        }

        // ALTER TRANSFORM FROM WORLD TO LOCAL
        inputDir = transform.TransformDirection(inputDir);

        // APLIES INPUT DIR TO TRANSFORM
        transform.position += inputDir * WalkSpeed * Time.deltaTime;




        // GROUND DETECTION
        RaycastHit hit;
        Physics.SphereCast(transform.position + GroundDetectionDistanceOffset, GroundDetectionRadius, Vector3.down, out hit, GroundDetectionDistance, GroundDetectionLayers);

        // SEND SPHERECAST FOR DETECTION
        if (Physics.SphereCast(transform.position + GroundDetectionDistanceOffset, GroundDetectionRadius, Vector3.down, out hit, GroundDetectionDistance, GroundDetectionLayers))
        {
            // DETETCS GROUND
            Grounded = true;

            JetpackFuel = JetpackFuelMax;

            /*
            // IF THERE IS STILL FUEL
            if (JetpackFuel > 0){
                // "REFILL THE FUEL"
                // DONT EXEED THE MAX
                if(JetpackFuel > JetpackFuelMax) JetpackFuel = JetpackFuelMax;


            }else if(JetpackFuel <= 0) {
                // DELAY
                JetpackEmptyCount += Time.deltaTime;
                if(JetpackFillingTime >= JetpackEmptyCountMax){
                    //"REFILL"
                    JetpackFuel += JetpackFillingTime / Time.deltaTime;
                }


            }
            */

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
