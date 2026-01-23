public interface IHoldable
{
    void OnPickUp(HoldContext context);
    void OnHoldUpdate(HoldContext context);
    void OnRelease(HoldContext context);
}
