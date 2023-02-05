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
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;
        soundAudioSource = gameObject.AddComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {

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

    public void StopBgm()
    {
        bgmAudioSource.Stop();
    }

    public void PlaySound(string name)
    {
        soundAudioSource.Stop();
        soundAudioSource.clip = clipList.Find((x) => x.name == name);
        soundAudioSource.Play();
    }
}
