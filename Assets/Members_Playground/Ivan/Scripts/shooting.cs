using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class shooting : NetworkBehaviour
{
    public Vector3 Shootposition;
    public Vector3 Shootrotation;

    public GameObject mask;
    public GameObject syringe;
    public Transform pointofshoot;

    public int bulletSpeed;
    public float despawnTime = 5.0f;

    public bool shootAble = true;
    public float waitBeforeNextShot = 1.25f;

    //this is showing the amount of mask and vaccine in the ambulance
    public int maskNum = 5;
    public int vaccineNum = 5;


 void Start ()
    {
      

    }
    [Client]
     void Update()
    {

        if (!hasAuthority)
        {
            // exit from update if this is not the local player
            return;
        }
        if (Input.GetKey(KeyCode.O) || Input.GetKey(KeyCode.Mouse0))
        {
            if (shootAble && maskNum != 0)
            {
                shootAble = false;
                //Shoot1();
                CmdShootMask();
                maskNum -= 1;
                StartCoroutine(ShootingYield());
                SoundEffectsHelper.Instance.MakePlayerShotSound();
            }
        }
        if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Mouse1))
        {
            if (shootAble && vaccineNum != 0)
            {
                shootAble = false;
                //Shoot2();
                 CmdShootSyringe();
                vaccineNum -= 1;
                StartCoroutine(ShootingYield());
                SoundEffectsHelper.Instance.MakePlayerShotSound();
            }
        }
    }

    IEnumerator ShootingYield()
    {
        yield return new WaitForSeconds(waitBeforeNextShot);
        shootAble = true;
    }
   /* void Shoot1() //shooting masks
    {
        var bullet = Instantiate(mask, pointofshoot.position + Shootposition , pointofshoot.rotation);
        bullet.transform.Rotate(0, Shootrotation.y, 0); 
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        
        maskNum -= 1;
        Destroy(bullet, despawnTime);
    }
    void Shoot2() //shooting vaccines
    {
        var bullet = Instantiate(syringe, pointofshoot.position + Shootposition, pointofshoot.rotation);
        bullet.transform.Rotate(0, Shootrotation.y, 0);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        vaccineNum -= 1;
        Destroy(bullet, despawnTime);
    }
    */
    [Command] 
    private void CmdShootSyringe() {
         var syringeInstance = Instantiate(syringe, pointofshoot.position + Shootposition, pointofshoot.rotation);
        syringeInstance.transform.Rotate(0, Shootrotation.y, 0);
        syringeInstance.GetComponent<Rigidbody>().velocity = syringeInstance.transform.forward * bulletSpeed;
        Destroy(syringeInstance, despawnTime);
        NetworkServer.Spawn(syringeInstance);
    }

    [Command] 
    private void CmdShootMask() {
         var maskInstance = Instantiate(mask, pointofshoot.position + Shootposition, pointofshoot.rotation);
        maskInstance.transform.Rotate(0, Shootrotation.y, 0);
        maskInstance.GetComponent<Rigidbody>().velocity = maskInstance.transform.forward * bulletSpeed;
        Destroy(maskInstance, despawnTime);
        NetworkServer.Spawn(maskInstance);
    }

}