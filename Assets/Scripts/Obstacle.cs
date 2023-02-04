using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, BattleAction
{
    public int hp = 3;
    public void UnderAttack(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
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