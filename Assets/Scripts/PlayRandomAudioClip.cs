using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomAudioClip : MonoBehaviour
{
    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        if (clips.Length > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}
