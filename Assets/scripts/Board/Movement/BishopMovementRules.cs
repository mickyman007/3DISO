using System;
using System.Collections.Generic;

public class BishopMovementRules : IMovementRules {
    public ISpace[] GetLegalMoves(IBoard board, IPiece piece) {
        var pieceSpace = piece.SpaceOccupied;
        var spaces = new List<ISpace>();

        // TODO try not to loop through EVERY space on the board
        foreach (var boardSpace in board.Spaces) {
            var x = boardSpace.X - pieceSpace.X;
            var y = boardSpace.Y - pieceSpace.Y;

            if (Math.Abs(x) == Math.Abs(y)) {
                spaces.Add(boardSpace);
            }
        }

        return spaces.ToArray();
    }
}
