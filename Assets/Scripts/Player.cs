using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    // The Rewired player id of this character
    public int playerId;

    // The movement speed of this character
    public float moveSpeed = 3.0f;
    public float acceleration  = 1000.0f;
    public float rotSpeed = 3.0f;

    // The rotation speed of this character
    public float rotationSpeed = 3.0f;

    public Transform interact_block_root;

    private Rewired.Player player; // The Rewired Player
    private Vector3 moveVector;
    private Vector3 lastMoveVector;
    public GameObject pickedObject;
    [SerializeField] private float topSpeed = 10.0f;


    public Tilemap floormap;
    public Tilemap boxmap;
    public Tile floor;
    public TileBase box;
    public TileBase current_face_tile;
    public GameObject current_face_object;
    public GameObject hold_block;
    private Rigidbody2D rb;
    private PlayerInteract interact_block;
    private Vector3 last_position;
    private bool _catch;
    private bool is_hold;
    private bool use;
    private SpriteRenderer sprite_renderer;
    private Animator animator;

    private bool is_collision;
    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        is_hold = false;
        interact_block = gameObject.GetComponentInChildren<PlayerInteract>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        boxmap = RootsManager.instance.map;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (player == null) return;
        GetInput();
        handleInteraction();
        ProcessInput();


    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("Move Vertical");
        _catch = player.GetButtonDown("Catch");
        use = player.GetButtonDown("Use");
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            float inputMagnitude = Mathf.Clamp01(moveVector.magnitude);
            moveVector.Normalize();
            // rb.MovePosition(transform.position + moveVector * moveSpeed * Time.deltaTime);
            // this.transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
            rb.AddForce(moveVector * acceleration * Time.deltaTime);
            if (rb.velocity.magnitude > topSpeed)
                rb.velocity = rb.velocity.normalized * topSpeed;
            //cc.Move(moveVector * moveSpeed * Time.deltaTime);

            if (moveVector != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveVector);
                interact_block.transform.parent.rotation = Quaternion.RotateTowards(interact_block.transform.parent.rotation, toRotation, rotationSpeed * Time.deltaTime);
                interact_block.transform.localPosition = new Vector3(0, 0.8f, 0);
            }

        }

        // Process fire
        if (_catch && pickedObject == null)
        {
            pick();
        }
        else if (_catch && pickedObject != null)
        {
            putDown();
        }

        if (use && pickedObject != null)
        {
            if (pickedObject.tag == "Kettle")
            {
                // print("use Kettle");
                if (RootsManager.instance.CanCreateRoot(interact_block.transform.position))
                {
                    // print("Can create root");
                    RootsManager.instance.CreateRoot(interact_block.transform.position);
                }
            }
            else if (pickedObject.tag == "Weapon")
            {
                animator.SetTrigger("wave");
                pickedObject.GetComponent<ObjectSkill>().UseSkill(interact_block.gameObject);
            }
        }
    }

    private void handleInteraction()
    {
        if (moveVector != Vector3.zero)
        {
            lastMoveVector = moveVector;
        }
        if (pickedObject) pickedObject.GetComponent<Object>().SetDirection(lastMoveVector.normalized);
        Vector3 v3 = lastMoveVector.normalized;
        if (v3.x > 0)
        {
            sprite_renderer.flipX = true;
           
        }
        else
        {
            sprite_renderer.flipX = false;
        }
        if (v3.y > 0)
        {
            //z = -Vector3.Angle(Vector3.up, v3);
        }
        else
        {
            //z = Vector3.Angle(Vector3.up, v3);
        }
    }

    private void pick()
    {
        // Debug.Log("pick");
        if (current_face_object == null) return;
        if (current_face_object.layer != 6) return; // Object layer
        pickedObject = current_face_object;
        current_face_object.GetComponent<BoxCollider2D>().enabled = false;
        pickedObject.transform.SetParent(interact_block.transform);
        pickedObject.transform.localPosition = Vector3.zero;
        pickedObject.transform.localRotation = Quaternion.identity;
        Object obj = pickedObject.GetComponent<Object>();
        if (obj && interact_block.gameObject) pickedObject.GetComponent<Object>().SetOwner(interact_block.gameObject,lastMoveVector.normalized);
    }

    private void putDown()
    {
        // Debug.Log("putDown");
        pickedObject.transform.parent = null;
        pickedObject.transform.rotation = Quaternion.identity;
        pickedObject.GetComponent<BoxCollider2D>().enabled = true;
        Object obj = pickedObject.GetComponent<Object>();
        if (obj) pickedObject.GetComponent<Object>().SetOwner(null, lastMoveVector.normalized);
        pickedObject = null;
    }

    private void OnDrawGizmos()
    {
        //if (interact_block == null) return;
        Vector3 origin = new Vector3(0, 0, 0);
        Vector3 direction = new Vector3(1, 0, 0);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawLine(Vector3.zero, moveVector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obstacle")
            is_collision = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obstacle")
            is_collision = false;
    }
}
