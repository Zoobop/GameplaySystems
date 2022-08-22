using UnityEngine;

namespace Entity
{
    public interface IAnimator
    {
        public Animator GetAnimator();
        public void SetAnimationBlend(float speedPercent, float deltaTime);
    }
}