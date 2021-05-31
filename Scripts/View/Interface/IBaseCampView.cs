using System;
using UniRx;

namespace kameffee.unity1week202104.View
{
    public interface IBaseCampView
    {
        int BaseCampId { get; }

        IObservable<IBaseCampConsumer> OnEnterArea();

        IObservable<IBaseCampConsumer> OnExitArea();

        void Action();

        IObservable<Unit> OnAction();
    }
}