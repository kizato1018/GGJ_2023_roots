using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public float Speed=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 goal = RootsManager.instance.FindNearRoot(transform.position).worldPosition;
        transform.position = Vector3.MoveTowards(transform.position, goal, Speed*Time.deltaTime);
        // if (transform.position == goal)
        // {
        //     RootsManager.Tilemap
        // }
    }
}
