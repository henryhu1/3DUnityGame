using UnityEngine;

public class Door : MonoBehaviour, IInterableObject
{
    [Header("Model")]
    [SerializeField] private Collider mainCollider;
    private Animator anim;

    [Header("Audio")]
    private AudioSource aud;
    [SerializeField] private AudioClip openAudio;
    [SerializeField] private AudioClip closeAudio;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        aud = gameObject.GetComponent<AudioSource>();
    }

    private void OpenDoor()
    {
        mainCollider.enabled = false;

        anim.SetBool("open", true);

        if (aud != null)
            aud.clip = openAudio;
            aud.Play();
    }

    private void CloseDoor()
    {
        mainCollider.enabled = true;

        anim.SetBool("open", false);

        if (aud != null)
            aud.clip = closeAudio;
            aud.Play();
    }

    public void OnInteractHover()
    {
    }

    public void OnInteractExit()
    {
    }

    public void OnInteract()
    {
        OpenDoor();
    }

    public void Highlight(bool isOn)
    {
    }
}
