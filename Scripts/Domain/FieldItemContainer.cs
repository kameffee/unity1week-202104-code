using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace kameffee.unity1week202104.Domain
{
    /// <summary>
    /// フィールド上のアイテム郡
    /// </summary>
    public class FieldItemContainer : IDisposable
    {
        private readonly List<ItemModel> container = new List<ItemModel>();

        // ピックアップされたとき
        public IObservable<ItemModel> OnPickup => onPickup;
        private readonly Subject<ItemModel> onPickup = new Subject<ItemModel>();

        public FieldItemContainer()
        {
        }

        public void Add(ItemModel itemModel)
        {
            if (container.Exists(model => model.ItemSettings.ItemId == itemModel.ItemSettings.ItemId))
            {
                Debug.LogError($"重複 : {itemModel}");
            }
            container.Add(itemModel);
        }

        public void Remove(ItemModel itemModel) => container.Remove(itemModel);

        /// <summary>
        /// 拾う
        /// </summary>
        public void Pickup(ItemModel itemModel)
        {
            Remove(itemModel);
            onPickup.OnNext(itemModel);
        }

        public ItemModel GetItem(int itemId) => container.FirstOrDefault(model => model.ItemSettings.ItemId == itemId);

        public void Dispose()
        {
            onPickup?.Dispose();
        }
    }
}