using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public class BaseCampView : MonoBehaviour, IBaseCampView
    {
        private static readonly string TargetTag = "Player";

        [SerializeField]
        private int baseCampId = 0;

        [SerializeField]
        private Collider2D collider2D;

        public int BaseCampId => baseCampId;

        public IObservable<IBaseCampConsumer> OnEnterArea() =>
            collider2D.OnTriggerEnter2DAsObservable()
                .Select(collider => collider.GetComponent<IBaseCampConsumer>());

        public IObservable<IBaseCampConsumer> OnExitArea() =>
            collider2D.OnTriggerExit2DAsObservable()
                .Select(collider => collider.GetComponent<IBaseCampConsumer>());

        public IObservable<Unit> OnAction() => action;
        private readonly Subject<Unit> action = new Subject<Unit>();

        public void Action() => action.OnNext(Unit.Default);
    }
}