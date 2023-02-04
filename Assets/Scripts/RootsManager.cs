using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RootData
{
    public RootData(Vector3 _worldPosition)
    {
        worldPosition = _worldPosition;
    }
    public Vector3 worldPosition;
    public Vector3Int v3IntPosition;
}
public class RootsManager : MonoBehaviour
{
    private List<RootData> RootDatas = new List<RootData>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRoot(Vector3 pos)
    {
        RootData rootData = new RootData(pos);
        RootDatas.Add(rootData);
    }

    public RootData FindNearRoot(Vector3 pos)
    {
        //TODO
        return null;
    }
}
