using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Monster/Monster Data")]
[System.Serializable]
public class Monster_SO : ScriptableObject
{
    [Header("Monster Info")]
    public Sprite sprite_;
    public int attack_;
    public int hp_;

    public Monster_SO (Monster_SO monster_so){
        sprite_ = monster_so.sprite_;
        attack_ = monster_so.attack_;
        hp_     = monster_so.hp_;

    }
}