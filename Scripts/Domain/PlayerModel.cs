using System;
using kameffee.unity1week202104.View;
using UniRx;
using UnityEngine;

namespace kameffee.unity1week202104.Domain
{
    public class PlayerModel : IDisposable
    {
        private readonly FieldItemContainer fieldItemContainer;

        /// アクティブ状態
        public IReadOnlyReactiveProperty<bool> Active => active;

        private readonly ReactiveProperty<bool> active = new ReactiveProperty<bool>();

        /// 酸素ボンベを使っている状態か
        public IReadOnlyReactiveProperty<bool> UseAirBomb => useAirBomb;

        private readonly ReactiveProperty<bool> useAirBomb = new ReactiveProperty<bool>();

        /// 動いていた時
        public IObservable<Vector2> OnMove => onMove;

        private readonly Subject<Vector2> onMove = new Subject<Vector2>();

        /// 現在フォーカスしているアイテム
        public IReadOnlyReactiveProperty<ItemModel> FocusItem => focusItem;

        private readonly ReactiveProperty<ItemModel> focusItem = new ReactiveProperty<ItemModel>();

        public bool HasFocusItem => focusItem.Value != null;

        public IReadOnlyReactiveProperty<BaseCampModel> FocusBaseCamp => focusBaseCamp;
        private readonly ReactiveProperty<BaseCampModel> focusBaseCamp = new ReactiveProperty<BaseCampModel>();

        /// 地面についているか
        public IReadOnlyReactiveProperty<bool> OnGround => onGround;

        private readonly ReactiveProperty<bool> onGround = new ReactiveProperty<bool>();

        // 死んだ時
        public IObservable<Unit> OnDead => onDead;
        private readonly Subject<Unit> onDead = new Subject<Unit>();

        // ライト有効フラグ
        public IReadOnlyReactiveProperty<bool> LightActive => lightActive;
        private readonly ReactiveProperty<bool> lightActive = new ReactiveProperty<bool>();

        // ジャンプした時
        public IObservable<Unit> OnJump => onJump;
        private readonly Subject<Unit> onJump = new Subject<Unit>();

        public IObservable<Unit> OnAction => onAction;
        private readonly Subject<Unit> onAction = new Subject<Unit>();

        // 復帰時
        public IObservable<Vector3> OnRespawn => onRespawn;
        private readonly Subject<Vector3> onRespawn = new Subject<Vector3>();

        public AirBombeModel AirBombe { get; }

        public ItemPouch ItemPouch { get; }

        public PlayerModel(AirBombeModel airBombe, ItemPouch itemPouch, FieldItemContainer fieldItemContainer)
        {
            this.fieldItemContainer = fieldItemContainer;
            this.AirBombe = airBombe;
            this.ItemPouch = itemPouch;
        }

        public void Move(Vector2 vector)
        {
            onMove.OnNext(vector);
        }

        /// <summary>
        /// アイテムを拾う.
        /// </summary>
        /// <param name="itemModel"></param>
        public void PickupItem(ItemModel itemModel)
        {
            if (itemModel == null) return;

            focusItem.Value = null;
            Debug.Log($"PickupItem : {itemModel}");
            fieldItemContainer.Pickup(itemModel);
            ItemPouch.Add(itemModel);
        }

        public void Action()
        {
            onAction.OnNext(Unit.Default);
        }

        public void Jump()
        {
            // 地面についてないときはジャンプできない.
            if (!onGround.Value) return;

            onJump.OnNext(Unit.Default);
        }

        public void OnEnterFocus(ItemModel itemModel)
        {
            itemModel.Focus(true);
            focusItem.Value = itemModel;
        }

        public void OnExitFocus(ItemModel itemModel)
        {
            if (itemModel == null) return;

            itemModel.Focus(false);
            focusItem.Value = null;
        }

        public void OnEnterBaseCampFocus(BaseCampModel baseCamp)
        {
            focusBaseCamp.Value = baseCamp;
        }

        public void OnExitBaseCampFocus(BaseCampModel baseCamp)
        {
            focusItem.Value = null;
        }

        public void SetFocusBaseCamp(BaseCampModel baseCampModel)
        {
            focusBaseCamp.Value = baseCampModel;
        }

        public void SetUseAirBomb(bool isUse)
        {
            useAirBomb.Value = isUse;
        }

        public void SetActive(bool isActive)
        {
            active.Value = isActive;
        }

        public void SetOnGround(bool onGround)
        {
            this.onGround.Value = onGround;
        }

        public void Dead()
        {
            onDead.OnNext(Unit.Default);
        }

        public void SetLight(bool isActive) => lightActive.Value = isActive;

        public void Dispose()
        {
            onMove?.Dispose();
        }

        public void Respawn(Vector3 respawnPosition)
        {
            onRespawn.OnNext(respawnPosition);
        }
    }
}