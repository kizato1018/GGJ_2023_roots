﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Object
{
    public int attackValue = 1;
    public float angle;
    public float radius;
    public LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UseSkill(GameObject owner)
    {
        Debug.Log("attack!");
        // 取得扇形的中心点
        Vector2 origin = owner.transform.position;

        // 构造扇形的方向
        Vector2 direction = owner.transform.up;

        // 检查扇形内是否有敌人碰撞体
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            Vector2 hitDirection = (Vector2)hit.transform.position - origin;
            if (Vector2.Angle(direction, hitDirection) <= angle / 2)
            {
                Debug.Log("Enemy found inside fan");
                BattleAction ba = hit.gameObject.GetComponent<BattleAction>();
                if (ba!=null) ba.UnderAttack(attackValue);
                return;
            }
        }
        Debug.Log("No enemy found inside fan");
    }

    private void OnDrawGizmos()
    {
        if(_owner ==null) return;
        Vector2 origin = _owner.transform.position;
        Vector2 direction = _owner.transform.up;

        // 画出扇形
        Vector2 leftLimit = Quaternion.Euler(0, 0, angle / 2) * direction;
        Vector2 rightLimit = Quaternion.Euler(0, 0, -angle / 2) * direction;
        Gizmos.DrawLine(origin, origin + leftLimit * radius);
        Gizmos.DrawLine(origin, origin + rightLimit * radius);
        Gizmos.DrawLine(origin + leftLimit * radius, origin + rightLimit * radius);

    }
}
