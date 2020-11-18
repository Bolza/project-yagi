using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D collider;
    private Vector3 moveVector;

    private enum MovementType { useVelocity, useTransform };
    [SerializeField] private MovementType useMovement;
    [SerializeField] private LayerMask groundLayer;
    public float jumpForce;

    private float max=0f;
    // Start is called before the first frame updat
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        moveVector = getMovement();
        if (body.transform.position.y > max)
        {
            max = body.transform.position.y;
           // Debug.Log(max);
        }
    }

    private void FixedUpdate()
    {
       // if (useMovement.Equals(MovementType.useVelocity)) useVelocity();

    }

    void useTransform() {
        transform.position += moveVector / 10;
    }

    void useVelocity()
    {
        body.velocity = moveVector * 10;
    }

    Vector3 getMovement()
    {
        bool grounded = isGrounded();
        float moveX = 0;
        float moveY = 0;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.Space) && grounded) {

            body.velocity = new Vector3(moveX, jumpForce, 0); 
        }
        
        return new Vector3(moveX, moveY, 0);
    }

    private bool isGrounded()
    {
        float downBy = .05f;
        RaycastHit2D cast = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, downBy, groundLayer);
        ExtDebug.DebugHitBox(cast, collider, downBy);
        return cast.collider != null;
    }
}

