using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Action {

    public float speed = 10.0f;
    public float rotSpeed = 100.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rot = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        transform.Translate(0, 0, move);
        transform.Rotate(0, rot, 0);
    }
}
