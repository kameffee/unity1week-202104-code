using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.View;
using UniRx;
using VContainer.Unity;

namespace kameffee.unity1week202104.Presenter
{
    public class TitlePresenter : IAsyncStartable, IDisposable
    {
        private readonly TitleModel model;
        private readonly ITitleView view;

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        public TitlePresenter(TitleModel model, ITitleView view)
        {
            this.model = model;
            this.view = view;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await model.IntroPerformer();

            view.OnClickStart
                .Subscribe(async _ => await model.StartGame())
                .AddTo(disposable);
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}