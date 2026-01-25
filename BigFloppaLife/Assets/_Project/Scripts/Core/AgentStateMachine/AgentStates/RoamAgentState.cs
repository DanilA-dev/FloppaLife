using D_Dev.PolymorphicValueSystem;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Core.PetStates
{
    [System.Serializable]
    public class RoamAgentState : BaseAgentState
    {
        #region Fields

        [SerializeReference] private PolymorphicValue<float> _moveSpeed;
        [SerializeReference] private PolymorphicValue<float> _roamRadius;

        private Vector3 _targetPosition;
        
        #endregion

        #region Overrides

        public override void OnEnter()
        {
            SetMoveSpeed();
            MoveToRandomPoint();
        }

        #endregion

        #region Public

        public bool IsTargetReached()
        {
            return Vector3.Distance(_controller.NavMeshAgent.transform.position, _targetPosition) <=
                   _controller.NavMeshAgent.stoppingDistance;
        }

        #endregion
        
        #region Private

        private void SetMoveSpeed()
        {
            _controller.NavMeshAgent.speed = _moveSpeed.Value;
        }
        
        private void MoveToRandomPoint()
        {
            if (_controller.NavMeshAgent == null || !_controller.NavMeshAgent.isOnNavMesh)
                return;

            Vector3 randomDirection = Random.insideUnitSphere * _roamRadius.Value;
            randomDirection += _controller.NavMeshAgent.transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _roamRadius.Value, NavMesh.AllAreas))
            {
                _targetPosition = hit.position;
                _controller.NavMeshAgent.SetDestination(hit.position);
            }
        }

        #endregion
    }
}