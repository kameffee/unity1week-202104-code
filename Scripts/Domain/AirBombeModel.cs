using System;
using UniRx;
using UnityEngine;

namespace kameffee.unity1week202104.Domain
{
    /// <summary>
    /// 酸素ボンベ
    /// </summary>
    public class AirBombeModel : IDisposable
    {
        public static readonly float MaxAir = 120f;

        /// 空気量
        public IReadOnlyReactiveProperty<float> Air => air;
        private readonly ReactiveProperty<float> air;

        /// 空になった通知
        public IObservable<Unit> OnEmpty => Air.Where(air => air <= 0f).AsUnitObservable();

        public AirBombeModel(float air = 0)
        {
            this.air = new ReactiveProperty<float>(air);
        }

        public void AddAir(float amount)
        {
            air.Value += amount;
        }

        public void RemoveAir(float amount)
        {
            air.Value = Mathf.Max(air.Value - amount, 0f);
        }

        public void SetAir(float value)
        {
            air.Value = value;
        }

        public void Dispose()
        {
            air?.Dispose();
        }
    }
}