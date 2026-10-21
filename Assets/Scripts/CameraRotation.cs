using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    // GET INPUT SYSTEM
    public InputSystem_Actions _input;

    [Header("ROTATE")]
    private float MousePosY;
    public float SensibilityY;
    private float AngleMin = -60;
    private float AngleMax = 60;
    //private float LookAngleMax = 65;
    public Transform _cameraTrsfm;

    private void Start()
    {
        // ENABLE INPUT SYSTEM
        _input = new InputSystem_Actions();
        _input.Enable();

        // MOUSE CENTRADO, SE BORRA DE LA PANTALLA
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void Update()
    {
        // GET MOUSE POSITION * SENSITIVITY
        MousePosY += - Mouse.current.delta.ReadValue().y * SensibilityY;

    }

    private void LateUpdate()
    {
        // CLAMPS MOUSE Y VALUES BETWEEN MAX AND MIN
        MousePosY = Mathf.Clamp(MousePosY, AngleMin, AngleMax);

        // ROTATES CAMERA
        transform.localEulerAngles = new Vector3 (MousePosY, 0, 0);

    }

    




}
