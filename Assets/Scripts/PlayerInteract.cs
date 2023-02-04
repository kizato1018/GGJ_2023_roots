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
        player.current_face_tile = player.boxmap.GetTile(location);
    }
    public void SetTile(Tile tile) {
        Vector3Int location = player.boxmap.WorldToCell(gameObject.transform.position);
        player.boxmap.SetTile(location, tile);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null || collision.transform.tag == "Player") return;
        player.current_face_object = collision.gameObject;
        Debug.Log(collision.gameObject.name);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        player.current_face_object = null;
    }
}