using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Utilities
{
    public static int playerDeaths = 0; 

    public static void RestartLevel1()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 0f; 
    }

}
    // Start is called before the first frame update
   
    




