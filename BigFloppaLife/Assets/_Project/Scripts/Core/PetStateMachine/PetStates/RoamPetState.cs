using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Core.PetStates
{
    [System.Serializable]
    public class RoamPetState : BasePetState
    {
        #region Fields

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _roamRadius = 10f;

        private Vector3 _targetPosition;
        
        #endregion

        #region Overrides

        public override void OnEnter()
        {
            base.OnEnter();
            MoveToRandomPoint();
        }

        #endregion

        #region Public

        public bool IsTargetReached()
        {
            return Vector3.Distance(_navMeshAgent.transform.position, _targetPosition) <=
                   _navMeshAgent.stoppingDistance;
        }

        #endregion
        
        #region Private

        private void MoveToRandomPoint()
        {
            if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh)
                return;

            Vector3 randomDirection = Random.insideUnitSphere * _roamRadius;
            randomDirection += _navMeshAgent.transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _roamRadius, NavMesh.AllAreas))
            {
                _targetPosition = hit.position;
                _navMeshAgent.SetDestination(hit.position);
            }
        }

        #endregion
    }
}