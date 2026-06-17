using D_Dev.MovementHandler;
using D_Dev.PolymorphicValueSystem;
using D_Dev.StateMachineBehaviour;
using D_Dev.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.States
{
    public class CharacterMoveState : BaseComponentState
    {
        #region Fields

        [SerializeField] private BaseMovementController _movementController;
        [Space]
        [SerializeReference] private PolymorphicValue<Vector3> _movementDirection = new Vector3ConstantValue();
        [SerializeReference] private PolymorphicValue<float> _maxMoveSpeed = new FloatConstantValue();
        [SerializeReference] private PolymorphicValue<float> _accelerationSpeed = new FloatConstantValue();
        [SerializeField] private bool _rotateTowardsMovement;

        [ShowIf(nameof(_rotateTowardsMovement))]
        [SerializeReference] private PolymorphicValue<Transform> _rotateRoot = new TransformConstantValue();
        [ShowIf(nameof(_rotateTowardsMovement))]
        [SerializeReference] private PolymorphicValue<float> _rotateSpeed = new FloatConstantValue();
        
        private RotationHandler _rotationHandler = new();
        
        #endregion

        #region State

        public override void OnEnter()
        { 
            _movementController.ResumeMovement();
            _movementController.SetMaxVelocity(_maxMoveSpeed.Value);
            _movementController.SetAcceleration(_accelerationSpeed.Value);

            if (_rotateTowardsMovement)
                _rotationHandler.Initialize(_rotateRoot.Value, _rotateSpeed.Value);
        }


        public override void OnUpdate()
        {
           if(_rotateTowardsMovement)
               _rotationHandler.RotateTowards(_movementDirection.Value);
           
           _movementController.SetDirection(_movementDirection.Value);
        }

        #endregion
    }
}