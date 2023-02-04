using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterGenerate List", menuName = "Entity Stats/Monster Generate List")]
public class MonsterGenerateList_SO : ScriptableObject
{
    [Header("Monster Generate List")]
    
    public List<MonsterGenerateSetting> generate_list;
}

public struct MonsterGenerateSetting{
    public Transform position;
    public int time;
}
