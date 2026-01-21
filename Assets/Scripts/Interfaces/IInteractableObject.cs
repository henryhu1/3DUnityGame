public interface IInteractableObject
{
    void OnHoverEnter();
    void OnHoverExit();
    void OnInteract(PlayerInteractHandler interactor);
}
