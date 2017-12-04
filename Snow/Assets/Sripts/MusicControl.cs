using System.Collections;
using UnityEngine;

public class MusicControl : MonoBehaviour {

    public AudioClip SongOne;
    public AudioClip SongTwo;
    public AudioClip SongThree;

    private AudioSource audioSource;
    private float time = 0;
    private float timePlayed = 0;
    private int lastPlayed = 1;

    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        PlaySongOne();
    }
	
	public void PlaySongOne()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(SongOne);
        timePlayed += 169.968f;
    }

    public void PlaySongTwo()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(SongTwo);
        timePlayed += 265.848f;
    }

    public void PlaySongThree()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(SongThree);
        timePlayed += 91.992f;
    }


    void Update () {
        time += Time.deltaTime;
        
        if (lastPlayed == 1 && time >= timePlayed)
        {
            PlaySongTwo();
            lastPlayed = 2;
        }
        if (lastPlayed == 2 && time >= timePlayed)
        {
            PlaySongThree();
            lastPlayed = 3;
        }
        if (lastPlayed == 3 && time >= timePlayed)
        {
            PlaySongOne();
            lastPlayed = 1;
        }
    }
}
