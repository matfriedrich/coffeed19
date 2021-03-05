using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Citizen : NetworkBehaviour
{

    [SyncVar]
    public bool infected;
    [SyncVar]
    public bool vaccinated;
    [SyncVar]
    public bool masked;

    [SyncVar]
    public DateTime last_infection_timestamp;

    public int short_term_immuniity_in_seconds = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void cmdSetVaccinated(bool vaccinated)
    {
        this.vaccinated = vaccinated;
    }

    public bool shortTermImmunity()
    {
        if (last_infection_timestamp == null)
            return false;

        if (DateTime.Now.Subtract(last_infection_timestamp).Seconds < short_term_immuniity_in_seconds)
            return true;

        return false;
    }

    private void OnTriggerEnter(Collider collided_object)
    {
        if (!isServer)
        {
            return;
        }

        if (collided_object.tag == "Mask")
        {
            Debug.Log("Masking triggerd");
            Destroy(collided_object.gameObject);
            transform.Find("MaskCollider").GetComponent<SphereCollider>().radius = 1;
        }

        if (collided_object.tag == "Syringe")
        {
            Debug.Log("Vaccine triggerd");
            Destroy(collided_object.gameObject);
            vaccinated = true;
        }

    }
}
