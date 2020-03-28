using System.Collections.Generic;
public class PawnMovementRules : IMovementRules {
    public bool IsFirstMove = true;

    public ISpace[] GetLegalMoves(IBoard board, IPiece piece) {
        var movementDirection = piece.Rotation.GetDirectionFromRotation();
        // TODO Diagonal attack potential

        var pieceSpace = piece.SpaceOccupied;
        var spaces = new List<ISpace>();
        if (board.IsInBounds(
            pieceSpace.X + movementDirection[0],
            pieceSpace.Y + movementDirection[1])) {
            spaces.Add(board.Spaces[pieceSpace.X + movementDirection[0], pieceSpace.Y + movementDirection[1]]);
        }

        if (IsFirstMove &&
            board.IsInBounds(
                pieceSpace.X + (movementDirection[0] * 2),
                pieceSpace.Y + (movementDirection[1]* 2))) {
            spaces.Add(board.Spaces[pieceSpace.X + (movementDirection[0] * 2),
                pieceSpace.Y + (movementDirection[1] * 2)]);
        }

        return spaces.ToArray();
    }
}
