using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Effects {
    public static void Expand(this Transform transform, float mult) {
        transform.localScale *= mult;
    }

    public static void Shrink(this Transform transform, float mult) {
        transform.localScale /= mult;
    }
}
