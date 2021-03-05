using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PowerUpSpawner : NetworkBehaviour
{

    private GameObject[] roadtiles;
    public NetworkIdentity MaskPowerUpPrefab;
    public NetworkIdentity VaccinePowerUpPrefab;
    public float SpawnProbability = 0.1f;
    public GameObject SpawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnStartServer()
    {
        if (!isServer)
        {
            return;
        }

        Transform t = SpawnPoints.transform;
        foreach (Transform child in t)
        {
            SpawnMaskPowerUp(child);
            SpawnVaccinePowerUp(child);
        }

        //roadtiles = GameObject.FindGameObjectsWithTag("Road");

        //foreach (GameObject roadtile in roadtiles)
        //{
        //    if (roadtile.name.Contains("corner"))
        //    {
        //        SpawnMaskPowerUp(roadtile.transform);
        //        SpawnVaccinePowerUp(roadtile.transform);
        //    }

        //}
    }

    [Server]
    private void SpawnMaskPowerUp(Transform pos)
    {
       
        Vector3 spawnPosition = pos.position;

        List<Vector3> offsets = new List<Vector3>();

        offsets.Add(new Vector3(0, 1, 2));
        offsets.Add(new Vector3(0, 1, -5));
        
        foreach(Vector3 spawn_offset in offsets)
        {
            GameObject NewMaskPowerUp = Instantiate(MaskPowerUpPrefab.gameObject, spawnPosition + spawn_offset, Quaternion.identity, transform);
            NetworkServer.Spawn(NewMaskPowerUp);
        }




    }

    [Server]
    private void SpawnVaccinePowerUp(Transform pos)
    {
        Vector3 spawnPosition = pos.position;

        List<Vector3> offsets = new List<Vector3>();
        offsets.Add(new Vector3(0, 1, -2));
        offsets.Add(new Vector3(0, 1, 5));

        foreach (Vector3 spawn_offset in offsets)
        {
            GameObject NewVaccinePowerUp = Instantiate(VaccinePowerUpPrefab.gameObject, spawnPosition + spawn_offset, Quaternion.identity, transform);
            NetworkServer.Spawn(NewVaccinePowerUp);
        }


    }
}
