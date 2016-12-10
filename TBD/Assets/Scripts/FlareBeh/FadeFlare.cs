using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFlare : MonoBehaviour {

    private LensFlare flare;

	// Use this for initialization
	void Start () {
        flare = GetComponent<LensFlare>();
	}
	
	// Update is called once per frame
	void Update () {
        flare.brightness -= 0.01f;
        if (flare.brightness<0.02f)
        {
            Destroy(gameObject);
        }
	}
}
