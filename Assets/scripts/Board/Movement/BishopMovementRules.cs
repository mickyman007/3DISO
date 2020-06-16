using System;
using System.Collections.Generic;

public class BishopMovementRules : IMovementRules {
    public ISpace[] GetLegalMoves(IBoard board, IPiece piece) {
        var pieceSpace = piece.SpaceOccupied;
        var spaces = new List<ISpace>();

        spaces.AddRange(getNorthEastSpaces(board, pieceSpace));
        spaces.AddRange(getNorthWestSpaces(board, pieceSpace));
        spaces.AddRange(getSouthEastSpaces(board, pieceSpace));
        spaces.AddRange(getSouthWestSpaces(board, pieceSpace));

        return spaces.ToArray();
    }

    private IEnumerable<ISpace> getNorthWestSpaces(IBoard board, ISpace space) {
        var spaces = new List<ISpace>();

        var x = space.X - 1;
        var y = space.Y + 1;

        while (board.IsInBounds(x, y)) {
            if (board.Pieces[x, y] == null) {
                spaces.Add(board.Spaces[x, y]);
            } else {
                break;
            }

            x--;
            y++;
        }

        return spaces;
    }

    private IEnumerable<ISpace> getNorthEastSpaces(IBoard board, ISpace space) {
        var spaces = new List<ISpace>();

        var x = space.X + 1;
        var y = space.Y + 1;

        while (board.IsInBounds(x, y)) {
            if (board.Pieces[x, y] == null) {
                spaces.Add(board.Spaces[x, y]);
            } else {
                break;
            }

            x++;
            y++;
        }

        return spaces;
    }

    private IEnumerable<ISpace> getSouthWestSpaces(IBoard board, ISpace space) {
        var spaces = new List<ISpace>();

        var x = space.X - 1;
        var y = space.Y - 1;

        while (board.IsInBounds(x, y)) {
            if (board.Pieces[x, y] == null) {
                spaces.Add(board.Spaces[x, y]);
            } else {
                break;
            }

            x--;
            y--;
        }

        return spaces;
    }

    private IEnumerable<ISpace> getSouthEastSpaces(IBoard board, ISpace space) {
        var spaces = new List<ISpace>();

        var x = space.X + 1;
        var y = space.Y - 1;

        while (board.IsInBounds(x, y)) {
            if (board.Pieces[x, y] == null) {
                spaces.Add(board.Spaces[x, y]);
            } else {
                break;
            }

            x++;
            y--;
        }

        return spaces;
    }
}
