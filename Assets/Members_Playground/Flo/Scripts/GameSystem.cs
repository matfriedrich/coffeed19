using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class GameSystem : NetworkBehaviour
{

    [SyncVar(hook = nameof(CounterChanged))]
    public int counter;
    public int infection_count;
    public static string IP;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CounterChanged(int oldval, int newval)
    {
        Debug.Log("counter Updated from: " + oldval.ToString() + " to: " + newval.ToString());
        GameObject.FindWithTag("HUD_Counter").GetComponent<UnityEngine.UI.Text>().text = "Infections: " + counter.ToString();
    }

    public void increaseCounter() {
        counter++;
    }

    public void decreaseCounter()
    {
        counter--;
    }

    public void setIP()
    {
        GameObject.Find("NetworkManager").GetComponent<NetworkManager>().networkAddress = GameObject.Find("Text_IP").GetComponent<TextMeshProUGUI>().text;
    }

}
