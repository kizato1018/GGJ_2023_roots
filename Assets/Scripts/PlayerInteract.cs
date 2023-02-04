using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInteract : MonoBehaviour
{
    public Player player;
    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }
    void Update() {
        if (player.boxmap == null) return;
        Vector3Int location = player.boxmap.WorldToCell(gameObject.transform.position);
        // print(location);
        // print(player.boxmap.GetTile(location));
        player.current_face = player.boxmap.GetTile(location);
    }
    public void SetTile(Tile tile) {
        Vector3Int location = player.boxmap.WorldToCell(gameObject.transform.position);
        player.boxmap.SetTile(location, tile);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        Debug.Log(collision.gameObject.name);
    }
}