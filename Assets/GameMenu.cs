using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Mirror;

public class GameMenu : MonoBehaviour
{
    public bool game_menu_canvas_visible = false;
    public Canvas game_menu_canvas;
    // Start is called before the first frame update
    void Start()
    {
        //game_menu_canvas.enabled = game_menu_canvas_visible;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!NetworkClient.isConnected)
        //    return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(game_menu_canvas_visible)
            {
                game_menu_canvas.enabled = false;
                game_menu_canvas_visible = false;
            }
            else
            {
                game_menu_canvas.enabled = true;
                game_menu_canvas_visible = true;
            }
               
        }
    }

    public void GameMenuCanvasOFF()
    {
        game_menu_canvas.enabled = false;
    }

    public void GameMenuCanvasON()
    {
        game_menu_canvas.enabled = true;
    }

}
