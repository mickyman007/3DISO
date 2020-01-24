using System;
using UnityEngine;

public class Space : MonoBehaviour, ISpace {
    private int x;
    private int y;
    private bool isSelected;
    private bool isMoveable;

    private void OnDrawGizmos() {
        Gizmos.color = isSelected? Color.green : Color.red;

        //Gizmos.DrawCube(
        //    new Vector3(x, 1, y),
        //    new Vector3(1, 1, 1));
        Gizmos.DrawWireCube(
            new Vector3(x, transform.position.y + 0.5f, y),
            new Vector3(1, 1, 1));
    }

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

    public bool IsSelected {
        get { return isSelected; }
        set { 
            if(isSelected == value) {
                return;
            }
            isSelected = value;
            if (value) {
                Select();
            } else {
                UnSelect();
            }
        }
    }

    public bool IsMoveable { 
        get { return isMoveable; }
        set { isMoveable = value; }
    }

    public event EventHandler OnSelection;

    private void Select() {
        transform.localScale *= 1.2f;
        Debug.Log("Selected" + transform.name);
        OnSelection(this, new EventArgs());
    }

    private void UnSelect() {
        transform.localScale /= 1.2f;
    }
}
