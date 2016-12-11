using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{

    public SpaceShip ship;
    public Action[] actions;
    public AudioSource alarm;
    public GameObject cursor;
    [SyncVar]
    public int state;
    public GameObject bulletPref;
    public GameObject asteroidPrefab;
    public GameObject asteroidPrefab2;
    public GameObject missilePrefab;
    private float timeTillNewAsteroid;
    public float timeTillNewMissile;
    private float timeTillNextCycle;
    private bool cycleWarning;
    public Texture textureCycleWarning;
    public Texture[] roleSprites = new Texture[3];

    private float maxEnergy = 100f;
    [SyncVar]
    public float energy;
    [SyncVar]
    public int hitpoints;
    public bool energyDown;

    [SyncVar]
    public int count;
    [SyncVar]
    public float energyDiff;

    [SyncVar]
    public bool ready;

    private bool paused;
    //00 Use this for initialization
    void Start()
    {
        paused = true;
        ship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>();
        timeTillNextCycle = Random.Range(30, 60);
        timeTillNewAsteroid = Random.Range(5, 10);
        timeTillNewMissile = Random.Range(20, 30);

        energy = 50;
        hitpoints = 100;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
            count = players[0].GetComponent<Player>().count;


        if (isLocalPlayer)
        {

            ship.player = this;
            state = (GameObject.FindGameObjectsWithTag("Player").Length - 1) % 3;
            CmdState(state);
            GameObject.Find("Button").GetComponent<Transform>().transform.position = GameObject.Find("Button").GetComponent<Transform>().transform.position + new Vector3(0, -30 * state, 0);
            Debug.Log(state);
        }
    }

    public void realStart()
    {

        ship.cams[3].enabled = false;
        if (isLocalPlayer)
        {
            if (state == 1)
                Instantiate(cursor, Vector3.zero, Quaternion.identity);
            Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;//Achtung
            ship.cams[state].enabled = true;
            Destroy(GameObject.Find("CanvasMen"));
        }

    }
    void LateUpdate()
    {
        if (hitpoints <= 0)
            SceneManager.LoadScene("RIP");
        if (paused)
            return;
        if (!isServer || !isLocalPlayer)
            return;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != gameObject)
                energyDiff += players[i].GetComponent<Player>().energyDiff;
        }
            energyDiff -= 2 * Time.deltaTime;
            CmdSetEnergy(energy - energyDiff);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            if (count == 3)
            {
                paused = false;
                realStart();
            }
            else
            {
                return;
            }
        }
        energyDiff = 0;
        if (energy <= 0)
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

        if (isServer)
        {
            //Asteroid Spawns
            if (timeTillNewAsteroid <= 0)
            {
                Vector3 pos = Random.onUnitSphere;
                CmdSpawnAsteroid(pos);
                timeTillNewAsteroid = Random.Range(3, 10);
            }
            else
            {
                timeTillNewAsteroid -= Time.deltaTime;
            }

            //Missile Spawns
            if (timeTillNewMissile <= 0)
            {
                Vector3 pos = Random.onUnitSphere;
                CmdSpawnMissile(pos);
                timeTillNewMissile = Random.Range(20, 30);
            }
            else
            {
                timeTillNewMissile -= Time.deltaTime;
            }

            //CYCLE
            if (timeTillNextCycle <= 0)
            {
                timeTillNextCycle = Random.Range(30, 60);
                CmdCycle();
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
        if (energyDown)
            GUI.Label(new Rect(width - 100, height - 25, 100, 25), "Energy down!");
        else
            GUI.Label(new Rect(width - 100, height - 25, 100, 25), "Energy:\t" + (int)energy);

        if (cycleWarning)
        {
            GUI.DrawTexture(new Rect(width / 2 - 200, height / 2 - 200, 400, 400), textureCycleWarning);
        }

        if (isLocalPlayer)
        {
            GUI.DrawTexture(new Rect(width - 50, 0, 50, 50), roleSprites[state]);
        }
    }

    public IEnumerator cycle()
    {
        if (isLocalPlayer)
        {
            alarm.Play();
            cycleWarning = true;
            yield return new WaitForSeconds(2.5f);
            cycleWarning = false;
            state++;
            state = state % 3;
            actions[state].reset();
            CmdState(state);
            CycleCams();
            if (state == 1)
                Instantiate(cursor, Vector3.zero, Quaternion.identity);
        }
    }

    public IEnumerator energyDowntime()
    {
        yield return new WaitForSeconds(5f);
        energy = 20;
        energyDown = false;
    }

    public void takeDamage(int damage)
    {
        if (!isServer)
        {
            return;
        }
        CmdSetHP(hitpoints - damage);
    }

    public void useEnergy(float energy)
    {
        energyDiff += energy;
    }

    public void gainEnergy(float energy)
    {
        CmdSetEnergy(this.energy + energy);
    }

    [ClientRpc]
    public void RpcCycle()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            GameObject a = GameObject.Find("Crosshair(Clone)");
            Destroy(a);
        }
            StartCoroutine(cycle());
    }

    [Command]
    public void CmdState(int state)
    {
        RpcState(state);
    }
    [ClientRpc]
    public void RpcState(int state)
    {
        this.state = state;
    }
    [Command]
    public void CmdCycle()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {

            players[i].GetComponent<Player>().RpcCycle();
        }
    }

    private void CycleCams()
    {
        for (int i = 0; i < 3; i++)
        {
            ship.cams[i].enabled = i == state;
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
        NetworkServer.SpawnWithClientAuthority(bullet, gameObject);
        Destroy(bullet, 4.0f);
    }

    [Command]
    public void CmdSpawnAsteroid(Vector3 pos)
    {
        pos *= 100;
        pos.y /= 35;
        pos.y = 0;
        //Randomwert
        float tmp = Random.Range(1, 6);
        GameObject asteroid;
        if (tmp < 4)
        {
            asteroid = (GameObject)Instantiate(asteroidPrefab, ship.transform.position + pos, ship.transform.rotation);
        }
        else
        {
            asteroid = (GameObject)Instantiate(asteroidPrefab2, ship.transform.position + pos, ship.transform.rotation);
        }
        NetworkServer.SpawnWithClientAuthority(asteroid, gameObject);
        Destroy(asteroid, 30f);
    }

    [Command]
    public void CmdSpawnMissile(Vector3 pos)
    {
        pos *= 200;
        pos.y = 0;
        GameObject missile = (GameObject)Instantiate(missilePrefab, ship.transform.position + pos, ship.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(missile, gameObject);
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
        if (energy >= maxEnergy)
            energy = maxEnergy;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().RpcSetEnergy(energy);
        }
    }

    [ClientRpc]
    public void RpcSetEnergy(float energy)
    {
        this.energy = energy;
    }

    [Command]
    public void CmdUpdateCount(int count)
    {
        if (count > 0)
            ready = true;
        else
            ready = false;
        count = this.count + count;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().RpcUpdateCount(count);
        }
    }

    [ClientRpc]
    public void RpcUpdateCount(int count)
    {
        this.count = count;
    }

    [Command]
    public void CmdCamUpdate(Quaternion rot)
    {
        ship.cam.rotation = rot;
        RpcCamUpdate(rot);
    }

    [ClientRpc]
    public void RpcCamUpdate(Quaternion rot)
    {
        ship.cam.rotation = rot;
    }
}