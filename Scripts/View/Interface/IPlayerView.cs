using System;
using UniRx;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public interface IPlayerView
    {
        IObservable<IItemView> OnHitPickUpItem { get; }

        IObservable<IItemView> OnEnterItem { get; }

        IObservable<IItemView> OnExitItem { get; }
        
        IObservable<IBaseCampView> OnEnterBaseCamp { get; }

        IObservable<IBaseCampView> OnExitBaseCamp { get; }

        void Move(Vector2 vector);

        void Jump();

        IReadOnlyReactiveProperty<bool> IsGround { get; }

        IReadOnlyReactiveProperty<bool> OnLightArea { get; }

        void SetPosition(Vector3 position);

        void SetActiveLight(bool isActive);
    }
}