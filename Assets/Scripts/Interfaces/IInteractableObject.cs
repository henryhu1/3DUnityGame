public interface IInteractableObject
{
    void OnInteractHover();
    void OnInteractExit();
    void OnInteract();
    void Highlight(bool isHighlighted);
}
