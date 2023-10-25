using System;
using UnityEngine;

namespace CommonUtility.Animations
{
    [Serializable]
    public class AnimatorBool
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private string _name;

        public void Set(bool value)
        {
            if (_animator == null)
                return;

            if (string.IsNullOrEmpty(_name))
                return;

            _animator.SetBool(_name, value);
        }
    }
}