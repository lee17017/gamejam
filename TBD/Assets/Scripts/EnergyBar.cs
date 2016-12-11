﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale.Set(1f,
            ((float)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().energy) / 100f
            , 1f);
    }
}
