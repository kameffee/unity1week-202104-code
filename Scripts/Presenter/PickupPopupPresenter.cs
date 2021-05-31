using System;
using Cysharp.Threading.Tasks;
using kameffee.unity1week202104.View;
using UnityEngine;

namespace kameffee.unity1week202104.Presenter
{
    public class PickupPopupPresenter
    {
        private readonly Func<Vector3, IPickupPopupView> factory;
        private readonly Camera targetCamera;

        private IPickupPopupView view;

        public PickupPopupPresenter(Func<Vector3, IPickupPopupView> factory, Camera camera)
        {
            this.factory = factory;
            this.targetCamera = camera;
        }

        public async UniTask OpenPopup(Vector3 position)
        {
            if (view == null)
            {
                view = factory.Invoke(position);
                view.Initialize(targetCamera);
            }
            await view.Open();
        }

        public async UniTask ClosePopup()
        {
            if (view != null)
            {
                await view.Close();
            }
        }
    }
}