public class PawnPiece : Piece {
    public override void Initialise(ISpace space) {
        SpaceOccupied = space;
        transform.name = "Piece";
        CanMove = true;
        MovementRules = new PawnMovementRules();

        (MovementRules as PawnMovementRules).IsFirstMove = false;
    }
}