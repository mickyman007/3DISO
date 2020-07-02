using System;
using System.Collections.Generic;

public class BishopMovementRules : IMovementRules {

    private Func<int, int> increment = new Func<int, int>(x => ++x);
    private Func<int, int> deincrement = new Func<int, int>(x => --x);

    public ISpace[] GetLegalMoves(IBoard board, IPiece piece) {
        var pieceSpace = piece.SpaceOccupied;
        var spaces = new List<ISpace>();

        spaces.AddRange(GetDiagonalSpaces(board, pieceSpace, deincrement, increment));
        spaces.AddRange(GetDiagonalSpaces(board, pieceSpace, increment, increment));
        spaces.AddRange(GetDiagonalSpaces(board, pieceSpace, deincrement, deincrement));
        spaces.AddRange(GetDiagonalSpaces(board, pieceSpace, increment, deincrement));

        return spaces.ToArray();
    }

    private IEnumerable<ISpace> GetDiagonalSpaces(IBoard board, ISpace space, Func<int, int> incrementX, Func<int, int> incrementY) {
        var spaces = new List<ISpace>();

        var x = space.X;
        var y = space.Y;

        var piece = board.Pieces[x, y];

        x = incrementX(x);
        y = incrementY(y);

        while (board.IsInBounds(x, y)) {
            if (board.Pieces[x, y] == null) {
                spaces.Add(board.Spaces[x, y]);
            } else {
                if (!board.Pieces[x, y].Team.Equals(piece.Team)) {
                    spaces.Add(board.Spaces[x, y]);
                }
                break;
            }

            x = incrementX(x);
            y = incrementY(y);
        }

        return spaces;
    }
}
