using D_Dev.MovementHandler;
using D_Dev.PolymorphicValueSystem;
using D_Dev.StateMachineBehaviour;
using D_Dev.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.States
{
    public class BaseCharacterState : BaseComponentState
    {
        #region Fields

        [SerializeField] protected BaseMovementController _movementController;

        [Space]
        [SerializeField] protected bool _canMove;
        [SerializeField] protected bool _canRotate;

        [ShowIf("@_canMove || _canRotate")]
        [SerializeReference] protected PolymorphicValue<Vector3> _movementDirection = new Vector3ConstantValue();

        [ShowIf(nameof(_canMove))]
        [SerializeReference] protected PolymorphicValue<float> _maxMoveSpeed = new FloatConstantValue();
        [ShowIf(nameof(_canMove))]
        [SerializeReference] protected PolymorphicValue<float> _accelerationSpeed = new FloatConstantValue();

        [ShowIf(nameof(_canRotate))]
        [SerializeReference] protected PolymorphicValue<Transform> _rotateRoot = new TransformConstantValue();
        [ShowIf(nameof(_canRotate))]
        [SerializeReference] protected PolymorphicValue<float> _rotateSpeed = new FloatConstantValue();

        protected RotationHandler _rotationHandler = new();

        #endregion

        #region State

        public override void OnEnter()
        {
            if (_canMove)
            {
                _movementController.ResumeMovement();
                _movementController.SetMaxVelocity(_maxMoveSpeed.Value);
                _movementController.SetAcceleration(_accelerationSpeed.Value);
            }
            else
            {
                _movementController.StopMovement();
            }

            if (_canRotate)
                _rotationHandler.Initialize(_rotateRoot.Value, _rotateSpeed.Value);
        }

        public override void OnUpdate()
        {
            if (_canRotate)
                _rotationHandler.RotateTowards(_movementDirection.Value);

            if (_canMove)
                _movementController.SetDirection(_movementDirection.Value);
        }

        #endregion
    }
}
