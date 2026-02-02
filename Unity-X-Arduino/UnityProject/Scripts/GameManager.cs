using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    
    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera; // Assign in Inspector
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private bool invertMouseY = false;
    
    private CharacterController controller;
    private float xRotation = 0f;
    private float currentSpeed;
    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // If camera not assigned, try to find it
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera == null)
            {
                playerCamera = FindObjectOfType<Camera>();
                Debug.LogWarning("Camera not assigned. Found camera in scene: " + (playerCamera != null));
            }
        }
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        if (playerCamera == null)
        {
            Debug.LogError("No camera found! Cannot handle mouse look.");
            return;
        }
        
        HandleMouseLook();
        HandleMovement();
        HandleSprint();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply mouseY direction
        if (invertMouseY)
        {
            xRotation += mouseY;
        }
        else
        {
            xRotation -= mouseY;
        }
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera up/down using the assigned camera
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // Rotate player left/right
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get movement direction based on where camera is looking
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        
        // Flatten the camera direction (ignore up/down)
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction
        Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;
        
        // Apply gravity
        if (!controller.isGrounded)
        {
            moveDirection.y -= 9.81f * Time.deltaTime;
        }
        else
        {
            moveDirection.y = -0.5f; // Small downward force to keep grounded
        }

        // Normalize diagonal movement
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // Move the character
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    void HandleSprint()
    {
        // Check for Shift + W or Shift + Up Arrow
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool forwardKeyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        
        isSprinting = shiftPressed && forwardKeyPressed;
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
    }

    // Public method to set camera (can be called from other scripts)
    public void SetCamera(Camera cam)
    {
        playerCamera = cam;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}