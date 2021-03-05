using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class UpdateHUD : MonoBehaviour
{
    GameObject player;
    Text text_masks_equipped;
    Text text_syringes_equipped;
    int num_masks;
    int num_syringes;
    public int selected_difficulty;


    public GameObject gamesystem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!NetworkClient.isConnected && GameObject.Find("Dropdown") != null)
        {
            
            selected_difficulty = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>().value;

            switch(selected_difficulty)
            {
                case 0:
                    gamesystem.GetComponent<NPCSpawner>().healthyCitizensPerRoadtileProb = 0.3f;
                    gamesystem.GetComponent<NPCSpawner>().numInfectedCitizens = 1;
                    break;
                case 1:
                    gamesystem.GetComponent<NPCSpawner>().healthyCitizensPerRoadtileProb = 0.5f;
                    gamesystem.GetComponent<NPCSpawner>().numInfectedCitizens = 3;
                    break;
                case 2:
                    gamesystem.GetComponent<NPCSpawner>().healthyCitizensPerRoadtileProb = 0.8f;
                    gamesystem.GetComponent<NPCSpawner>().numInfectedCitizens = 5;
                    break;
            }

            if (GameObject.Find("Text_IP") != null)
                GameObject.Find("NetworkManager").GetComponent<NetworkManager>().networkAddress = GameObject.Find("Text_IP").GetComponent<TextMeshProUGUI>().text;
        }

        if (!NetworkClient.isConnected)
            return;

        player = GameObject.FindGameObjectWithTag("Player");

        num_masks = player.GetComponent<shooting>().maskNum;
        num_syringes = player.GetComponent<shooting>().vaccineNum;

        text_masks_equipped = GameObject.FindGameObjectWithTag("HUD_Masks_Equipped").GetComponent<Text>();
        text_syringes_equipped = GameObject.FindGameObjectWithTag("HUD_Syringe_Equipped").GetComponent<Text>();

        text_masks_equipped.text = "Masks: " + num_masks.ToString();
        text_syringes_equipped.text = "Vaccines: " + num_syringes.ToString();

        if (gamesystem.GetComponent<GameSystem>().counter == 0)
            GameObject.FindGameObjectWithTag("HUD_Won").GetComponent<Text>().enabled = true;

    }

    
}
