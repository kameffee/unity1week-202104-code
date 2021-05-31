namespace kameffee.unity1week202104.View
{
    public interface IPlayerInput
    {
        float GetHorizontal();
        float GetVertical();
        bool GetAction();
        bool Jump();
    }
}