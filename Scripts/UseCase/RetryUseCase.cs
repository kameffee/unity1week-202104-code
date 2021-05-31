using Cysharp.Threading.Tasks;
using kameffee.unity1week202104.Domain;
using UnityEngine;

namespace kameffee.unity1week202104.UseCase
{
    public class RetryUseCase
    {
        private readonly PlayerModel playerModel;
        private readonly FadeModel fadeModel;

        public RetryUseCase(PlayerModel playerModel, FadeModel fadeModel)
        {
            this.playerModel = playerModel;
            this.fadeModel = fadeModel;
        }

        public async UniTask Run()
        {
            await fadeModel.FadeOut();

            // 消費しないように
            playerModel.SetUseAirBomb(false);
            // 満タンにする.
            playerModel.AirBombe.SetAir(AirBombeModel.MaxAir);

            // 初期位置に戻す
            playerModel.Respawn(new Vector3(-1.8f, 0.5f, 0));

            await fadeModel.FadeIn();
            
            // ボンベ有効化
            playerModel.SetUseAirBomb(true);
            playerModel.SetActive(true);
        }
    }
}