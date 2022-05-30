using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameBehavior : MonoBehaviour
{
    public string labelText = "Collect all 4 items and win your freedom";
    public const int maxItems = 1;
    public bool showWinScreen = false;
    public bool showLossScreen = false;

    private int _itemsCollected = 0;
    private int _playerHP = 1;

    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;

            if (_itemsCollected >= maxItems)
            {
                UpdateWinLossWindows(true);
            }
            else
            {
                labelText = "Item Found, only " + (maxItems - _itemsCollected) + "more to go!";
            }

            //Debug.LogFormat("Items :{0} ", _itemsCollected);              
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                UpdateWinLossWindows(false); 
            }
            else
            {
                labelText = "Ouch .... that's got hurt"; }

            Debug.LogFormat("Lives :{0} ", _playerHP);
        }
    }

    void UpdateWinLossWindows(bool window)
    {
        if (window)
        {
            labelText = "You've found all the items!";
            showWinScreen = true;
        }
        else
        {
            labelText = "You want another life with that?";
            showLossScreen = true;
        }

        Time.timeScale = 0f;
    }


  

    //El metodo OnGUI es como un Update 
    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2, 150, 25), "Player Health:" + _playerHP);

        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2 + 40, 150, 25), "Items Collected: " + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + Screen.height / 3, 300, 50), labelText);

        if (showWinScreen)
        {
            //Se hace el if por que es cuando el boton es clickeado 
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel1();
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Lose ..."))
            {
                Utilities.RestartLevel1();
            }
        }

    }

}
