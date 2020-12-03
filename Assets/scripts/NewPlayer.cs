using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer: MonoBehaviour {
    public float toX = 0;
    private CharacterController cc;
    // Start is called before the first frame update
    void Start() {
        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update() {
        cc.Move(new Vector3(toX, 0, 0));
    }
}
