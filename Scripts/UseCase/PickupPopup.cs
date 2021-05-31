using System;
using Cysharp.Threading.Tasks;
using kameffee.unity1week202104.Presenter;
using kameffee.unity1week202104.View;
using UnityEngine;

namespace kameffee.unity1week202104
{
    public class PickupPopup
    {
        private readonly PickupPopupPresenter popupPresenter;

        public PickupPopup(PickupPopupPresenter popupPresenter)
        {
            this.popupPresenter = popupPresenter;
        }

        public async UniTask OpenPopup(Vector3 position, ItemSettings settings)
        {
            Debug.Log("OpenPopup : " + settings);
            await popupPresenter.OpenPopup(position);
        }

        public async UniTask ClosePopup()
        {
            await popupPresenter.ClosePopup();
        }
    }
}