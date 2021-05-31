using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public class GameOverCanvas : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        private void Start()
        {
            Initialize(false);
        }

        public void Initialize(bool isShow)
        {
            if (isShow)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1;
            }
            else
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0;
            }
        }
        
        public async UniTask Show(float duration = 1)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            await canvasGroup.DOFade(1, duration);
        }

        public async UniTask Hide(float duration = 1)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            await canvasGroup.DOFade(0, duration);
        }
    }
}