using System;
using System.Collections.Generic;

public class QueenMovementRules : IMovementRules {
    public ISpace[] GetLegalMoves(IBoard board, IPiece piece) {
        var pieceSpace = piece.SpaceOccupied;
        var spaces = new List<ISpace>();

        foreach (var boardSpace in board.Spaces) {
            var x = Math.Abs(boardSpace.X - pieceSpace.X);
            var y = Math.Abs(boardSpace.Y - pieceSpace.Y);

            if (x == y ||
                x == 0 || 
                y == 0) {
                spaces.Add(boardSpace);
            }
        }

        return spaces.ToArray();
    }
}
