using UnityEngine;

public class Space : MonoBehaviour, ISpace {
    private int x;
    private int y;
    private bool isMoveable;

    public int X { 
        get { return x; }
    }

    public int Y {
        get { return y; }
    }

    public void Initialise(int x, int y) {
        this.x = x;
        this.y = y;
        transform.name = typeof(Space).ToString() + "(" + x + ", " + y + ")";
    }

    public bool IsSelected { get; set; }

    public bool IsMoveable { 
        get { return isMoveable; }
        set { isMoveable = value; }
    }
}
