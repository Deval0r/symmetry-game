using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip welcomeHomeTrack; // Assign the audio clip in the Inspector
    public float musicVolume = 1.0f; // Volume for the background music
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component if it doesn't already exist
        audioSource = gameObject.AddComponent<AudioSource>();

        // Assign the audio clip to the AudioSource
        audioSource.clip = welcomeHomeTrack;

        // Set the AudioSource to loop
        audioSource.loop = true;

        // Set the initial volume
        audioSource.volume = musicVolume;

        // Play the audio
        audioSource.Play();
    }

    void Update()
    {
        // Update the volume in case it is changed in the Inspector
        audioSource.volume = musicVolume;
    }
}