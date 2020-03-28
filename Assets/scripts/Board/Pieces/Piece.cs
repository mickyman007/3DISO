using System;
using UnityEngine;

public class Piece : MonoBehaviour, IPiece {
    private ISpace space;
    private bool isSelected;
    private Renderer renderer;
    private Material originalMaterial;
    private Rotation rotation;

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

    public Rotation Rotation {
        get { return rotation; }
        set {
            rotation = value;
            this.transform.rotation = Quaternion.Euler(0, (int) value, 0);
        }
    }

    public event EventHandler OnSelection;

    public void RefreshMoveableSpaces(IBoard board) {
        MoveableSpaces = MovementRules.GetLegalMoves(board, this);
    }

    public virtual void Initialise(ISpace space) {
        SpaceOccupied = space;
        transform.name = "Piece";
        CanMove = true;
        MovementRules = new QueenMovementRules();
    }

    void Awake() {
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

    void OnDrawGizmos() {
        Gizmos.color = isSelected ? Color.green : Color.red;

        Gizmos.DrawWireSphere(new Vector3(
            transform.position.x, 
            transform.position.y, 
            transform.position.z), 0.5f);

        var movementDirection = Rotation.GetDirectionFromRotation();

        Gizmos.DrawWireCube(new Vector3(
                transform.position.x + movementDirection[0],
                transform.position.y,
                transform.position.z + movementDirection[1]),
            new Vector3(movementDirection[0], 0.2f, movementDirection[1]));
    }
}
