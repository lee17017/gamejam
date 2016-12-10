using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "bullet")
            Debug.Log("AAAHAADSFASDHFAHSDHFDSH");  
    }
}
