using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInteract : MonoBehaviour
{
    public Player player;
    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        player.current_face = other.gameObject;
    }

    private void OnCollisionExit2D(Collision2D other) {
        player.current_face = null;
    }
}