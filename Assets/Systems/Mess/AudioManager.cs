using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    private float bpm = 173f;
    private Coroutine audioTransitionCoroutine;

    [SerializeField] float customWait = 4;
    private void Start()
    {
        StartCoroutine(SingTrack1()); // sync
    }

    IEnumerator SingTrack1()
    {
        
        yield return new WaitForSeconds(customWait);
        Debug.Log("KK: audio should play");
        PlayAudio(audioSources[1].clip, 1, true);
    }

    // it should start every 1 or 3rd bar but we can make it optional by boolean
    // Use this method to play a specific audio clip on a specific AudioSource
    public void PlayAudio(AudioClip clip, int audioSourceIndex, bool syncWithBeat = true)
    {
        if (audioSourceIndex >= 0 && audioSourceIndex < audioSources.Length)
        {
            if (syncWithBeat)
            {
                if (audioTransitionCoroutine != null)
                {
                    StopCoroutine(audioTransitionCoroutine);
                }

                // Calculate the time to wait based on the specified BPM
                float waitTime = 60f / bpm * 4f;

                // Start the coroutine for smooth audio transition
                audioTransitionCoroutine = StartCoroutine(TransitionToNewClip(clip, audioSourceIndex, waitTime));
            }
            else
            {
                // Play the audio clip without synchronization
                audioSources[audioSourceIndex].clip = clip;
                audioSources[audioSourceIndex].Play();
            }
        }
        else
        {
            Debug.LogError("Invalid audio source index");
        }
    }

    private IEnumerator TransitionToNewClip(AudioClip newClip, int audioSourceIndex, float transitionTime)
    {
        // Wait for the specified time before starting the new audio clip
        yield return new WaitForSeconds(transitionTime);

        // Crossfade to the new audio clip over the specified transition time
        float currentTime = 0f;
        float startVolume = audioSources[audioSourceIndex].volume;

        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            audioSources[audioSourceIndex].volume = Mathf.Lerp(startVolume, 0f, currentTime / transitionTime);
            yield return null;
        }

        // Set the new audio clip and reset the volume
        audioSources[audioSourceIndex].clip = newClip;
        audioSources[audioSourceIndex].Play();

        currentTime = 0f;
        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            audioSources[audioSourceIndex].volume = Mathf.Lerp(0f, startVolume, currentTime / transitionTime);
            yield return null;
        }
    }

    // Use this method to fade in/out a specific AudioSource
    public void FadeAudio(int audioSourceIndex, float targetVolume, float fadeDuration)
    {
        if (audioSourceIndex >= 0 && audioSourceIndex < audioSources.Length)
        {
            StartCoroutine(FadeAudioCoroutine(audioSources[audioSourceIndex], targetVolume, fadeDuration));
        }
        else
        {
            Debug.LogError("Invalid audio source index");
        }
    }

    private System.Collections.IEnumerator FadeAudioCoroutine(AudioSource audioSource, float targetVolume, float fadeDuration)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}
