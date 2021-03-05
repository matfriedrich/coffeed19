using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public enum PowerUpType { MASK, SYRINGE };

public class PowerUp : MonoBehaviour
{

    public float rotationspeed = 0.5f;
    public int amount = 5;
    public float duration = 4f; //for respawn
    public PowerUpType Type = PowerUpType.MASK;
    public Material[] powerup_materials; //just an array of materials, specify in Start() depending on PowerupType

    //public GameObject pickupEffect;

    private void Start()
    {

        // set materials from Powerup type
        //switch(Type)
        //{
        //    case PowerUpType.MASK:
        //        Material[] powerup_materials_mask = new Material[] { powerup_materials[0], powerup_materials[1] };
        //        this.GetComponentInParent<MeshRenderer>().materials = powerup_materials_mask;
        //        break;
        //    case PowerUpType.SYRINGE:
        //        Material[] powerup_materials_syringe = new Material[] { powerup_materials[1], powerup_materials[0] };
        //        this.GetComponentInParent<MeshRenderer>().materials = powerup_materials_syringe;
        //        break;
        //}
    }

    private void Update()
    {
        RotatePowerUp(rotationspeed);
    }

    void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player")
		{
			StartCoroutine(Pickup(other));
		}
	}


    IEnumerator Pickup(Collider player)
	{
		//pickup efect
		//Instantiate(pickupEffect, transform.position, transform.rotation);

		//increase mask ammount
		shooting stats = player.GetComponent<shooting>();

        if (Type == PowerUpType.MASK)
        {
            stats.maskNum += amount;
            Debug.Log("Mask Power up picked up!");
        }
           
        else if (Type == PowerUpType.SYRINGE)
        {
            stats.vaccineNum += amount;
            Debug.Log("Syringe Power up picked up!");
        }


        //disable the visual and collider until it respawn
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;


        //foreach (Transform child in gameObject.transform)
        //{
        //    child.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //    child.gameObject.GetComponent<Collider>().enabled = false;
        //}


        yield return new WaitForSeconds(duration);

        //respawn
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

        //foreach (Transform child in gameObject.transform)
        //{
        //    child.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //    child.gameObject.GetComponent<Collider>().enabled = true;
        //}


    }

    private void RotatePowerUp(float angle_per_tick)
    {
        transform.Rotate(new Vector3(0, 1, 0), angle_per_tick);
    }
}
