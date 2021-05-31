using System;
using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.View;
using UniRx;
using VContainer.Unity;

namespace kameffee.unity1week202104.Presenter
{
    public class PlayerStatusPresenter : IPostInitializable, IDisposable
    {
        private readonly IAirBombStatusView view;
        private readonly AirBombeModel model;

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        public PlayerStatusPresenter(AirBombeModel model, IAirBombStatusView view)
        {
            this.model = model;
            this.view = view;
        }

        public void PostInitialize()
        {
            this.model.Air
                .Subscribe(air => view.Render(air / AirBombeModel.MaxAir))
                .AddTo(disposable);
        }

        public void Dispose()
        {
            model?.Dispose();
        }
    }
}