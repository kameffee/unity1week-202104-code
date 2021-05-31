namespace kameffee.unity1week202104.View
{
    /// <summary>
    /// ベースキャンプ利用者
    /// </summary>
    public interface IBaseCampConsumer
    {
        void OnEnterBaseCamp(IBaseCampView baseCampView);

        void OnExitBaseCamp(IBaseCampView baseCampView);
    }
}