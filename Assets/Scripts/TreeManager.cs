using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;



public class TreeManager : MonoBehaviour
{
    static public TreeManager instance;

    public int maxWaterValue = 100;
    public int waterValue = 0;
    public List<PoolController> PoolControllerList = new List<PoolController>();

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartAddWater(PoolController poolController)
    {
        if (!PoolControllerList.Contains(poolController)) PoolControllerList.Add(poolController);
        StartCoroutine(AddWater());
    }

    public void RemovePoolController(PoolController poolController)
    {
        if (PoolControllerList.Contains(poolController)) PoolControllerList.Remove(poolController);
    }

    //public void Stop

    IEnumerator AddWater()
    {
        yield return new WaitForSeconds(1);
        if (PoolControllerList.Count == 0) yield break;
        foreach (PoolController data in PoolControllerList)
        {
            if (data.waterValue > 0)
            {
                data.waterValue--;
                waterValue++;
            }
        }
        if (waterValue >= maxWaterValue)
        {
            Debug.Log("º¡¤F");
        }
        else
        {
            StartCoroutine(AddWater());
        }
    }
}
