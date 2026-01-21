using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite reticleSprite;
    [SerializeField] private Sprite highlightedReticleSprite;

    [Header("UI")]
    [SerializeField] private Image reticleImage;

    private void Start()
    {
        reticleImage.sprite = reticleSprite;
    }

    private void ChangeToHighlighted()
    {
        reticleImage.sprite = highlightedReticleSprite;
    }

    private void ChangeToNormal()
    {
        reticleImage.sprite = reticleSprite;
    }
}
