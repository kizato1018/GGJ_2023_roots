using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;



public class TreeManager : MonoBehaviour
{
    static public TreeManager instance;

    public int maxWaterValue = 100;
    public int waterValue = 0;
    public List<PoolController> AllPoolControllerList = new List<PoolController>();

    public List<PoolController> PoolControllerList = new List<PoolController>();

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddWater());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartAddWater(PoolController poolController)
    {
        if (!PoolControllerList.Contains(poolController)) PoolControllerList.Add(poolController);
        
    }

    public void RemovePoolController(PoolController poolController)
    {
        if (PoolControllerList.Contains(poolController)) PoolControllerList.Remove(poolController);
    }

    //public void Stop

    IEnumerator AddWater()
    {
        while(waterValue < maxWaterValue)
        {
            foreach (PoolController pc in PoolControllerList)
            {
                if (pc.waterValue > 0)
                {
                    pc.waterValue--;
                    waterValue++;
                }
                print(pc.waterValue);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
