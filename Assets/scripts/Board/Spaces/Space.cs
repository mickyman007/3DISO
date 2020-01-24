﻿using System;
using UnityEngine;

public class Space : MonoBehaviour, ISpace {
    private bool isSelected;

    public int X { get; private set; }

    public int Y { get; private set; }

    public void Initialise(int x, int y) {
        this.X = x;
        this.Y = y;
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
                Unselect();
            }
        }
    }

    public bool IsMoveable { get; set; }

    public event EventHandler OnSelection;

    private void Select() {
        transform.localScale *= 1.2f;
        Debug.Log("Selected" + transform.name);
        OnSelection(this, new EventArgs());
    }

    private void Unselect() {
        transform.localScale /= 1.2f;
        Debug.Log("Unselected" + transform.name);
    }

    void OnDrawGizmos() {
        Gizmos.color = isSelected ? Color.green : Color.red;

        Gizmos.DrawWireCube(
            new Vector3(X, transform.position.y + 0.5f, Y),
            new Vector3(0.5f, 1, 0.5f));
    }
}
