using System.Collections.Generic;
using UnityEngine;

public class BulletsAudio : MonoBehaviour
{
    public static BulletsAudio Instance;
    [SerializeField] List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] AudioClip audioClip;

    private void Awake()
    {
        Instance = this;
    }

    public void SetupBulletAudio(int bulletCount)
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

    public void PlayBulletAudio(int index)
    {
        audioSources[index].Play();
    }
}