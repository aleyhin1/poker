public interface IMoves
{
    void Fold(PlayerController playerController);
    void Call(PlayerController playerController);
    void Bob(PlayerController playerController);
    void Raise(PlayerController playerController, float raiseAmount);
    void Rest(PlayerController playerController, float restAmount);
}