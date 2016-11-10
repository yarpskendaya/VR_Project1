using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    // Audiosource to play in the active scene
    public AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        // initialization of audio source
        audioSource = GetComponentInParent<AudioSource>();
    }

    // Method to start and stop playback of the audio source, using the in-game music menu
    public void StartStop ()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        else
            audioSource.Play();
    }
}
