using System;
using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.View;
using UniRx;
using VContainer.Unity;

namespace kameffee.unity1week202104.Presenter
{
    public class ItemStatusPresenter : IPostInitializable, IDisposable
    {
        private readonly ItemPouch itemPouch;
        private readonly IItemStatusView view;

        private CompositeDisposable disposable = new CompositeDisposable(); 

        public ItemStatusPresenter(ItemPouch itemPouch, IItemStatusView view)
        {
            this.itemPouch = itemPouch;
            this.view = view;
        }

        public void PostInitialize()
        {
            itemPouch.ItemCount
                .Subscribe(count => view.RenderOwn(count, 5))
                .AddTo(disposable);
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}