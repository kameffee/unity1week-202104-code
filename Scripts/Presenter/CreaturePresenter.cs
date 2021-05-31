using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace kameffee.unity1week202104.Presenter
{
    public class CreaturePresenter : MonoBehaviour
    {
        private CreatureModel model;

        [SerializeField]
        private CreatureView view;

        [Inject]
        public void Setup(CreatureModel model)
        {
            Debug.Log("Creature Setup");
            this.model = model;
            
            view.Animation.Idle();
        }
    }
}