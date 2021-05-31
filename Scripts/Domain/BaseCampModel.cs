using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace kameffee.unity1week202104.Domain
{
    public class BaseCampModel : IDisposable
    {
        public int BaseCampId { get; set; }

        public BaseCampModel()
        {
        }

        /// <summary>
        /// 補給の一連処理
        /// </summary>
        public async UniTask Supply(AirBombeModel airBombeModel, CancellationToken token = default)
        {
            // 酸素の補給
            var from = airBombeModel.Air.Value;
            var to = AirBombeModel.MaxAir;
            await DOVirtual.Float(from, to, 3f, value =>
            {
                airBombeModel.SetAir(value);
            }).SetEase(Ease.InOutSine).WithCancellation(token);
        }

        public void Dispose()
        {
        }
    }
}