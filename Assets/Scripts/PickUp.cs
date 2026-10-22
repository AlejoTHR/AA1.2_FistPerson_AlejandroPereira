using UnityEngine;

public class PickUp : MonoBehaviour
{
    // GET INPUT SYSTEM
    public InputSystem_Actions _input;


    [Header("RAY FOR PICKUP")]
    public Transform _camera;
    public float PickUpDistance;
    public LayerMask PickUpLayer;



    private void Start()
    {

        // ENABLE INPUT SYSTEM
        _input = new InputSystem_Actions();
        _input.Enable();
    }

    private void Update()
    {
        // DRAW RAY FORWARD TO DETECT PICKUP
        RaycastHit hit;
        Physics.Raycast(_camera.position, _camera.forward, out hit, PickUpDistance, PickUpLayer);

        if(hit.collider != null)
        {

            if (_input.Player.PickUp.IsPressed()){

                // AFFECT PICKUP INERTIA
                hit.rigidbody.linearVelocity = _camera.up;
                
                // PICKUP POSITION = CAMERA POSITION + CAMERA FORWARD IN PICKUP DISTANCE
                hit.transform.position = _camera.transform.position + _camera.forward * PickUpDistance;
            }

        }




    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_camera.position, _camera.forward * PickUpDistance);

    }


}
