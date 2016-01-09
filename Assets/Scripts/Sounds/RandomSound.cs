using UnityEngine;
using System.Collections.Generic;

public class RandomSound : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> sounds;

    [SerializeField]
    private AudioSource audioSource;

    private int lastNumberPlayed;

    public void PlayRandomSound() {

        //select a random number in the list
        int numberToPlay = Mathf.FloorToInt(sounds.Count * Random.Range(0, 0.99f));

        //if the current number is the same as the last number, pick a new number in the list.
        while (numberToPlay == lastNumberPlayed) {
            numberToPlay = Mathf.FloorToInt(sounds.Count * Random.Range(0, 0.99f));
        }

        //save the number in last numberplayed, to check we wont play this number again next time.
        lastNumberPlayed = numberToPlay;

        //assing the sound in the AudioSource.
        audioSource.clip = sounds[numberToPlay];

        //play the assigned sound.
        audioSource.Play();
    }
}
