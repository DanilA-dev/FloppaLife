using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.Core
{
    public class RagdollController : MonoBehaviour
    {
        #region Fields

        [Title("Components")]
        [SerializeField] private Transform _musclesRoot;
        [SerializeField] private Collider _mainColl;
        [SerializeField] private Rigidbody _mainbody;

        [Title("Get Up")]
        [SerializeField] private Transform _ragdollRootBone;
        [SerializeField] private bool _alignRootOnDeactivate = true;
        [SerializeField] private bool _alignRotation = true;
        [ShowIf("@_alignRootOnDeactivate")]
        [SerializeField] private LayerMask _groundMask = ~0;
        [ShowIf("@_alignRootOnDeactivate")]
        [SerializeField, Min(0f)] private float _groundCheckDistance = 2f;

        [Title("Settings")]
        [SerializeField, ReadOnly] private bool _isActive;

        private const float FACING_THRESHOLD = 0.0001f;

        [FoldoutGroup("Events")]
        public UnityEvent OnRagdollActivate;
        [FoldoutGroup("Events")]
        public UnityEvent OnRagdollDeactivate;

        private Dictionary<Collider, Rigidbody> _ragdollMuscles;
        
        #endregion

        #region Properties

        public bool IsActive => _isActive;

        #endregion
        
        #region Monobehaviour

        private void Awake()
        {
            GetMuscles();
            DeactivateRagdoll();
        }

        #endregion

        #region Public

        public void ActivateRagdoll()
        {
            _mainColl.enabled = false;
            _mainbody.isKinematic = true;
            
            SetMusclesKinematic(false);
            SetMusclesColliderActive(true);

            if(_isActive)
                return;
            
            _isActive = true;
            OnRagdollActivate?.Invoke();
        }

        public void DeactivateRagdoll()
        {
            if (_isActive && _alignRootOnDeactivate)
                AlignRootToRagdollBone();

            _mainColl.enabled = true;
            _mainbody.isKinematic = false;

            SetMusclesKinematic(true);
            SetMusclesColliderActive(false);

            if(!_isActive)
                return;

            _isActive = false;
            OnRagdollDeactivate?.Invoke();
        }

        public void AlignRootToRagdollBone()
        {
            if (_ragdollRootBone == null || _mainbody == null)
                return;

            var root = _mainbody.transform;

            Vector3 targetPosition = _ragdollRootBone.position;
            if (Physics.Raycast(targetPosition, Vector3.down, out var hit, _groundCheckDistance, _groundMask))
                targetPosition = hit.point;

            Quaternion targetRotation = root.rotation;
            if (_alignRotation)
            {
                Vector3 flatForward = Vector3.ProjectOnPlane(GetBoneFacing(), Vector3.up);
                if (flatForward.sqrMagnitude > FACING_THRESHOLD)
                    targetRotation = Quaternion.LookRotation(flatForward.normalized, Vector3.up);
            }

            root.SetPositionAndRotation(targetPosition, targetRotation);
        }

        #endregion
        
        #region Private

        private void GetMuscles()
        {
            var colls = _musclesRoot.GetComponentsInChildren<Collider>();
            if(colls == null)
                return;

            _ragdollMuscles = new();
            
            foreach (var coll in colls)
                _ragdollMuscles.TryAdd(coll, coll.attachedRigidbody);
        }

        private void SetMusclesKinematic(bool value)
        {
            foreach (var (coll, body) in _ragdollMuscles)
                body.isKinematic = value;
        }
        
        private void SetMusclesColliderActive(bool value)
        {
            foreach (var (coll, body) in _ragdollMuscles)
                coll.enabled = value;
        }

        private Vector3 GetBoneFacing()
        {
            Vector3 boneUp = _ragdollRootBone.up;
            return boneUp.y >= 0f ? -boneUp : boneUp;
        }

        #endregion

        #region Debug

        [FoldoutGroup("Debug")]
        [Button]
        private void ToggleRagdoll(bool value)
        {
            if(value)
                ActivateRagdoll();
            else
                DeactivateRagdoll();
        }

        #endregion
    }
}