using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Virus_Spreader : NetworkBehaviour
{   
    [SyncVar]
    public bool infected;
    [SyncVar]
    public bool vaccinated;

    public NetworkIdentity citizenPrefab;
    public NetworkIdentity infectedPrefab;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collided_object)
    { 
        if(!isServer) {
            return;
        }

        
        if (collided_object.tag == "Citizen")
        {

        
            Debug.Log("infection triggerd");
            
            replaceNPC(collided_object.gameObject, infectedPrefab);
            
            
        }
/*
        if (collided_object.tag == "Mask")
        {
            Debug.Log("Masking triggerd");

            this.GetComponent<SphereCollider>().radius = 1;
        }

        if (collided_object.tag == "Syringe")
        {
            Debug.Log("Vaccine triggerd");

            infected = false;
            vaccinated = true;
        }

        */
    }

    [Server]
    private void replaceNPC(GameObject target, NetworkIdentity newPrefab) {
        GameObject gamesystem = GameObject.FindWithTag("GameSystem");
        GameObject newNPC = Instantiate(gamesystem.GetComponent<NPCSpawner>().infectedPrefab.gameObject, target.transform.position, Quaternion.identity);
        NetworkServer.Spawn(newNPC);
        NetworkServer.Destroy(target);
        
        if(newNPC.tag == "infected")
            gamesystem.GetComponent<GameSystem>().increaseCounter();
    }

}
