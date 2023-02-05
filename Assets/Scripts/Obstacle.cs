using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, BattleAction
{
    public int hp = 3;
    public bool indestructible = false;
    public void UnderAttack(int damage)
    {
        if (indestructible) return;
        hp -= damage;
        if (hp <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        RootsManager.instance.DeleteObstacle(transform.position);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
