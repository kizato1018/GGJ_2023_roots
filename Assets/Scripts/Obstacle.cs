using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, BattleAction
{
    public int hp = 3;
    public bool indestructible = false;
    public IEnumerator UnderAttack(int damage)
    {
        if (indestructible) AudioManager.instance.PlaySound("339360__newagesoup__drop02");
        else AudioManager.instance.PlaySound("96634__cgeffex__ricochet-wood");

        if (indestructible) yield break;
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
