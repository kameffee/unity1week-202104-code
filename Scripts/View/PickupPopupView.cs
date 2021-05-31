using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public class PickupPopupView : MonoBehaviour, IPickupPopupView
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        private Vector3 defaultPosition;

        private void Awake()
        {
            canvasGroup.DOFade(0, 0f);
        }

        public void Initialize(Camera targetCamera)
        {
            GetComponent<Canvas>().worldCamera = targetCamera;
            defaultPosition = transform.position;
        }

        public async UniTask Open()
        {
            canvasGroup.DOFade(1, 0.3f);
        }

        public async UniTask Close()
        {
            canvasGroup.DOFade(0, 0.3f);
        }
    }
}