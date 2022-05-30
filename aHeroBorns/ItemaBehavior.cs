using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemaBehavior : MonoBehaviour
{

    public GameBehavior gameManager;  

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>(); 
    }

    void OnCollisionEnter(Collision collision)
    {
        // 2
        if (collision.gameObject.name == "Player")
        {
            // 3
            Destroy(this.transform.parent.gameObject);
            // 4
            Debug.Log("Item collected!");
            gameManager.Items += 1; 
        }
    }



// Update is called once per frame
void Update()
    {
        
    }
}
