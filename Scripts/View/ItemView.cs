using System;
using Cysharp.Threading.Tasks;
using kameffee.unity1week202104.Domain;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace kameffee.unity1week202104.View
{
    [Serializable]
    public class ItemSettings
    {
        [SerializeField]
        private int itemId;

        public int ItemId => itemId;

        public override string ToString()
        {
            return $"{nameof(ItemId)}: {ItemId}";
        }
    }

    public class ItemView : MonoBehaviour, IItemView
    {
        [SerializeField]
        private ItemSettings itemSettings;
        public ItemSettings ItemSettings => itemSettings;

        [SerializeField]
        private ParticleSystem pickupEffect;
        
        public Vector3 Position => transform.position;

        public IObservable<Unit> OnFocus =>
            this.OnTriggerEnter2DAsObservable()
                .Where(collider => collider.CompareTag("Player"))
                .AsUnitObservable();
        
        public IObservable<Unit> OnUnFocus
            => this.OnTriggerExit2DAsObservable()
                .Where(collider => collider.CompareTag("Player"))
                .AsUnitObservable();

        public async UniTask PlayEffect()
        {
            pickupEffect.Play();
            await UniTask.WaitUntil(() => pickupEffect.isStopped);
        }

        public void Delete()
        {
            Destroy(gameObject);
        }
    }
}