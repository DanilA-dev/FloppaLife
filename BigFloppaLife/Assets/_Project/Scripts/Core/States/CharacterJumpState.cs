using D_Dev.PolymorphicValueSystem;
using UnityEngine;

namespace _Project.Scripts.Core.States
{
    public class CharacterJumpState : BaseCharacterState
    {
        #region Fields

        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeReference] private PolymorphicValue<Vector3> _jumpDirection = new Vector3ConstantValue();
        [SerializeReference] private PolymorphicValue<float> _maxJumpVelocity = new FloatConstantValue();

        #endregion

        #region State

        public override void OnEnter()
        {
            base.OnEnter();

            var jump = _jumpDirection.Value.normalized * _maxJumpVelocity.Value;
            var velocity = _rigidbody.linearVelocity;
            _rigidbody.linearVelocity = new Vector3(velocity.x, jump.y, velocity.z);
        }

        #endregion
    }
}
