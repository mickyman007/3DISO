using System;
using System.Collections.Generic;

public class KnightMovementRules : IMovementRules {

    public ISpace[] GetLegalMoves(IBoard board, IPiece piece) {
        var pieceSpace = piece.SpaceOccupied;
        var spaces = new List<ISpace>();

        foreach (var boardSpace in board.Spaces) {
            var x = boardSpace.X - pieceSpace.X;
            var y = boardSpace.Y - pieceSpace.Y;

            if (Math.Abs(x) == 1 && Math.Abs(y) == 2 ||
                Math.Abs(x) == 2 && Math.Abs(y) == 1) {
                spaces.Add(boardSpace);
            }
        }

        return spaces.ToArray();
    }
}
