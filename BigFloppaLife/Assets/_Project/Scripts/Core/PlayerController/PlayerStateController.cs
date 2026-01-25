using _Project.Scripts.Core.PlayerController.PlayerStates;
using D_Dev.StateMachineBehaviour;

namespace _Project.Scripts.Core.PlayerController
{
    public class PlayerStateController : ModularStateMachineBehaviour<PlayerStateName, BasePlayerState>
    {
        #region Overrides

        protected override void OnStatesInitialized()
        {
            foreach (var basePlayerState in _states)
                basePlayerState.Init(this);
        }
        
        #endregion
    }
}