using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {
	float amount = 0.0f;
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Random.rotation, amount * 100000f);
		amount *= 0.7f;
	}

	public void ShakeIt() {
		amount = 1.0f;
	}
}
