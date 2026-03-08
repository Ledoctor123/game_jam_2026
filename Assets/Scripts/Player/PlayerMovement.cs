using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] public float walkSpeed = 8f;
        [SerializeField] public float rotationSpeed = 10f;
        //[SerializeField] public float _runSpeed = 8f;

        [Header("Dash Settings")] 
        [SerializeField] public float dashSpeed = 15f;
        [SerializeField] public float dashDuration = 0.2f;
        [SerializeField] public float dashCooldown = 0.5f;
        
        private Rigidbody _rb;
        private InputAction _moveAction;
        private InputAction _dashAction;
        //private InputAction _runAction;
        
        private Vector3 _movementVector;
        private Vector3 _dashDirection;
        private bool _isDashing;
        private float _dashEndTime;
        private float _nextDashTime;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _moveAction = InputSystem.actions.FindAction("Move");
            _dashAction = InputSystem.actions.FindAction("Dash");
            //_runAction = InputSystem.actions.FindAction("Sprint");
        }

        void Update()
        {
            if (_moveAction == null) return;
            Vector2 moveValue = _moveAction.ReadValue<Vector2>();
            Vector3 inputVector = new Vector3(moveValue.x, 0f, moveValue.y).normalized;
            
            _movementVector = Quaternion.Euler(0, 63f, 0) * inputVector;

            if (_dashAction != null && _dashAction.WasPressedThisFrame() && Time.time >= _nextDashTime && !_isDashing)
            {
                StartDash();
            }
            //_isRunning = _runAction != null && _runAction.IsPressed();
        }

        private void FixedUpdate()
        {
            if (_isDashing)
            {
                HandleDash();
            }
            else
            {
                HandleMovement(); 
                HandleRotation();
            }
        }

        private void StartDash()
        {
            _isDashing = true;
            _dashEndTime = Time.time + dashDuration;
            
            _dashDirection = _movementVector.magnitude > 0.1f ? _movementVector : transform.forward;
        }

        private void HandleDash()
        {
            _rb.linearVelocity = new Vector3(_dashDirection.x * dashSpeed, _rb.linearVelocity.y, _dashDirection.z * dashSpeed);

            if (Time.time >= _dashEndTime)
            {
                _isDashing = false;
                _nextDashTime = Time.time + dashDuration;
                
                _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
            }
        }
        
        private void HandleMovement()
        {
            if (_movementVector.magnitude > 0.1f)
            {
                //float currentSpeed = _isRunning ? _runSpeed : _walkSpeed;

                float currentSpeed = walkSpeed;
                _rb.linearVelocity = new Vector3(_movementVector.x * currentSpeed, _rb.linearVelocity.y, _movementVector.z * currentSpeed);
            }
            else
            {
                _rb.linearVelocity = new Vector3(0F, _rb.linearVelocity.y, 0F);
            }
        }

        private void HandleRotation()
        {
            if (_movementVector.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_movementVector);
                _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}