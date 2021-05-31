using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public interface IItemView
    {
        ItemSettings ItemSettings { get; }

        Vector3 Position { get; }

        IObservable<Unit> OnFocus { get; }

        IObservable<Unit> OnUnFocus { get; }

        UniTask PlayEffect();

        void Delete();
    }
}