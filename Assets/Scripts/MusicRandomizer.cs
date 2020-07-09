using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRandomizer : MonoBehaviour
{

    public AudioClip[] tracks;


    // Start is called before the first frame update
    void Start()
    {
        int trackToPlay = UnityEngine.Random.Range(0,tracks.Length-1);

        //audio = GetComponent<AudioSource>();
        ScenesManager.instance.changeMusic(tracks[trackToPlay]);
        ScenesManager.instance.gameMusic.Play();

    }
}
