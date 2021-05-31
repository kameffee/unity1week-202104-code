using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.Utility;
using kameffee.unity1week202104.View;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace kameffee.unity1week202104.UseCase
{
    public class GameCycle : IPostInitializable, IAsyncStartable, IDisposable
    {
        private readonly IGoalPoint goalPoint;
        private readonly PlayerModel playerModel;
        private readonly PlayableList playableList;
        private readonly FadeModel fadeModel;
        private readonly GameOverPerformer gameOverPerformer;
        private readonly RetryUseCase retryUseCase;
        private readonly CinemaScopeModel cinemaScopeModel;

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        public GameCycle(PlayerModel playerModel, IGoalPoint goalPoint,
            CinemaScopeModel cinemaScopeModel,
            PlayableList playableList,
            FadeModel fadeModel,
            GameOverPerformer gameOverPerformer, 
            RetryUseCase retryUseCase)
        {
            this.playerModel = playerModel;
            this.goalPoint = goalPoint;
            this.playableList = playableList;
            this.fadeModel = fadeModel;
            this.gameOverPerformer = gameOverPerformer;
            this.retryUseCase = retryUseCase;
            this.cinemaScopeModel = cinemaScopeModel;
        }

        public void PostInitialize()
        {
            playerModel.SetActive(false);

            goalPoint.OnGoal
                .Where(_ => playerModel.ItemPouch.ItemCount.Value >= 5)
                .Subscribe(async _ => await OnGoal())
                .AddTo(disposable);

            playerModel.OnDead
                .Subscribe(async _ => await OnGameOver())
                .AddTo(disposable);
        }

        private async UniTask OnGameOver()
        {
            Debug.Log("On Dead");
            // プレイヤー操作を無効
            playerModel.SetActive(false);

            // Dead 演出
            await gameOverPerformer.Run();

            // リトライ処理
            await retryUseCase.Run();
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0f), cancellationToken: cancellation);

            if (fadeModel.IsOut.Value)
            {
                await fadeModel.FadeIn();
            }

            // await cinemaScopeModel.Show();

            // await playableList.Intro.PlayAsync();

            // await cinemaScopeModel.Hide();

            playerModel.SetActive(true);
            playerModel.SetUseAirBomb(true);
        }

        private async UniTask OnGoal()
        {
            Debug.Log("Goal");
            playerModel.SetActive(false);
            playerModel.SetUseAirBomb(false);

            await cinemaScopeModel.Show();

            await playableList.Outro.PlayAsync();

            // フェードアウト
            fadeModel.SetState(true);
            await cinemaScopeModel.Hide(2);

            await SceneManager.LoadSceneAsync("Title");
        }

        public void Dispose()
        {
            playerModel?.Dispose();
            disposable?.Dispose();
        }
    }
}