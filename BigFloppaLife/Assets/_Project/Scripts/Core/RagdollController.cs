using System;
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

        [Title("Settings")]
        [SerializeField, ReadOnly] private bool _isActive;

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
            _mainColl.enabled = true;
            _mainbody.isKinematic = false;
            
            SetMusclesKinematic(true);
            SetMusclesColliderActive(false);

            if(!_isActive)
                return;
            
            _isActive = false;
            OnRagdollDeactivate?.Invoke();
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