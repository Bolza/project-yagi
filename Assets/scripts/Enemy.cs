using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy: MonoBehaviour {
    // Start is called before the first frame update
    public static readonly int varXSpeed = Animator.StringToHash("xSpeed");
    public virtual void Attack() {

    }

    protected Transform targetWaypoint;

    public int health;
    public int baseDamage;
    public float speed = 1f;
    public int loot;
    [SerializeField] protected Transform pointA, pointB;


    void Hit(int dmg) {
        health -= dmg;
    }

    void Start() {

    }

    // Update is called once per frame
    public abstract void Update();

    void Move() {
        float step = 1 * Time.deltaTime;
        Vector3 to = new Vector2(transform.position.x, transform.position.y);
        if (Input.GetKey(KeyCode.L)) {
            to.x += 1;
        }
        if (Input.GetKey(KeyCode.K)) {
            to.x -= 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, to, step);
    }


}
