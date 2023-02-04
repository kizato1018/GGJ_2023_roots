using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public float Speed=1;
    public float sleep_time = 1.0f;
    private float sleep_timer=0.0f;
    public float check_time = 3.0f;
    private float check_timer=0.0f;
    private RootData current_goal = null; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current_goal == null || check_timer > check_time)
        {
            current_goal = RootsManager.instance.FindNearRoot(transform.position);
            check_timer = 0.0f;
        }
        check_timer += Time.deltaTime;
        if (current_goal != null) {
            Vector3 moveVector = (current_goal.worldPosition - transform.position);
            moveVector.Normalize();
            transform.Translate(moveVector * Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position,current_goal.worldPosition) < 0.01f)
            {
                sleep_timer += Time.deltaTime;
                if(sleep_timer > sleep_time)
                {
                    RootsManager.instance.DeleteRoot(current_goal.worldPosition);
                    sleep_timer = 0.0f;
                    current_goal = null;
                }
            }
        }
        // print(current_goal);
    }
}
