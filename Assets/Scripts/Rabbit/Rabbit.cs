using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour, BattleAction
{
    public int hp = 5;
    public float Speed = 1;
    public float sleep_time = 1.0f;
    public float sleep_timer = 0.0f;
    public float check_time = 3.0f;
    public float check_timer = 0.0f;
    public RootData current_goal = null;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (current_goal == null || check_timer > check_time)
        {
            if (transform != null && RootsManager.instance != null)
                current_goal = RootsManager.instance.FindNearRoot(transform.position);
            check_timer = 0.0f;
        }
        check_timer += Time.deltaTime;
        if (current_goal != null)
        {
            Vector3 moveVector = (current_goal.worldPosition - transform.position);
            moveVector.Normalize();
            transform.Translate(moveVector * Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, current_goal.worldPosition) < 0.01f)
            {
                animator.SetTrigger("attack");
                AudioManager.instance.PlaySound("412068__inspectorj__chewing-carrot-a");
                sleep_timer += Time.deltaTime;
                if (sleep_timer > sleep_time)
                {
                    RootsManager.instance.DeleteRoot(current_goal.worldPosition);
                    sleep_timer = 0.0f;
                    current_goal = null;
                    //animator.SetBool("is_attacking", false);
                }
            }
        }
        // print(current_goal);
    }

    public IEnumerator UnderAttack(int damage)
    {
        AudioManager.instance.PlaySound("89769__cgeffex__fist-punch-3");
        hp -= damage;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (hp <= 0)
        {
            animator.SetTrigger("die");
            yield return new WaitForSeconds(1f);
            Died();
        }
        else
        {
            animator.SetTrigger("hurt");
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Died()
    {
        Destroy(gameObject);
    }
}
