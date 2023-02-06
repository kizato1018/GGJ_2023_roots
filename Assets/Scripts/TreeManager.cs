using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    static public TreeManager instance;

    public int maxWaterValue = 100;
    public int waterValue = 0;
    public int percentage1 = 50;
    public int percentage2 = 50;
    public List<PoolController> AllPoolControllerList = new List<PoolController>();

    public List<PoolController> PoolControllerList = new List<PoolController>();

    public SpriteRenderer treeSprite;
    public List<Sprite> treeSprites = new List<Sprite>();
    public Text waterValueText;

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
        if (waterValue >= (maxWaterValue * percentage2 / 100))
        {
            treeSprite.sprite = treeSprites[2];
        }
        else if(waterValue >= (maxWaterValue * percentage1 / 100) && waterValue < (maxWaterValue * percentage2 / 100))
        {
            treeSprite.sprite = treeSprites[1];
        }
        else
        {
            treeSprite.sprite = treeSprites[0];
        }
        waterValueText.text = waterValue.ToString();
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

    public IEnumerator AddWater()
    {
        while(waterValue < maxWaterValue)
        {
            foreach (PoolController pc in AllPoolControllerList)
            {
                if (pc.isConnect && pc.waterValue > 0)
                {
                    pc.waterValue--;
                    waterValue++;
                    if(waterValue >= maxWaterValue)
                    {
                        GameManager.instance.GameWin();
                        yield break;
                    }
                }
                //print(pc.waterValue);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
