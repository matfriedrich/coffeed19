using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBehaviour : NetworkBehaviour
{
    private Rigidbody rb;

    public float normalspeed = 10.0f;
    private float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float jumpheight = 5.0f;

    public bool boostAble = true;
    public float boostspeed = 20.0f;
    public float boosttime = 3.0f;
    public float waitBeforeNextBoost = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        speed = normalspeed;
    }

    [Client]
    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
        {
            // exit from update if this is not the local player
            return;
        }


        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(-translation, 0, 0);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

        if(Input.GetKey(KeyCode.LeftShift) && boostAble)
        {
            speed = boostspeed;
            boostAble = false;
            StartCoroutine(BoostingYield());
            StartCoroutine(Boost());
        }

        IEnumerator Boost()
        {
            yield return new WaitForSeconds(boosttime);
            speed = normalspeed;
        }

        IEnumerator BoostingYield()
        { 
            yield return new WaitForSeconds(waitBeforeNextBoost);
            boostAble = true;
        }

    }
}
