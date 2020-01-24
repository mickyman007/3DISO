using UnityEngine;

public static class BoardUtilities {
    public static int[] WorldToCoord(Vector3 position) {
        return new int[] { (int)position.x, (int)position.y };
    }

    public static Vector3 CoordToWorld(int x, int y) {
        return new Vector3(x, 0, y);
    }
}
