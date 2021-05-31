using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace kameffee.unity1week202104.View
{
    public class TitleView : MonoBehaviour, ITitleView
    {
        [SerializeField]
        private Button startButton;

        public IObservable<Unit> OnClickStart => startButton.OnClickAsObservable();
    }
}