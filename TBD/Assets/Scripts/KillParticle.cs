using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour {
    
	void Update () {
        if (!this.gameObject.GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(gameObject);
        }

    }
}
