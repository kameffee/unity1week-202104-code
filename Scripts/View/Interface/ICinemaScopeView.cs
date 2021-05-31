using Cysharp.Threading.Tasks;

namespace kameffee.unity1week202104.View
{
    public interface ICinemaScopeView
    {
        UniTask Show(float duration);

        UniTask Hide(float duration);
    }
}