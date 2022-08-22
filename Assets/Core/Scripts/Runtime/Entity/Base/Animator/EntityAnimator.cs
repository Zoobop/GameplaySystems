using UnityEngine;

namespace Entity
{
    public class EntityAnimator : MonoBehaviour, IAnimator
    {
        [SerializeField] private Animator _animator;
        [Range(0, 1)] [SerializeField] private float _animationSmoothTime;

        private static readonly int Speed = Animator.StringToHash("Speed");

        public void SetAnimationBlend(float blendPercent, float time)
        {
            _animator.SetFloat(Speed, blendPercent, _animationSmoothTime, time);
        }

        public Animator GetAnimator()
        {
            return _animator;
        }
    }
}
