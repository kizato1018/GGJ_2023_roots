using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectSkill
{
    public void UseSkill(GameObject owner);
}

public interface BattleAction
{
    public void UnderAttack(int damage);
}

public class Object : MonoBehaviour, ObjectSkill
{
    protected GameObject _owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WasTaken()
    {

    }

    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }

    public virtual void UseSkill(GameObject owner)
    {
    }
}
