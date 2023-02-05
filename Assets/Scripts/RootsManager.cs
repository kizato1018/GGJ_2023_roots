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
    public TileBase root;
    public List<Transform> Start_RootDatas = new List<Transform>();
    public List<RootData> RootDatas = new List<RootData>();
    
    // Start is called before the first frame update
    private bool is_connected = false;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        foreach (Transform start in Start_RootDatas)
        {
            RootData rootData = new RootData(start.position, map);
            RootDatas.Add(rootData);
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
        bool current_connect = is_connected;
        RootData rootData = new RootData(pos, map);
        RootDatas.Add(rootData);
        map.SetTile(rootData.v3IntPosition, root);
        CheckToPool();
        is_connected = TreeManager.instance.AllPoolControllerList[0].isConnect;
        if(current_connect != is_connected)
        {
            Debug.Log("連上水池");
        }
    }
    public void DeleteRoot(Vector3 pos)
    {
        bool current_connect = is_connected;
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
        CheckToPool();
        is_connected = TreeManager.instance.AllPoolControllerList[0].isConnect;
        if(current_connect != is_connected)
        {
            Debug.Log("斷開水池");
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

    public void CheckToPool()
    {

        List<Vector3Int> _visited = new List<Vector3Int>();
        Queue<Vector3Int> q = new Queue<Vector3Int>();
        for(int i = 0; i < Start_RootDatas.Count; i++){
            q.Enqueue(RootDatas[i].v3IntPosition);
        }


        foreach(PoolController pc in TreeManager.instance.AllPoolControllerList)
        {
            pc.isConnect = false;
        }
        while(q.Count > 0) {
            Vector3Int location = q.Dequeue();
            print(location);
            if (_visited.Contains(location)) continue;
            _visited.Add(location);
            if (map.GetTile(location) && map.GetTile(location).name == "pool")
            {
                print("is POOL 1!");
                TreeManager.instance.AllPoolControllerList[0].isConnect = true;
                continue;
            }
            if (map.GetTile(location) && map.GetTile(location).name == "pool1")
            {
                print("is POOL 2!");
                TreeManager.instance.AllPoolControllerList[1].isConnect = true;
                continue;
                // return TreeManager.instance.AllPoolControllerList[1];
            }
            if (map.GetTile(location) && map.GetTile(location).name == "pool2")
            {
                print("is POOL 3!");
                TreeManager.instance.AllPoolControllerList[2].isConnect = true;
                continue;
                // return TreeManager.instance.AllPoolControllerList[2];
            }
            if(map.GetTile(location) && map.GetTile(location).name == "root")
            {
                q.Enqueue(location+Vector3Int.up);
                q.Enqueue(location+Vector3Int.right);
                q.Enqueue(location+Vector3Int.down);
                q.Enqueue(location+Vector3Int.left);
            }
        }
        return;
    }
}
