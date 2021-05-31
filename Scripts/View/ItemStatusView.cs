using TMPro;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public class ItemStatusView : MonoBehaviour, IItemStatusView
    {
        [SerializeField]
        private TextMeshProUGUI numText;

        public void RenderOwn(int num, int max)
        {
            numText.SetText($"{num}/{max}");
        }
    }
}