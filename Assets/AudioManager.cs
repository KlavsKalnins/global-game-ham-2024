using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    private void Start()
    {
        
    }
    // Use this method to play a specific audio clip on a specific AudioSource
    public void PlayAudio(AudioClip clip, int audioSourceIndex)
    {
        if (audioSourceIndex >= 0 && audioSourceIndex < audioSources.Length)
        {
            audioSources[audioSourceIndex].clip = clip;
            audioSources[audioSourceIndex].Play();
        }
        else
        {
            Debug.LogError("Invalid audio source index");
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
