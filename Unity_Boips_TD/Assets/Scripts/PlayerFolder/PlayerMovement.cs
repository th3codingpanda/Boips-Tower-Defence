using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        
        private InputManager input;
        [Header("Camera")]
        [SerializeField] public float mouseSensitivity;
        private float _verticalRotation;
        private float _horizontalRotation;
        [SerializeField] Camera playerCamera;

        
        [Header("Movement")]
        [SerializeField] Rigidbody rb;
        public float moveSpeed = 5f;
        private bool _sprinting;
    
        [Header("Jumping")] 

        public float jumpForce = 10f;
        public float fallMultiplier = 2.5f; 
        public float ascendMultiplier = 2f; 
        private bool _isGrounded = true;
        public LayerMask groundLayer;
        private float _groundCheckTimer;
        private readonly float _groundCheckDelay = 0;
        private readonly float _playerHeight = 2;
        private float _raycastDistance;
        
        void Start()
        {
            input = InputManager.Instance;
            // Set the raycast to be slightly beneath the player's feet
            _raycastDistance = (_playerHeight / 2) + 0.2f;
            input.CameraEvent.AddListener(RotateCamera);
            input.MoveEvent.AddListener(MovePlayer);
            input.JumpEvent.AddListener(Jump);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            // Checking when we're on the ground and keeping track of our ground check delay
            if (_isGrounded && _groundCheckTimer >= 0f)
            {
                _groundCheckTimer -= Time.deltaTime;
            }


        }

        void FixedUpdate()
        {
            ApplyJumpPhysics();
            // If we aren't moving and are on the ground, stop velocity so we don't slide
 
        }

        void MovePlayer(Vector2 direction)
        {
            Vector3 movement = (transform.right * direction.x + transform.forward * direction.y).normalized;
            Vector3 targetVelocity = movement * moveSpeed;
        
            // Apply movement to the Rigidbody
            Vector3 velocity = rb.linearVelocity;
            velocity.x = targetVelocity.x;
            velocity.z = targetVelocity.z;
            rb.linearVelocity = velocity;
            if (_isGrounded && direction is { x: 0, y: 0 })
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }

 
        }

        void RotateCamera(Vector2 direction)
        {
            _horizontalRotation = direction.x * mouseSensitivity;
            transform.Rotate(0, _horizontalRotation, 0);
            
            _verticalRotation -= direction.y * mouseSensitivity;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
            
            playerCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        }

        void Jump()
        {
            if (!_isGrounded && _groundCheckTimer <= 0f)
            {
                Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
                _isGrounded = Physics.Raycast(rayOrigin, Vector3.down, _raycastDistance, groundLayer);
            }
            if (_isGrounded)
            {
                _isGrounded = false;
                _groundCheckTimer = _groundCheckDelay;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }

            
        }

        void ApplyJumpPhysics()
        {
            if (rb.linearVelocity.y < 0) 
            {
                // Falling: Apply fall multiplier to make descent faster
                rb.linearVelocity += Vector3.up * (Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime);
            } // Rising
            else if (rb.linearVelocity.y > 0)
            {
                // Rising: Change multiplier to make player reach peak of jump faster
                rb.linearVelocity += Vector3.up * (Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime);
            }
        }
    }
}