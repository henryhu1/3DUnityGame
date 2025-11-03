public interface IPlayerComponentable
{
    void Initialize(PlayerManager manager, PlayerEventBus bus);
    void Uninitialize();
}
