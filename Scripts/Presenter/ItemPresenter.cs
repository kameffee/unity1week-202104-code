using System;
using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.View;
using naichilab.EasySoundPlayer.Scripts;
using UniRx;
using UnityEngine;
using VContainer;

namespace kameffee.unity1week202104.Presenter
{
    public class ItemPresenter : MonoBehaviour
    {
        [SerializeField]
        private ItemView view;

        private ItemModel model;
        private PickupPopup pickupPopup;

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        [Inject]
        public void Setup(ItemModel itemModel, PickupPopup pickupPopup)
        {
            Debug.Log("ItemPresenter Setup");
            this.model = itemModel;
            this.pickupPopup = pickupPopup;
        }

        public void Start()
        {
            Debug.Log("ItemPresenter Start");
            model.Position = view.Position;
            model.Initialize(view.ItemSettings);

            // Focus
            view.OnFocus.Subscribe(_ => model.Focus(true));
            view.OnUnFocus.Subscribe(_ => model.Focus(false));

            // フォーカスされた時
            model.OnFocus
                .SkipLatestValueOnSubscribe()
                .Subscribe(async isFocus =>
                {
                    if (isFocus) await pickupPopup.OpenPopup(view.Position + Vector3.up * 1, model.ItemSettings);
                    else await pickupPopup.ClosePopup();
                }).AddTo(disposable);

            // 拾われた
            model.OnPickup
                .Subscribe(async _ =>
                {
                    Debug.Log("Pickup");
                    Dispose();
                    SePlayer.Instance?.Play(0);
                    await view.PlayEffect();
                    view.Delete();
                    await pickupPopup.ClosePopup();
                })
                .AddTo(disposable);
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}