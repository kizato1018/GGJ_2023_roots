using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectSkill
{
    public void UseSkill();
}

public class Object : MonoBehaviour, ObjectSkill
{
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

    public void UseSkill()
    {
    }
}
