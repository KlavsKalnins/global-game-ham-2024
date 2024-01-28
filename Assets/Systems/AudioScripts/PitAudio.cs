using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PitAudio : MonoBehaviour
{
    public static PitAudio Instance;
    [SerializeField] List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] AudioClip audioClip;

    private void Awake()
    {
        Instance = this;
        Setup(15);
    }

    public void Setup(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.clip = audioClip;
            a.playOnAwake = false;
            a.loop = false;
            audioSources.Add(a);
        }
    }

    public void PlayAudio()
    {
        audioSources.Where(a => !a.isPlaying).FirstOrDefault().Play();
    }
}