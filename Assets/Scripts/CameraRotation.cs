using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    // GET INPUT SYSTEM
    public InputSystem_Actions _input;

    [Header("ROTATE")]
    private Vector3 MousePos;
    public Transform _cameraTrsfm;


    private void Start()
    {
        // ENABLE INPUT SYSTEM
        _input = new InputSystem_Actions();
        _input.Enable();

    }


    private void Update()
    {
        // GET MOUSE POSITION
        MousePos =  Mouse.current.delta.ReadValue();
        
        // CAMERA ROTATION EQUAL TO MOUSEPOS
        _cameraTrsfm.Rotate(new Vector3(-MousePos.y, transform.localEulerAngles.y,0));





    }





}
