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

    // The rotation speed of this character
    public float rotationSpeed = 3.0f;

    // The bullet speed
    public float bulletSpeed = 15.0f;
    
    

    // Assign a prefab to this in the inspector.
    // The prefab must have a Rigidbody component on it in order to work.
    public GameObject bulletPrefab;

    public GameObject current_face;
    public GameObject hold_block;
    private Rewired.Player player; // The Rewired Player
    private CharacterController cc;
    private Vector3 moveVector;
    private bool use;
    private bool is_hold;
    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        is_hold = false;
        // Get the character controller
        //cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (player == null) return;
        GetInput();
        ProcessInput();
        // if(current_face != null) {
        //     print(current_face.name);
        // }
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("Move Vertical");
        use = player.GetButtonDown("Catch");

    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            float inputMagnitude = Mathf.Clamp01(moveVector.magnitude);
            moveVector.Normalize();

            this.transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
            //cc.Move(moveVector * moveSpeed * Time.deltaTime);

            if(moveVector != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveVector);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

        }

        // Process fire
        if (use)
        {
            if(is_hold == false && current_face.name == "box"){
                print("hit box");
                hold_block = current_face;
                is_hold = true;
            }
            // GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
            // bullet.GetComponent<Rigidbody>().AddForce(transform.right * bulletSpeed, ForceMode.VelocityChange);
        }
    }
}
