using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Player : MonoBehaviour
{
    // The Rewired player id of this character
    public int playerId;

    // The movement speed of this character
    public float moveSpeed = 3.0f;

    // The bullet speed
    public float bulletSpeed = 15.0f;

    // Assign a prefab to this in the inspector.
    // The prefab must have a Rigidbody component on it in order to work.
    public GameObject bulletPrefab;

    private Rewired.Player player; // The Rewired Player
    private CharacterController cc;
    private Vector3 moveVector;
    private bool fire;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        //cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (player == null) return;
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("Move Vertical");
        fire = player.GetButtonDown("Catch");
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            //cc.Move(moveVector * moveSpeed * Time.deltaTime);
            this.transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        }

        // Process fire
        if (fire)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.right * bulletSpeed, ForceMode.VelocityChange);
        }
    }
}
