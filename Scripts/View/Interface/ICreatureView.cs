using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public interface ICreatureView
    {
        CreatureAnimation Animation { get; }

        void Move(Vector2 vector);
    }
}