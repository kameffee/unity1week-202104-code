using UnityEngine;

namespace kameffee.unity1week202104.View
{
    /// <summary>
    /// 生き物
    /// </summary>
    public class CreatureView : MonoBehaviour, ICreatureView
    {
        [SerializeField]
        private Rigidbody2D rigidbody;

        [SerializeField]
        private CreatureSettings settings;

        [SerializeField]
        private CreatureAnimation animation;

        public CreatureAnimation Animation => animation;

        public void Move(Vector2 vector)
        {
            rigidbody.velocity = new Vector2(vector.x * settings.moveSpeed, rigidbody.velocity.y);
        }
    }
}