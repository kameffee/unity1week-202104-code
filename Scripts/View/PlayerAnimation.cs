using System;
using Spine.Unity;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private SkeletonMecanim mecanim;

        private static readonly int kIsGround = Animator.StringToHash("is_ground");
        private static readonly int kJump = Animator.StringToHash("jump");
        private static readonly int kIsWalk = Animator.StringToHash("is_walk");

        public void Right()
        {
            transform.localScale =  new Vector3(1, transform.localScale.y);
        }

        public void Left()
        {
            transform.localScale = new Vector3(-1, transform.localScale.y);
        }

        public void Walk(bool isWalk)
        {
            animator.SetBool(kIsWalk, isWalk);
        }

        public void Jump()
        {
            animator.SetTrigger(kJump);
        }

        public void SetGround(bool isGround)
        {
            animator.SetBool(kIsGround, isGround);
        }
    }
}