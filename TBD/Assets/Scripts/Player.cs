using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    
    public SpaceShip ship;
    public Action[] actions;
    public int state;
    public GameObject bulletPref;
    public GameObject asteroidPrefab;
    private float timeTillNewAsteroid;
    [SerializeField]
    private float timeTillNextCycle;
    private bool cycleWarning;
    public Texture textureCycleWarning;

    [SyncVar]
    public float energy;
    [SyncVar]
    public int hitpoints;
    public bool energyDown;

	// Use this for initialization
	void Start ()
    {
        ship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>();

        timeTillNextCycle = Random.Range(10, 10);
        timeTillNewAsteroid = Random.Range(5, 10);

        energy = 20;
        hitpoints = 100;
        
        if (isLocalPlayer)
        {
            ship.player = this;
            state = (GameObject.FindGameObjectsWithTag("Player").Length - 1 + 2) % 3;
            ship.cams[state].enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(energy <= 0)
        {
            energyDown = true;
            StartCoroutine(energyDowntime());
        }

        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdCycle();
        }
        actions[state].Move();
        
        if(isServer)
        {
            //Energy Reg 2/s
            gainEnergy(2f * Time.deltaTime);

            //Asteroid Spawns
            if(timeTillNewAsteroid <= 0)
            {
                Vector3 pos = Random.onUnitSphere;
                CmdSpawnAsteroid(pos);
                timeTillNewAsteroid = Random.Range(5, 10);
            }
            else
            {
                timeTillNewAsteroid -= Time.deltaTime;
            }

            //CYCLE
            if(timeTillNextCycle <= 0)
            {
                timeTillNextCycle = Random.Range(5, 10);
                StartCoroutine(cycle());
            }
            else
            {
                timeTillNextCycle -= Time.deltaTime;
            }
        }
	}

    void OnGUI()
    {
        float width = Screen.width;
        float height = Screen.height;
        GUI.Label(new Rect(width - 100, height - 50, 100, 25), "HP:\t" + hitpoints);
        if(energyDown)
            GUI.Label(new Rect(width - 100, height - 25, 100, 25), "Energy down!");
        else
            GUI.Label(new Rect(width - 100, height - 25, 100, 25), "Energy:\t" + (int)energy);

        if(cycleWarning)
        {
            GUI.DrawTexture(new Rect(width / 2 - 200, height / 2 - 200, 400, 400), textureCycleWarning);
        }
    }

    public IEnumerator cycle()
    {
        cycleWarning = true;
        yield return new WaitForSeconds(1.5f);
        cycleWarning = false;
        CmdCycle();
    }

    public IEnumerator energyDowntime()
    {
        yield return new WaitForSeconds(5f);
        energy = 20;
        energyDown = false;
    }

    public void takeDamage(int damage)
    {
        if(!isServer)
        {
            return;
        }
        CmdSetHP(hitpoints - damage);
    }

    public bool useEnergy(float energy)
    {
        if(this.energy < energy)
        {
            return false;
        }
        else
        {
            CmdSetEnergy(this.energy - energy);
            return true;
        }
    }

    public void gainEnergy(float energy)
    {
        CmdSetEnergy(this.energy + energy);
    }

    [ClientRpc]
    public void RpcCycle()
    {
        if (isLocalPlayer)
        {
            state++;
            state = state % 3;
            CycleCams();
        }
    }

    [Command]
    public void CmdCycle()
    {
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().RpcCycle();
        GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<Player>().RpcCycle();
        GameObject.FindGameObjectsWithTag("Player")[2].GetComponent<Player>().RpcCycle();
    }

    private void CycleCams()
    {
        for (int i=0;i<3;i++)
        {
            ship.cams[i].enabled = i==state;
        }
    }

    /*[ClientRpc]
    public void RpcShipMove(Vector3 position, Vector3 rotate)
    {
        ship.transform.position = position;
        ship.transform.Rotate(rotate);
    }

    [Command]
    public void CmdShipMove(Vector3 transl, Vector3 rotate)
    {
        ship.transform.position =  ship.transform.position + transl;
        ship.transform.Rotate(rotate);

        RpcShipMove(ship.transform.position, rotate);
    }*/
    public void shipMove(Vector3 transl, Vector3 rotate)
    {
        ship.transform.Translate(transl);
        ship.transform.Rotate(rotate);
    }

    [Command]
    public void CmdFire()
    {
        var bullet = (GameObject)Instantiate(bulletPref, ship.cam.position, ship.cam.rotation);
        NetworkServer.SpawnWithClientAuthority(bullet,gameObject);
        Destroy(bullet, 4.0f);
    }

    [Command]
    public void CmdSpawnAsteroid(Vector3 pos)
    {
        pos *= 100;
        pos.y /= 35;
        pos.y = 0;
        var asteroid = (GameObject)Instantiate(asteroidPrefab, ship.transform.position + pos, ship.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(asteroid, gameObject);
        Destroy(asteroid, 30f);
    }

    [Command]
    public void CmdSetHP(int hp)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().RpcSetHP(hp);
        }
    }

    [ClientRpc]
    public void RpcSetHP(int hp)
    {
        hitpoints = hp;
    }

    [Command]
    public void CmdSetEnergy(float energy)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().RpcSetEnergy(energy);
        }
    }

    [ClientRpc]
    public void RpcSetEnergy(float energy)
    {
        this.energy = energy;
    }
}
