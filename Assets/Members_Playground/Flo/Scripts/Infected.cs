using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Infected : NetworkBehaviour
{   


    private GameObject citizenPrefab;
    private GameObject infectedPrefab;

    private GameObject gamesystem;
    // Start is called before the first frame update
    void Start()
    {
       gamesystem = GameObject.FindWithTag("GameSystem");
       infectedPrefab = gamesystem.GetComponent<NPCSpawner>().infectedPrefab.gameObject;
       citizenPrefab = gamesystem.GetComponent<NPCSpawner>().citizenPrefabs[Random.Range(0,2)].gameObject;

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

            if(collided_object.GetComponent<Citizen>().vaccinated)
            {
                if (Random.Range(0, 100) > NPCSpawner.vaccine_efficiency && !collided_object.GetComponent<Citizen>().shortTermImmunity())
                    replaceNPC(collided_object.gameObject, infectedPrefab);
            }
            else
                replaceNPC(collided_object.gameObject, infectedPrefab);


        }

        if (collided_object.tag == "Mask")
        {
            Debug.Log("Masking triggerd");
            SoundEffectsHelper.Instance.MakeHitSound();
            Destroy(collided_object.gameObject);
            this.GetComponentInChildren<SphereCollider>().radius = 1;
        }

        if (collided_object.tag == "Syringe")
        {
            Debug.Log("Vaccine triggerd");
            SoundEffectsHelper.Instance.MakeHitSound();
            Destroy(collided_object.gameObject);
            replaceNPC(this.gameObject, citizenPrefab);
        }
        
    }

    [Server]
    private GameObject replaceNPC(GameObject target, GameObject newPrefab) {
        
        GameObject newNPC = Instantiate(newPrefab.gameObject, target.transform.position, Quaternion.identity, gamesystem.transform);
        NetworkServer.Spawn(newNPC);
        NetworkServer.Destroy(target);

        

        if(newNPC.tag == "Infected")
        {
            //infected = false;
            //infected = true;
            newNPC.GetComponent<Citizen>().infected = true;
            newNPC.GetComponent<Citizen>().vaccinated = false;
            gamesystem.GetComponent<GameSystem>().increaseCounter();
        }  
        else
        {
            newNPC.GetComponent<Citizen>().infected = false;
            newNPC.GetComponent<Citizen>().vaccinated = true;
            gamesystem.GetComponent<GameSystem>().decreaseCounter();
        }
        
        return newNPC;
        
    }

}
