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
    public float rotSpeed = 3.0f;

    // The rotation speed of this character
    public float rotationSpeed = 3.0f;

    // The bullet speed
    public float bulletSpeed = 15.0f;



    // Assign a prefab to this in the inspector.
    // The prefab must have a Rigidbody component on it in order to work.
    public GameObject bulletPrefab;

    public Transform GetObjectPositionRoot;
    public Transform GetObjectPosition;

    private Rewired.Player player; // The Rewired Player
    private Vector3 moveVector;
    private Vector3 lastMoveVector;
    private bool fire;
    private Collider2D collider;
    private GameObject pickedObject;
    private float rotateSpeed = 0.1f;
    [SerializeField] private float yVelocity = 0.0f;

    public Tilemap floormap;
    public Tilemap boxmap;
    public Tile floor;
    public Tile box;
    public TileBase current_face;
    public GameObject hold_block;
    private PlayerInteract interact_block;
    private bool use;
    private bool is_hold;
    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        is_hold = false;
        interact_block = gameObject.GetComponentInChildren<PlayerInteract>();
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (player == null) return;
        GetInput();
        handleInteraction();
        ProcessInput();

        float z = 0;
        Vector3 v3 = lastMoveVector.normalized;
        if (v3.x > 0)
        {
            //z = -Vector3.Angle(Vector3.right, v3);
            //SmoothRotationY(90);
        }
        else
        {
            //z = Vector3.Angle(Vector3.right, v3);
        }
        if (v3.y > 0)
        {
            //z = -Vector3.Angle(Vector3.up, v3);
        }
        else
        {
            //z = Vector3.Angle(Vector3.up, v3);
        }
        //if (GetObjectPositionRoot)
        //{
        //    GetObjectPositionRoot.eulerAngles = new Vector3(0, 0, z);
        //    GetObjectPositionRoot.forward = Vector3.Lerp(GetObjectPositionRoot.forward, moveVector, Time.deltaTime);
        //}
    }

    public void SmoothRotationY(float iTargetAngle)
    {
        GetObjectPositionRoot.eulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(transform.eulerAngles.y, iTargetAngle, ref yVelocity, rotateSpeed));
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

            if (moveVector != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveVector);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

        }

        // Process fire
        if (use && pickedObject == null)
        {
            pick();
        }
        else if (use && pickedObject != null)
        {
            putDown();
        }
    }

    private void handleInteraction()
    {
        if (moveVector != Vector3.zero)
        {
            lastMoveVector = moveVector;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, 1, 1 << LayerMask.NameToLayer("Object"));
        if (hit)
        {
            Debug.Log(hit.collider.name);
            collider = hit.collider;
        }
        else
        {
            collider = null;
            if (current_face)
            {
                print(current_face.name);
                interact_block.SetTile(null);
            }
        }
    }

    private void pick()
    {
        Debug.Log("pick");
        if (collider == null) return;
        if (GetObjectPositionRoot)
            collider.transform.SetParent(GetObjectPositionRoot);
        collider.transform.localPosition = Vector3.zero;
        collider.GetComponent<BoxCollider2D>().enabled = false;
        pickedObject = collider.gameObject;
    }

    private void putDown()
    {
        Debug.Log("putDown");
        pickedObject.transform.parent = null;
        pickedObject.GetComponent<BoxCollider2D>().enabled = true;
        pickedObject = null;
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = new Vector3(0, 0, 0);
        Vector3 direction = new Vector3(1, 0, 0);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawRay(origin, moveVector);
    }
}
