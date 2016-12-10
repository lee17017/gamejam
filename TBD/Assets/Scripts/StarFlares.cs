using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFlares : MonoBehaviour {

    public GameObject[] starFabs;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 1000; i++)
        {
            GameObject g = (GameObject)Instantiate(starFabs[Random.Range(0, 3)], Random.onUnitSphere * 1000, Quaternion.identity);
            g.GetComponent<LensFlare>().brightness = Random.Range(0.01f, 0.8f);
            g.GetComponent<LensFlare>().color = Random.ColorHSV();
        }
        for (int i = 0; i < 2000; i++)
        {
            GameObject g = (GameObject)Instantiate(starFabs[Random.Range(0, 3)], Random.onUnitSphere * 200, Quaternion.identity);
            g.GetComponent<LensFlare>().brightness = Random.Range(0.01f, 0.8f);
            g.GetComponent<LensFlare>().color = Random.ColorHSV();
        }
    }

}
