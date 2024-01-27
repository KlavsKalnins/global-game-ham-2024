using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField] KeyCode keycode = KeyCode.N;
    [SerializeField] AudioSource audioSource;
    void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            audioSource.Play();
        }  
    }
}
