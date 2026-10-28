using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    // GET INPUT SYSTEM
    public InputSystem_Actions _input;
    

    [Header("RAY FOR PICKUP")]
    public Transform _camera;
    public float PickUpDistance;
    public float PickUpDistanceInitial;
    public float PickUpDistanceMax;
    public float PickUpSensibility;
    public LayerMask PickUpLayer;
    public float MouseScrollY;



    private void Start()
    {

        // ENABLE INPUT SYSTEM
        _input = new InputSystem_Actions();
        _input.Enable();
    }

    private void Update()
    {
        MouseScrollY = Mouse.current.scroll.ReadValue().y;

        if (PickUpDistance < 2) PickUpDistance = 2;

        // DRAW RAY FORWARD TO DETECT PICKUP
        RaycastHit hit;
        Physics.Raycast(_camera.position, _camera.forward, out hit, PickUpDistance, PickUpLayer);

        if(hit.collider != null)
        {

            // IF PICKUP IS DETECTED BY RAYCAST AND INPUT IS DETECTED
            if (_input.Player.PickUp.IsPressed()){

                // PICKUP DISTANCE ADAPTS TO MOUSE SCROLL
                PickUpDistance = PickUpDistance + MouseScrollY;
                // PICK DISTANCE CAN EXEED MAX VALUE
                if (PickUpDistance > PickUpDistanceMax) PickUpDistance = PickUpDistanceMax;


                // PICK DESTINY POSITION IS: camera poition + camera forward times PickUpDistance
                Vector3 PickUpDestiny = _camera.transform.position + _camera.forward * PickUpDistance;
              
                // PICKUP INERTIA TOWARDS DESTINY
                hit.rigidbody.linearVelocity = (PickUpDestiny - hit.transform.position) * PickUpSensibility * Time.deltaTime;


            }


        }
        else
        {
            // WHEN NO LONGER DETETCS
            PickUpDistance = PickUpDistanceInitial;
        }






    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_camera.position, _camera.forward * PickUpDistance);

    }


}
