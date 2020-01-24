using UnityEngine;

public static class BoardUtilities {
    public static int[] WorldToCoord(Vector3 position) {
        return new int[] { (int)position.x, (int)position.y };
    }

    public static Vector3 CoordToWorld(int x, int y) {
        return new Vector3(x, 0, y);
    }

    public static ISpace GetSpaceAtPosition(Vector3 position) {
        Collider[] colliders = Physics.OverlapSphere(position, 1f);
        if (colliders.Length > 1) {
            foreach (var collider in colliders) {
                var go = collider.gameObject;
                if (go.TryGetComponent<ISpace>(out var space)) continue;
                return space;
            }
        }
        return null;
    }
}
