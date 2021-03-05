using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CountWithButtonPress : NetworkBehaviour
{
    public GameObject gamesystem;
    private NetworkIdentity gameSystemNetworkId;
    

    // Start is called before the first frame update
    void Start()
    {
        gamesystem = GameObject.FindWithTag("GameSystem");
        gameSystemNetworkId = gamesystem.GetComponent<NetworkIdentity>();
    }

    [ClientCallback]
    // Update is called once per frame
    void Update()
    {
      

        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameSystemNetworkId.AssignClientAuthority(connectionToClient);


            CmdInreaseCounter();  
            gameSystemNetworkId.RemoveClientAuthority();   
        }
    }

    void CounterChanged(int oldVal, int newVal) {
       Debug.Log("counter Updated from: " + oldVal.ToString() + " to: " + newVal.ToString());
    }

    private void OnServerInitialized()
    {
     
        Debug.Log("Server initialized");
    }

    [Command]
    public void CmdInreaseCounter()
    {
        gamesystem.GetComponent<GameSystem>().increaseCounter();
    }



}
