using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFlares : MonoBehaviour {

    public GameObject[] starFabs;
    public GameObject empty;
	// Use this for initialization
	void Start ()
    {
        GameObject parent = (GameObject)Instantiate(empty);
        for (int i = 0; i < 350; i++)
        {
            GameObject g = (GameObject)Instantiate(starFabs[Random.Range(0, 3)], Random.onUnitSphere * 1000, Quaternion.identity);
            g.transform.SetParent(parent.transform);
            g.GetComponent<LensFlare>().brightness = Random.Range(0.01f, 0.8f);
            g.GetComponent<LensFlare>().color = Random.ColorHSV();
        }
        for (int i = 0; i < 150; i++)
        {
            GameObject g = (GameObject)Instantiate(starFabs[Random.Range(0, 3)], Random.onUnitSphere * 200, Quaternion.identity);
            g.transform.SetParent(parent.transform);
            g.GetComponent<LensFlare>().brightness = Random.Range(0.01f, 0.8f);
            g.GetComponent<LensFlare>().color = Random.ColorHSV();
        }
    }

}
