using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolController : MonoBehaviour
{
    public int waterValue;
    public int percentage = 50;
    public bool isConnect = false;
    public Text waterValueText;
    public SpriteRenderer poolSprite;
    public List<Sprite> poolSprites = new List<Sprite>();

    private int waterValueMax;
    // Start is called before the first frame update
    void Start()
    {
        waterValueMax = waterValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (waterValue > (waterValueMax * percentage / 100))
        {
            poolSprite.sprite = poolSprites[0];
        }
        else
        {
            poolSprite.sprite = poolSprites[1];
        }
        waterValueText.text = waterValue.ToString();
    }
}
