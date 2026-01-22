using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private PlayerInteractHandler interactHandler;

    [Header("Sprites")]
    [SerializeField] private Sprite reticleSprite;
    [SerializeField] private Sprite highlightedReticleSprite;

    [Header("UI")]
    [SerializeField] private Image reticleImage;

    private bool isHighlighting;

    private void Start()
    {
        isHighlighting = false;
        reticleImage.sprite = reticleSprite;
    }

    private void OnEnable()
    {
        interactHandler.OnInteractableHover += ChangeReticle;
    }

    private void OnDisable()
    {
        interactHandler.OnInteractableHover -= ChangeReticle;
    }

    private void ChangeReticle(bool isHovering)
    {
        if (isHovering) ChangeToHighlighted();
        else ChangeToNormal();
    }

    private void ChangeToHighlighted()
    {
        if (isHighlighting) return;

        isHighlighting = true;
        reticleImage.sprite = highlightedReticleSprite;
    }

    private void ChangeToNormal()
    {
        if (!isHighlighting) return;

        isHighlighting = false;
        reticleImage.sprite = reticleSprite;
    }
}
