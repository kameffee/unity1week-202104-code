using Cysharp.Threading.Tasks;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public interface IPickupPopupView
    {
        void Initialize(Camera targetCamera);

        UniTask Open();

        UniTask Close();
    }
}