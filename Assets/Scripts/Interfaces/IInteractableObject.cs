public interface IInteractableObject
{
    void OnHoverEnter();
    void OnHoverExit();
    void OnInteract();
    bool CanInteract(PlayerInteractionState state);
}
