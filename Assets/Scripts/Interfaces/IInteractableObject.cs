public interface IInteractableObject
{
    void OnHoverEnter();
    void OnHoverExit();
    void OnInteract(InteractionContext ctx);
    bool CanInteract(PlayerInteractionState state);
}
