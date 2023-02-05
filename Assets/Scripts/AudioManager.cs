using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> clipList = new List<AudioClip>();

    private AudioSource bgmAudioSource;
    private AudioSource soundAudioSource;
    static public AudioManager instance;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;
        soundAudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBgm(string name)
    {
        bgmAudioSource.clip = clipList.Find((x) => x.name == name);
        bgmAudioSource.Play();
    }

    public void PlaySound(string name)
    {
        soundAudioSource.clip = clipList.Find((x) => x.name == name);
        soundAudioSource.Play();
    }
}
