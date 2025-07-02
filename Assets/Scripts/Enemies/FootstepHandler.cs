using UnityEngine;

public class FootstepHandler : MonoBehaviour
{
    [Header("Som de passo")]
    public AudioSource footstepSource;
    public AudioClip[] footstepClips;

    public void PlayFootstep()
    {
        if (footstepSource != null && footstepClips.Length > 0)
        {
            // Toca um som aleatório da lista de passos
            int index = Random.Range(0, footstepClips.Length);
            footstepSource.PlayOneShot(footstepClips[index]);
        }
    }
}
