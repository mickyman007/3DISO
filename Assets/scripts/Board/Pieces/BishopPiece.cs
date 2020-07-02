public class BishopPiece : Piece {
    public override void Initialise(ISpace space) {
        SpaceOccupied = space;
        transform.name = "BishopPiece";
        CanMove = true;
        MovementRules = new BishopMovementRules();
    }
}