using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPlataformBehavior : MonoBehaviour
{

    public PlayerBehavior player; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == player.name)
        {
            Debug.Log("Bonus de salto obtenido");
            player.PowerJump();
            Destroy(this.gameObject);
        }
    }


}
