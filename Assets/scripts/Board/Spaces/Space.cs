﻿using System;
using UnityEngine;

public class Space : MonoBehaviour, ISpace {
    private bool isSelected;
    private bool isHighlighted;

    private Renderer renderer;
    private Material originalMaterial;

    public int X { get; private set; }

    public int Y { get; private set; }

    public IBoard Board { get; set; }

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

    public bool IsHighlighted {
        get { return isHighlighted; }
        set {
            if (isHighlighted != value) {
                isHighlighted = value;
                renderer.material.ToggleOutLine(originalMaterial);
            }
            
        }
    }

    public bool CanSelect { get; set; }

    public bool CanMoveTo { get; set; }

    public event EventHandler OnSelection;
    public virtual void Initialise(int x, int y) {
        this.X = x;
        this.Y = y;
        transform.name = this.GetType().Name + "(" + x + ", " + y + ")";
        CanSelect = true;
        CanMoveTo = true;
    }

    void Start() {
        renderer = GetComponent<Renderer>();
        originalMaterial = new Material(renderer.material);
    }

    public void OnMouseEnter() {
    }

    public void OnMouseExit() {
    }

    private void Select() {
        transform.Expand(1.2f);
        renderer.material.ToggleOutLine(originalMaterial);
        Debug.Log("Selected" + transform.name);
        OnSelection(this, new EventArgs());
    }

    private void Unselect() {
        transform.Shrink(1.2f);
        renderer.material.ToggleOutLine(originalMaterial);
        Debug.Log("Unselected" + transform.name);
        OnSelection(this, new EventArgs());
    }

    void OnDrawGizmos() {
        Gizmos.color = isSelected ? Color.green : Color.red;

        if (isHighlighted) {
            Gizmos.color = Color.blue;
        }

        Gizmos.DrawWireCube(
            new Vector3(X, transform.position.y + 0.5f, Y),
            new Vector3(0.5f, 1, 0.5f));
    }
}
