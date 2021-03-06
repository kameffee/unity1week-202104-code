using Cysharp.Threading.Tasks;

namespace kameffee.unity1week202104.View
{
    public interface IFadeView
    {
        void Initialize(bool isOut);

        // 画面を隠す
        UniTask FadeOut(float duration = 1f);

        // 画面を表示
        UniTask FadeIn(float duration = 1f);
    }
}