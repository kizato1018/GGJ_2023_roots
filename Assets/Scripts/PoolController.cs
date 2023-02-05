using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolController : MonoBehaviour
{
    public int waterValue;
    public bool isConnect = false;
    public Text waterValueText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waterValueText.text = waterValue.ToString();
    }
}
