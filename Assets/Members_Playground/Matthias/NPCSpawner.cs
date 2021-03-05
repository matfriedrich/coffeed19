using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System.Collections.Generic;

enum vaccines{BIONTECH = 85, ASTRA_ZENECA = 70, SPUTNIK = 91};

public class NPCSpawner : NetworkBehaviour
{
    public GameObject gamesystem;
    public List<NetworkIdentity> citizenPrefabs;
    public NetworkIdentity infectedPrefab;
    public float healthyCitizensPerRoadtileProb = 0.1f;
    public int numInfectedCitizens = 5;
    public static int vaccine_efficiency = System.Convert.ToInt16(vaccines.ASTRA_ZENECA);
    public static float mask_efficiency = 0.2f;

    private GameObject[] roadtiles;


    public override void OnStartServer()
    {
        if (!isServer)
        {
            return;
        }

        roadtiles = GameObject.FindGameObjectsWithTag("Road");

        foreach(GameObject roadtile in roadtiles)
        {
            if(Random.Range(0.0f, 1.0f) < healthyCitizensPerRoadtileProb)
                SpawnCitizen(roadtile.transform);
        }


        for(int i=0; i < numInfectedCitizens; i++)
        {
            int random_index = Random.Range(0, roadtiles.Length);
            if (gamesystem.GetComponent<GameSystem>().counter <= numInfectedCitizens)
                SpawnInfected(roadtiles[random_index].transform);
        }


    }


    [Server]
    private void SpawnCitizen(Transform pos)
    {
        Vector3 spawnPosition = new Vector3(pos.position.x, 1, pos.position.z);

        GameObject newCitizen = Instantiate(citizenPrefabs[Random.Range(0, citizenPrefabs.Count)].gameObject, spawnPosition, Quaternion.identity, transform);
        NetworkServer.Spawn(newCitizen);
    }

    [Server]
    private void SpawnInfected(Transform pos)
    {
        Vector3 spawnPosition = new Vector3(pos.position.x, 1, pos.position.z);

        gamesystem.GetComponent<GameSystem>().increaseCounter();

        GameObject newInfected = Instantiate(infectedPrefab.gameObject, spawnPosition, Quaternion.identity, transform);
        NetworkServer.Spawn(newInfected);
    }


}

