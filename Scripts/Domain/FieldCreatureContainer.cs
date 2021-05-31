using System.Collections.Generic;
using kameffee.unity1week202104.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace kameffee.unity1week202104.Domain
{
    public class FieldCreatureContainer : IPostStartable
    {
        private List<CreaturePresenter> container;

        [Inject]
        public FieldCreatureContainer(IReadOnlyList<CreaturePresenter> list)
        {
            container = new List<CreaturePresenter>(list);
        }

        public void PostStart()
        {
            Debug.Log("containercount: " + container.Count);
        }
    }
}