using Spine.Unity;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public class CreatureAnimation : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        public void Idle()
        {
            // skeletonAnimation.state.SetAnimation(0, "idle", true);
        }

        public void Walk()
        {
            // skeletonAnimation.state.SetAnimation(0, "walk", true);
        }
    }
}