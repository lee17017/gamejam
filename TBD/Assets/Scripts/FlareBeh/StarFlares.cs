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
        Vector3 pos;
        for (int i = 0; i < 350; i++)
        {
            pos = Random.insideUnitSphere * 2000;
            pos.y = -Mathf.Abs(pos.y);
            GameObject g = (GameObject)Instantiate(starFabs[Random.Range(0, 3)], pos, Quaternion.identity);
            g.transform.SetParent(parent.transform);
            g.GetComponent<LensFlare>().brightness = Random.Range(0.01f, 0.8f);
            g.GetComponent<LensFlare>().color = Random.ColorHSV();
        }
        for (int i = 0; i < 150; i++)
        {
            pos = Random.insideUnitSphere * 200;
            pos.y = -Mathf.Abs(pos.y);
            GameObject g = (GameObject)Instantiate(starFabs[Random.Range(0, 4)], pos, Quaternion.identity);
            g.transform.SetParent(parent.transform);
            g.GetComponent<LensFlare>().brightness = Random.Range(0.01f, 0.8f);
            g.GetComponent<LensFlare>().color = Random.ColorHSV();
        }
    }

}
