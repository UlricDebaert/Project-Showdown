
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Music[] musics;
    Music lastPlayedMusic;

    bool canPlayMusic;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canPlayMusic = true;
        lastPlayedMusic = null;
    }

    private void Update()
    {
        if(!audioSource.isPlaying && canPlayMusic)
        {
            canPlayMusic = false;
            SelectMusic();
            audioSource.Play();
        }

        if(!audioSource.isPlaying) canPlayMusic = true;
    }

    void SelectMusic()
    {
        int rand = Random.Range(0, musics.Length);
        //print("music lenght " + musics.Length);

        if(musics.Length > 1)
        {
            while (musics[rand] == lastPlayedMusic)
            {
                rand = Random.Range(0, musics.Length);
            }
        }

        audioSource.volume = musics[rand].musicVolume;
        audioSource.clip = musics[rand].musicClip;
        lastPlayedMusic = musics[rand];

    }
}
