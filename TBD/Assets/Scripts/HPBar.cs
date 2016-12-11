using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour {



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale.Set(1f,
            ((float)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hitpoints) / 100f
            , 1f);
            
    }
}
