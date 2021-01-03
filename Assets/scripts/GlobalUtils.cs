using System;
using UnityEngine;
using System.Collections.Generic;


public static class GlobalUtils {


    public static GameObject FindGameObjectChildWithTag(this GameObject parent, string tag) {
        Transform t = parent.transform;
        foreach (Transform tr in t) {
            if (tr.tag == tag) {
                return tr.gameObject;
            }
        }
        return null;
    }
}
