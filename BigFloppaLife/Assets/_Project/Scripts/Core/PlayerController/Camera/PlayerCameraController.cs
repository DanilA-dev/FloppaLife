using D_Dev.InputSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.PlayerController
{
    public class PlayerCameraController : MonoBehaviour
    {
        #region Fields

        [Title("Camera Settings")]
        [SerializeField] private InputRouter _inputRouter; 
        [SerializeField] private Transform _cameraRoot;
        [SerializeField] private bool _rotateCharacterRootTransform = true;
        [SerializeField] private float _topAngle = 80f;
        [SerializeField] private float _botAngle = -80f;
        [SerializeField] private float _rotationSpeed = 100f;
        [SerializeField] private float _rotationSmoothTime = 0.1f;
        [SerializeField] private bool _isLocked;
        
        private const float CAMERA_ROTATION_THRESHOLD = 0.01f;
        
        private Vector2 _currentLookInput;
        
        private float _cameraTargetPitch;
        private float _rotationVelocity;
        private float _targetRotation;
        private float _yaw;
        private float _pitch;

        #endregion

        #region Properties

        public Transform CameraRoot => _cameraRoot;
        public Vector3 CameraForward => _cameraRoot.forward;
        public Vector3 CameraRight => _cameraRoot.right;

        #endregion

        #region Monobehaviour

        private void Awake()
        {
            if(_inputRouter != null)
                _inputRouter.Look += OnLook;
        }

        private void OnDestroy()
        {
            if (_inputRouter != null)
                _inputRouter.Look -= OnLook;
        }

        private void LateUpdate() => UpdateCameraRotation();

        #endregion

        #region Public

        public void LockCamera() => _isLocked = true;

        public void UnlockCamera() => _isLocked = false;

        #endregion

        #region Listeners

        private void OnLook(Vector2 delta) => _currentLookInput = delta;

        #endregion

        #region Private

        private void UpdateCameraRotation()
        {
            if (_currentLookInput.sqrMagnitude >= CAMERA_ROTATION_THRESHOLD && !_isLocked)
            {
                float deltaTimeMultiplier = 1.0f;

                _yaw += _currentLookInput.x * _rotationSpeed * deltaTimeMultiplier * Time.deltaTime;
                _pitch += _currentLookInput.y * _rotationSpeed * deltaTimeMultiplier * Time.deltaTime;
            }

            _yaw = Mathf.Clamp(_yaw, float.MinValue, float.MaxValue);
            _pitch = Mathf.Clamp(_pitch, _botAngle, _topAngle);

            _cameraRoot.rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);
        }
        
        #endregion
    }
}