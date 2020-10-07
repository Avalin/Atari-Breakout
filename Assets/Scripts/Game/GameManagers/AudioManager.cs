using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region Audioclips
    private static AudioClip blob;
    private static AudioClip blobPitched;
    private static AudioClip sadTrombone;
    private static AudioClip kaChing;
    private static AudioClip hit;
    #endregion
    //Private
    private static AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        LoadAudio();
    }

    void LoadAudio()
    {
        blob = Resources.Load<AudioClip>("Sound/SFX/blob");
        blobPitched = Resources.Load<AudioClip>("Sound/SFX/blob_pitched");
        sadTrombone = Resources.Load<AudioClip>("Sound/SFX/sad_trombone");
        kaChing = Resources.Load<AudioClip>("Sound/SFX/ka-ching");
        hit = Resources.Load<AudioClip>("Sound/SFX/hit");
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "Blob":
                audioSource.PlayOneShot(blob);
                break;

            case "Paddle Blob":
                audioSource.PlayOneShot(blobPitched);
                break;

            case "Sad Trombone":
                audioSource.PlayOneShot(sadTrombone);
                break;

            case "Ka-ching!":
                audioSource.PlayOneShot(kaChing);
                break;

            case "Hit":
                if (audioSource.isPlaying) return;
                audioSource.PlayOneShot(hit);
                break;
        }
    }
}
