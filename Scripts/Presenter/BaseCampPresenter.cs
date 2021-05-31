using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.View;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace kameffee.unity1week202104.Presenter
{
    public class BaseCampPresenter : MonoBehaviour
    {
        [SerializeField]
        private BaseCampView view;

        private BaseCampModel model;
        private AirBombeModel airBombeModel;

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        [Inject]
        public void Setup(BaseCampModel model, AirBombeModel airModel)
        {
            Debug.Log("Setup");
            this.model = model;
            this.airBombeModel = airModel;
        }

        private void Start()
        {
            this.view.OnAction()
                .Subscribe(async _ => await Supply())
                .AddTo(disposable);

            view.OnEnterArea()
                .Subscribe(consumer => consumer.OnEnterBaseCamp(view))
                .AddTo(disposable);

            view.OnExitArea()
                .Subscribe(consumer => consumer.OnExitBaseCamp(view))
                .AddTo(disposable);
        }

        public async UniTask Supply()
        {
            Debug.Log("Supply");
            CancellationTokenSource token = new CancellationTokenSource();
            await model.Supply(airBombeModel, token.Token);
        }
    }
}