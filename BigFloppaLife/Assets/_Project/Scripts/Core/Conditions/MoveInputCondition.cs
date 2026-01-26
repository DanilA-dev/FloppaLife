using System;
using D_Dev.Base;
using D_Dev.InputSystem;
using UnityEngine;

namespace _Project.Scripts.Core.Conditions
{
    [Serializable]
    public class MoveInputCondition : ICondition
    {
        #region Fields

        [SerializeField] private InputRouter _inputRouter;
        [SerializeField] private bool _value;

        private bool _isSubscribed;
        private bool _isMoving;
            
        #endregion

        #region ICondition

        public bool IsConditionMet()
        {
            if (!_isSubscribed)
            {
                _inputRouter.Move += OnMove;
                _isSubscribed = true;
            }

            return _isMoving == _value;
        }

        public void Reset()
        {
        }
        
        #endregion

        #region Listeners

        private void OnMove(Vector2 move)
        {
            _isMoving = move != Vector2.zero;
        }

        #endregion
    }
}