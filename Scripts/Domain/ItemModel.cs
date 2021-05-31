using System;
using kameffee.unity1week202104.View;
using UniRx;
using UnityEngine;

namespace kameffee.unity1week202104.Domain
{
    /// <summary>
    /// アイテムモデル
    /// </summary>
    public class ItemModel : IDisposable
    {
        private readonly FieldItemContainer container;

        public Vector3 Position { get; set; }

        public ItemSettings ItemSettings { get; set; }

        // フォーカスされているか
        public IReadOnlyReactiveProperty<bool> OnFocus => onFocus;
        private readonly ReactiveProperty<bool> onFocus = new ReactiveProperty<bool>();

        // 拾われた通知
        public IObservable<ItemModel> OnPickup =>
            container.OnPickup
                .Where(model => model == this);

        public bool IsPickuped { get; private set; }

        private IDisposable disposable;

        public ItemModel(FieldItemContainer container)
        {
            this.container = container;
        }

        public void Initialize(ItemSettings itemSettings)
        {
            ItemSettings = itemSettings;
            container.Add(this);
        }

        public void Focus(bool isFocus)
        {
            onFocus.Value = isFocus;
        }

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(ItemSettings)}: {ItemSettings}";
        }

        public void Dispose()
        {
            container?.Dispose();
            onFocus?.Dispose();
            disposable?.Dispose();
        }
    }
}