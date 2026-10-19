using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    // GET INPUT SYSTEM
    public InputSystem_Actions _input;

    [Header("ROTATE")]
    private float MousePos;
    private float LookAngleMax = 65;
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
        MousePos = - Mouse.current.delta.ReadValue().y;

        if (_cameraTrsfm.localEulerAngles.x > LookAngleMax)
        {
            _cameraTrsfm.localEulerAngles = new Vector3(LookAngleMax, 0, 0);
        }

    }
    private void LateUpdate()
    {
        // ROTATE CAMERA
        _cameraTrsfm.Rotate(MousePos, 0, 0);



    }

    




}
