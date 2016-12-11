using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiceover : MonoBehaviour {

    public AudioSource aud;
    public AudioClip newmusic;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        StartCoroutine(soundswitch());
	}

    IEnumerator soundswitch()
    {
        yield return new WaitForSeconds(18.8f);
        aud.clip = newmusic;
        aud.Stop();
        aud.Play();
    }
}
