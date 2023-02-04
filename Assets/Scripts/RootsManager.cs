using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootData
{
    public RootData(Vector3 _worldPosition, Tilemap map)
    {
        worldPosition = _worldPosition;
        v3IntPosition = map.WorldToCell(worldPosition);
    }
    public Vector3 worldPosition;
    public Vector3Int v3IntPosition;
}
public class RootsManager : MonoBehaviour
{
    public Tilemap map;
    public List<Transform> Start_RootDatas = new List<Transform>();
    public List<RootData> RootDatas = new List<RootData>();
    private List<Vector3Int> _visited = new List<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform start in Start_RootDatas)
        {
            CreateRoot(start.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanCreateRoot(Vector3 pos)
    {
        Vector3Int location = map.WorldToCell(pos);
        return map.GetTile(location) == null &&
               map.GetTile(location+Vector3Int.up).name == "root" || 
               map.GetTile(location+Vector3Int.right).name == "root" || 
               map.GetTile(location+Vector3Int.down).name == "root" || 
               map.GetTile(location+Vector3Int.left).name == "root";
    }

    public void CreateRoot(Vector3 pos)
    {
        RootData rootData = new RootData(pos, map);
        RootDatas.Add(rootData);
    }

    public RootData FindNearRoot(Vector3 pos)
    {
        RootData near_root = null;
        float min_distance = -1.0f;
        for(int i = Start_RootDatas.Count; i < RootDatas.Count; i++) {
            if(near_root == null)
            {
                near_root = RootDatas[i];
                min_distance = Vector3.Distance(pos, near_root.worldPosition);
            }
            else
            {
                float distance = Vector3.Distance(pos, near_root.worldPosition);
                if (distance < min_distance)
                {
                    near_root = RootDatas[i];
                    min_distance = Vector3.Distance(pos, near_root.worldPosition);
                }
            }
        }
        return near_root;
    }

    public RootData GetRoot(Vector3 pos)
    {
        return new RootData(pos, map);
    }

    public bool CheckToPool(Vector3Int location)
    {
        if (map.GetTile(location+Vector3Int.up).name == "pool" || 
            map.GetTile(location+Vector3Int.right).name == "pool" || 
            map.GetTile(location+Vector3Int.down).name == "pool" || 
            map.GetTile(location+Vector3Int.left).name == "pool")
        {
            _visited.Clear();
            return true;
        }
        
        if (map.GetTile(location+Vector3Int.up).name == "root" && _visited.Contains(location+Vector3Int.up))
        {
            _visited.Add(location+Vector3Int.up);
            CheckToPool(location+Vector3Int.up);
            _visited.Remove(location+Vector3Int.up);
        }
        if (map.GetTile(location+Vector3Int.right).name == "root" && _visited.Contains(location+Vector3Int.right))
        {
            _visited.Add(location+Vector3Int.right);
            CheckToPool(location+Vector3Int.right);
            _visited.Remove(location+Vector3Int.right);
        }
        if (map.GetTile(location+Vector3Int.down).name == "root" && _visited.Contains(location+Vector3Int.down))
        {
            _visited.Add(location+Vector3Int.down);
            CheckToPool(location+Vector3Int.down);
            _visited.Remove(location+Vector3Int.down);
        }
        if (map.GetTile(location+Vector3Int.left).name == "root" && _visited.Contains(location+Vector3Int.left))
        {
            _visited.Add(location+Vector3Int.left);
            CheckToPool(location+Vector3Int.left);
            _visited.Remove(location+Vector3Int.left);
        }
        return false;
    }
}
