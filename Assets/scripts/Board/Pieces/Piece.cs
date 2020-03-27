using System;
using UnityEngine;

public class Piece : MonoBehaviour, IPiece {
    private ISpace space;
    private bool isSelected;
    private Renderer renderer;
    private Material originalMaterial;

    public ISpace SpaceOccupied { 
        get { return space; }
        set { 
            space = value;
            transform.position = space.GetWorldCoords();
        }
    }

    public bool IsSelected {
        get { return isSelected; }
        set {
            if (isSelected == value) {
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

    public IMovementRules MovementRules { get; set; }
    public ISpace[] MoveableSpaces { get; set; }

    public bool CanMove { get; set; }

    public event EventHandler OnSelection;

    public void Initialise(ISpace space) {
        SpaceOccupied = space;
        transform.name = "Piece";
        CanMove = true;
        MovementRules = new BishopMovementRules();
    }

    void Start() {
        renderer = GetComponent<Renderer>();
        originalMaterial = new Material(renderer.material);
    }

    private void Select() {
        renderer.material.ToggleOutLine(originalMaterial);
        Debug.Log("Selected" + transform.name);
        OnSelection(this, new EventArgs());
    }

    private void Unselect() {
        renderer.material.ToggleOutLine(originalMaterial);
        Debug.Log("Unselected" + transform.name);
        OnSelection(this, new EventArgs());
    }

}
