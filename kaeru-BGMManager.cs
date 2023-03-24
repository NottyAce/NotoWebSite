using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    public AudioSource intro;
    public AudioSource loop;

    AudioSource[] audioSource = new AudioSource[2];

	// Use this for initialization
	void Start () {
        //       audioSource[0] = gameObject.AddComponent<AudioSource>();
        //       audioSource[1] = gameObject.AddComponent<AudioSource>();

        //       if (intro != null)
        //       {

        //       }

        intro.PlayScheduled(AudioSettings.dspTime);
        loop.PlayScheduled(AudioSettings.dspTime + ((float)intro.clip.samples) / (float)intro.clip.frequency);
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
