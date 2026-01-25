using D_Dev.StateMachineBehaviour;

namespace _Project.Scripts.Core.PlayerController.PlayerStates
{
    public abstract class BasePlayerState : BaseModularState<PlayerStateName>
    {
        #region Fields
        
        protected PlayerStateController _controller;

        #endregion
        
        #region Properties

        public float ExitTime { get; }

        #endregion

        #region Public

        public void Init(PlayerStateController playerController) => _controller = playerController;
        
        #endregion
    }
}