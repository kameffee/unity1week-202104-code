using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace kameffee.unity1week202104.Domain
{
    public class TitleModel
    {
        private readonly FadeModel fadeModel;

        public TitleModel(FadeModel fadeModel)
        {
            this.fadeModel = fadeModel;
        }

        public async UniTask IntroPerformer()
        {
            // イントロ演出
            await fadeModel.FadeOut(0);
            await fadeModel.FadeIn(3);
        }

        public async UniTask StartGame()
        {
            await fadeModel.FadeOut();
            await SceneManager.LoadSceneAsync("Main");
        }
    }
}