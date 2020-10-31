using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
    public delegate void NoneParamFunction();

    public static void SetActiveFast(this GameObject o, bool s) {
        if (o.activeSelf != s) {
            o.SetActive(s);
        }
    }
}