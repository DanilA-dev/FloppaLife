using D_Dev.MovementHandler;
using D_Dev.StateMachineBehaviour;
using UnityEngine;

namespace _Project.Scripts.Core.States
{
    public class CharacterIdleState : BaseComponentState
    {
        #region Field

        [SerializeField] private BaseMovementController _movementController;

        #endregion

        #region State

        public override void OnEnter()
        {
            _movementController.StopMovement();
        }

        #endregion
    }
}