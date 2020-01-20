public class Space : ISpace {
    private int x;
    private int y;
    private bool isMoveable;

    public int X { 
        get { return x; }
    }

    public int Y {
        get { return y; }
    }

    public Space(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public bool IsSelected { get; set; }

    public bool IsMoveable { 
        get { return isMoveable; }
        set { isMoveable = value; }
    }
}
