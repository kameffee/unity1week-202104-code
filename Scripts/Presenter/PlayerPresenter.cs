using System;
using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.UseCase;
using kameffee.unity1week202104.View;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;
using VContainer.Unity;

namespace kameffee.unity1week202104.Presenter
{
    public class PlayerPresenter : IPostInitializable, IFixedTickable, ITickable, IDisposable
    {
        private readonly IPlayerView view;
        private readonly PlayerAnimation animation;
        private readonly IPlayerInput input;
        private readonly PlayerModel model;
        private readonly AirBombeModel airBombeModel;
        private readonly BaseCampModel baseCampModel;
        private readonly FieldItemContainer fieldItemContainer;

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        public PlayerPresenter(
            IPlayerView view,
            PlayerAnimation animation,
            IPlayerInput input,
            PlayerModel model,
            AirBombeModel airBombeModel,
            BaseCampModel baseCampModel, 
            FieldItemContainer fieldItemContainer)
        {
            this.view = view;
            this.animation = animation;
            this.input = input;
            this.model = model;
            this.airBombeModel = airBombeModel;
            this.baseCampModel = baseCampModel;
            this.fieldItemContainer = fieldItemContainer;
        }

        public void PostInitialize()
        {
            // 酸素がなくなった時
            airBombeModel.OnEmpty
                .Subscribe(_ => model.Dead())
                .AddTo(disposable);

            // アイテム
            view.OnEnterItem
                .Subscribe(item => model.PickupItem(fieldItemContainer.GetItem(item.ItemSettings.ItemId)))
                .AddTo(disposable);

            // ジャンプ
            model.OnJump
                .Subscribe(_ =>
                {
                    view.Jump();
                    animation.Jump();
                });

            // 地面
            view.IsGround.Subscribe(model.SetOnGround);
            view.IsGround.Subscribe(isGround => animation.SetGround(isGround));

            // ベースキャンプ
            view.OnEnterBaseCamp
                .Subscribe(async _ => await baseCampModel.Supply(airBombeModel))
                .AddTo(disposable);

            view.OnExitBaseCamp
                .Subscribe(_ => model.OnExitBaseCampFocus(baseCampModel))
                .AddTo(disposable);

            model.OnAction
                .Subscribe(async _ =>
                {
                    if (model.FocusItem.Value != null)
                    {
                        // ゲット処理
                    }
                    else if (model.FocusBaseCamp.Value != null)
                    {
                        await model.FocusBaseCamp.Value.Supply(model.AirBombe);
                    }
                });

            model.OnMove
                .Subscribe(vector2 =>
                {
                    if (vector2.x > 0.01f || vector2.x < -0.01f)
                        animation.Walk(true);
                    else
                        animation.Walk(false);

                    if (vector2.x > 0.01f) animation.Right();
                    if (vector2.x < -0.01f) animation.Left();
                })
                .AddTo(disposable);

            // 歩行中が続いてしまうため
            model.Active
                .Where(active => active == false)
                .Subscribe(active => animation.Walk(false))
                .AddTo(disposable);
            
            // 復帰時
            model.OnRespawn
                .Subscribe(pos => view.SetPosition(pos))
                .AddTo(disposable);

            // ライト
            model.LightActive
                .Subscribe(isActive => view.SetActiveLight(isActive))
                .AddTo(disposable);

            view.OnLightArea
                .Subscribe(onArea => model.SetLight(onArea))
                .AddTo(disposable);
        }

        public void Tick()
        {
            if (model.Active.Value)
            {
                if (input.GetAction())
                {
                    // 拾う
                    if (model.HasFocusItem)
                    {
                        model.PickupItem(model.FocusItem.Value);
                    }
                }
            }

            if (model.UseAirBomb.Value)
            {
                // 1秒で1減らす
                airBombeModel.RemoveAir(1 * Time.deltaTime);
            }

            if (input.Jump())
            {
                model.Jump();
            }
        }

        public void FixedTick()
        {
            if (!model.Active.Value) return;

            var horizontal = input.GetHorizontal();
            var vertical = input.GetVertical();
            var vector = new Vector2(horizontal, vertical);
            model.Move(vector);
            view.Move(vector);
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}