using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Action : NetworkBehaviour {

    public Player player;

    public virtual void Move()
    {
        return;
    }

    public virtual void reset()
    {
        return;
    }

}
