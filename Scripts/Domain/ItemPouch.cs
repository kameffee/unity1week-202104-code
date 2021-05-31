using System;
using System.Linq;
using UniRx;

namespace kameffee.unity1week202104.Domain
{
    /// <summary>
    /// 持っているアイテム郡
    /// </summary>
    public class ItemPouch : IDisposable
    {
        private readonly ReactiveCollection<ItemModel> container = new ReactiveCollection<ItemModel>();

        public IObservable<ItemModel> OnAddItem => container.ObserveAdd().Select(ev => ev.Value);
        public IObservable<ItemModel> OnRemoveItem => container.ObserveAdd().Select(ev => ev.Value);
        public IReadOnlyReactiveProperty<int> ItemCount => itemCount;
        private ReactiveProperty<int> itemCount = new ReactiveProperty<int>();

        private CompositeDisposable disposable = new CompositeDisposable();

        public ItemPouch()
        {
            container.ObserveCountChanged()
                .Subscribe(count => itemCount.Value = count)
                .AddTo(disposable);
        }

        public void Add(ItemModel itemModel) => container.Add(itemModel);

        public void Remove(ItemModel itemModel) => container.Remove(itemModel);

        public ItemModel GetItem(int itemId) => container.FirstOrDefault(model => model.ItemSettings.ItemId == itemId);

        public void Dispose()
        {
            container?.Dispose();
            itemCount?.Dispose();
            disposable?.Dispose();
        }
    }
}