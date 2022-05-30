using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform patrolRoute;
    public Transform player; 

    public List<Transform> locations;
    


    private int locationIndex = 0;
    private int _lives = 3; 
    private NavMeshAgent agent;

    public int EnemyLives
    {
        get { return _lives; }
        private set { _lives = value; 
        
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down"); 
            }
        
        }
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform; 

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child); 
        }
    }


    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;
        
        agent.destination = locations[locationIndex].position; 

        locationIndex = (locationIndex + 1) % locations.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position; 
            Debug.Log("Player detected - attack!"); 
        }
   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical Hit!"); 
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation(); 
        }
        
    }
}
