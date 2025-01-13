using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip welcomeHomeTrack; // Assign the audio clip in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component if it doesn't already exist
        audioSource = gameObject.AddComponent<AudioSource>();

        // Assign the audio clip to the AudioSource
        audioSource.clip = welcomeHomeTrack;

        // Set the AudioSource to loop
        audioSource.loop = true;

        // Play the audio
        audioSource.Play();
    }
}