using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    public Vector3 camOffst = new Vector3(0f, 1.2f, -2.6f);

    private Transform target; 
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform; 
    }

    private void LateUpdate()
    {
        this.transform.position = target.TransformPoint(camOffst);

        this.transform.LookAt(target);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
