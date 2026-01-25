using UnityEngine;

namespace _Project.Scripts.Core.PetStates
{
    [System.Serializable]
    public class IdleAgentState : BaseAgentState
    {
        #region Overrides

        public override void OnEnter()
        {
            _controller.NavMeshAgent.isStopped = true;
            _controller.NavMeshAgent.velocity = Vector3.zero;
        }

        public override void OnExit()
        {
            _controller.NavMeshAgent.isStopped = false;
        }

        #endregion
    }
}