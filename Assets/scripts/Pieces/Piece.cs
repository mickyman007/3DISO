using System;
using UnityEngine;

public class Piece : MonoBehaviour, IPiece {
    private ISpace space;
    private bool isSelected;

    public ISpace SpaceOccupied { 
        get { return space; }
        set { space = value; }
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

    public bool CanMove { get; set; }

    public event EventHandler OnSelection;

    public void Initialise(ISpace space) {
        SpaceOccupied = space;
        transform.name = "Piece";
        CanMove = true;
    }

    // Update is called once per frame
    void Update() {
        if (space != null) {
            this.transform.position = space.GetWorldCoords();
        }
    }

    private void Select() {
        transform.localScale *= 1.2f;
        Debug.Log("Selected" + transform.name);
        OnSelection(this, new EventArgs());
    }

    private void Unselect() {
        transform.localScale /= 1.2f;
        Debug.Log("Unselected" + transform.name);
    }

}
