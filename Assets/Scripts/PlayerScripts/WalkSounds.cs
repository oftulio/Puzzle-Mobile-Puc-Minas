using UnityEngine;

public class WalkSounds : MonoBehaviour
{
    public AudioSource footstepAudio;
    public AudioClip[] footstepClips; // Vários sons para variar

    public void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            int index = Random.Range(0, footstepClips.Length);
            footstepAudio.PlayOneShot(footstepClips[index]);
        }
        else if (!footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }
    }
}
