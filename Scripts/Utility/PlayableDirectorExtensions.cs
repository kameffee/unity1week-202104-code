using Cysharp.Threading.Tasks;
using UnityEngine.Playables;

namespace kameffee.unity1week202104.Utility
{
    public static class PlayableDirectorExtensions
    {
        public static UniTask PlayAsync(this PlayableDirector self)
        {
            self.Play();
            return UniTask.WaitWhile(() => self.state == PlayState.Playing);
        }
    }
}