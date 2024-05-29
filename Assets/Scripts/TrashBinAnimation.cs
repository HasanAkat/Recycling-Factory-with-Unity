using UnityEngine;

public class TrashBinAnimation : MonoBehaviour
{
    private Animator animator;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager> ();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayOpenCloseAnimation()
    {
        // Animator Controller'da tanýmlý olan animasyonu oynat
        audioManager.PlaySFX(audioManager.trashBin);
        animator.SetTrigger("isOpen");
    }
}
