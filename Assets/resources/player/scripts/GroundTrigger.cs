using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger: MonoBehaviour {

    [SerializeField] private string groundTag;
    public event EventHandler OnGroundEnter;
    public event EventHandler OnGroundExit;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == groundTag) {
            OnGroundEnter?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == groundTag) {
            OnGroundExit?.Invoke(this, EventArgs.Empty);
        }
    }
}