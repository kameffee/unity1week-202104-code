using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public class GoalPoint : MonoBehaviour, IGoalPoint
    {
        [SerializeField]
        private Collider2D collider2D;

        public IObservable<Unit> OnGoal => collider2D.OnTriggerEnter2DAsObservable().AsUnitObservable();
    }
}