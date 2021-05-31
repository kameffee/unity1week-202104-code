using System;
using UniRx;

namespace kameffee.unity1week202104.View
{
    public interface ITitleView
    {
        IObservable<Unit> OnClickStart { get; }
    }
}