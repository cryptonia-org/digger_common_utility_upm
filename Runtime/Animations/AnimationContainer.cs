using System;
using UnityEngine;
using CommonUtility.Extensions;

namespace CommonUtility.Animations
{
    [Serializable]
    public class AnimationContainer
    {
        [SerializeField]
        private Animation _animation;
        [SerializeField]
        private AnimationClip _clip;
        [SerializeField, Range(0f, 1f)]
        private float _blend;

        private WaitForSeconds _duration;

        public WaitForSeconds Duration
        {
            get
            {
                if (_duration == null)
                    _duration = _clip == null ? new WaitForSeconds(0) : new WaitForSeconds(_clip.length);

                return _duration;
            }
        }

        public float DurationFloat => _clip == null ? 0f : _clip.length;

        public void ToStart() => _animation.ToStartClip(_clip);

        public WaitForSeconds Call()
        {
            _animation?.SetClip(_clip, _blend);
            return Duration;
        }

        public void CallMomentum() => _animation.FinishClip(_clip);
    }
}