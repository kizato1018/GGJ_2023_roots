using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
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
    static public RootsManager instance;
    public Tilemap map;
    public List<Transform> Start_RootDatas = new List<Transform>();
    public List<RootData> RootDatas = new List<RootData>();
    private List<Vector3Int> _visited = new List<Vector3Int>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        foreach (Transform start in Start_RootDatas)
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

        Vector3Int up = location+Vector3Int.up;
        Vector3Int right = location+Vector3Int.right;
        Vector3Int down = location+Vector3Int.down;
        Vector3Int left = location+Vector3Int.left;
        TileBase tile = map.GetTile(location);
        if (tile != null && tile.name == "obstacle") return false;
        return map.GetTile(location) == null && (
               (map.GetTile(up) && map.GetTile(up).name == "root") || 
               (map.GetTile(right) && map.GetTile(right).name == "root") || 
               (map.GetTile(down) && map.GetTile(down).name == "root") || 
               (map.GetTile(left) && map.GetTile(left).name == "root"));
    }

    public void CreateRoot(Vector3 pos)
    {
        RootData rootData = new RootData(pos, map);
        RootDatas.Add(rootData);
        CheckAllRoots();
    }
    public void DeleteRoot(Vector3 pos)
    {
        RootData rootData = new RootData(pos, map);
        map.SetTile(rootData.v3IntPosition, null);
        foreach (RootData rd in RootDatas)
        {
            if (rd.v3IntPosition == rootData.v3IntPosition)
            {
                RootDatas.Remove(rd);
                break;
            }
        }
        CheckAllRoots();
    }
    public void CheckAllRoots() {
        for(int i = 0; i < Start_RootDatas.Count; ++i) {
            PoolController pc = CheckToPool(RootDatas[i].v3IntPosition);
            print(pc);
            if (pc != null) 
            {
                TreeManager.instance.StartAddWater(pc);
                print("Connected!!");
            }
            _visited.Clear();
        }
    }

    public void DeleteObstacle(Vector3 worldPosition)
    {
        Vector3Int v3IntPosition = map.WorldToCell(worldPosition);
        map.SetTile(v3IntPosition, null);
    }

    public RootData FindNearRoot(Vector3 pos)
    {
        RootData near_root = null;
        float min_distance = 999.0f;
        for (int i = Start_RootDatas.Count; i < RootDatas.Count; i++)
        {
            if (near_root == null)
            {
                near_root = RootDatas[i];
                min_distance = Vector3.Distance(pos, near_root.worldPosition);
            }
            else
            {
                float distance = Vector3.Distance(pos, RootDatas[i].worldPosition);
                if (distance < min_distance)
                {
                    near_root = RootDatas[i];
                    min_distance = distance;
                }
            }
        }
        return near_root;
    }

    public RootData GetRoot(Vector3 pos)
    {
        return new RootData(pos, map);
    }

    public PoolController CheckToPool(Vector3Int location)
    {

        Vector3Int up = location+Vector3Int.up;
        Vector3Int right = location+Vector3Int.right;
        Vector3Int down = location+Vector3Int.down;
        Vector3Int left = location+Vector3Int.left;

        if ((map.GetTile(up) && map.GetTile(up).name == "pool") || 
            (map.GetTile(right) && map.GetTile(right).name == "pool") || 
            (map.GetTile(down) && map.GetTile(down).name == "pool") || 
            (map.GetTile(left) && map.GetTile(left).name == "pool"))
        {
            
            print("is POOL 1!");
            return TreeManager.instance.AllPoolControllerList[0];
        }

        if ((map.GetTile(up) && map.GetTile(up).name == "pool1") || 
            (map.GetTile(right) && map.GetTile(right).name == "pool1") || 
            (map.GetTile(down) && map.GetTile(down).name == "pool1") || 
            (map.GetTile(left) && map.GetTile(left).name == "pool1"))
        {
            print("is POOL 2!");
            return TreeManager.instance.AllPoolControllerList[1];
        }

        if ((map.GetTile(up) && map.GetTile(up).name == "pool2") || 
            (map.GetTile(right) && map.GetTile(right).name == "pool2") || 
            (map.GetTile(down) && map.GetTile(down).name == "pool2") || 
            (map.GetTile(left) && map.GetTile(left).name == "pool2"))
        {

            print("is POOL 2!");
            return TreeManager.instance.AllPoolControllerList[2];
        }

        _visited.Add(location);
        if (map.GetTile(up) && map.GetTile(up).name == "root" && !_visited.Contains(up))
        {
            PoolController pc = CheckToPool(up);
            if(pc)
                return pc;
        }
        if (map.GetTile(right) && map.GetTile(right).name == "root" && !_visited.Contains(right))
        {
            PoolController pc = CheckToPool(right);
            if(pc)
                return pc;
        }
        if (map.GetTile(down) && map.GetTile(down).name == "root" && !_visited.Contains(down))
        {
            PoolController pc = CheckToPool(down);
            if(pc)
                return pc;
        }
        if (map.GetTile(left) && map.GetTile(left).name == "root" && !_visited.Contains(left))
        {
            PoolController pc = CheckToPool(left);
            if(pc)
                return pc;
        }
        _visited.Remove(location);
        return null;
    }
}
