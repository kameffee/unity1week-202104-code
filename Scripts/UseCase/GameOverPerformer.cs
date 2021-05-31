using System;
using Cysharp.Threading.Tasks;
using kameffee.unity1week202104.View;

namespace kameffee.unity1week202104.UseCase
{
    /// <summary>
    /// Dead演出
    /// </summary>
    public class GameOverPerformer
    {
        private readonly GameOverCanvas gameOverCanvas;

        public GameOverPerformer(GameOverCanvas gameOverCanvas)
        {
            this.gameOverCanvas = gameOverCanvas;
        }

        public async UniTask Run()
        {
            await gameOverCanvas.Show();

            await UniTask.Delay(TimeSpan.FromSeconds(5));

            await gameOverCanvas.Hide();
        }
    }
}