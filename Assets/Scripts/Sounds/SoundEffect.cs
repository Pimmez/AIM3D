using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {

    [SerializeField]
    private string musicName;

    private AudioSource _music;

    // Use this for initialization
    void Start()
    {
        _music = (AudioSource)gameObject.AddComponent<AudioSource>();

        //load the sound
        _music.clip = LoadSound(musicName);
        //playing the music
        PlayMusic(_music);
    }

    private AudioClip LoadSound(string fileName)
    {
        AudioClip clip = (AudioClip)Resources.Load("Audio/" + fileName);
        return clip;
    }


    private void PlayMusic(AudioSource sound)
    {
        sound.loop = true;
        sound.Play();
    }

    private void StopMusic(AudioSource sound)
    {
        sound.Stop();
    }
}
