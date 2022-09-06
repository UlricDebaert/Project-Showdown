
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Music[] musics;
    Music lastPlayedMusic;

    bool canPlayMusic;
    float defaultVolume;

    AudioSource audioSource;

    private void Start()
    {
        PauseMenu.musicOn = true;
        audioSource = GetComponent<AudioSource>();
        canPlayMusic = true;
        lastPlayedMusic = null;
        defaultVolume = audioSource.volume;
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

        if(!PauseMenu.musicOn)
        {
            audioSource.volume = 0.0f;
        }
        else
        {
            audioSource.volume = defaultVolume;
        }
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
