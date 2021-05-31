using UnityEngine;
using UnityEngine.UI;

namespace kameffee.unity1week202104.View
{
    public class AirBombStatusView : MonoBehaviour, IAirBombStatusView
    {
        [SerializeField]
        private Slider slider;

        public void Render(float value)
        {
            slider.value = value;
        }
    }
}