using System;
using UnityEngine;

public static class BoardUtilities {
    public static int[] WorldToCoord(Vector3 position) {
        return new int[] { (int)position.x, (int)position.y };
    }

    public static Vector3 CoordToWorld(int x, int y) {
        return new Vector3(x, 0, y);
    }

    public static Vector3 GetWorldCoords(this ISpace space) {
        return new Vector3(space.X, 0, space.Y);
    }

    /// <summary>
    /// Sets <paramref name="space"/> IsSelected to true, and adds to <paramref name="board"/> selection.
    /// </summary>
    /// <param name="board">The board</param>
    /// <param name="space"></param>
    /// <remarks>Opposite of <see cref="DeselectSpace"/>.</remarks>
    public static void SelectSpace(this IBoard board, ISpace space) {
        board.SelectedSpace = space;
        space.IsSelected = true;
    }

    /// <summary>
    /// Sets <paramref name="board"/>'s SelectedSpace.IsSelected to false, and removes from board selection.
    /// </summary>
    /// /// <remarks>Opposite of <see cref="SelectSpace"/>.</remarks>
    public static void DeselectSpace(this IBoard board) {
        if (board.SelectedSpace != null) {
            board.SelectedSpace.IsSelected = false;
        }
        board.SelectedSpace = null;
    }

    /// <summary>
    /// Sets <paramref name="space"/> IsSelected to true, and adds to <paramref name="board"/> selection.
    /// </summary>
    /// <param name="board">The board</param>
    /// <param name="space"></param>
    /// <remarks>Opposite of <see cref="DeselectSpace"/>.</remarks>
    public static void SelectPeice(this IBoard board, IPiece peice) {
        board.SelectedPiece = peice;
        peice.IsSelected = true;

        //highlight legal moves for this piece
        peice.MoveableSpaces = peice.MovementRules.GetLegalMoves(board, peice);
        foreach (var moveableSpace in peice.MoveableSpaces) {
            moveableSpace.IsHighlighted = true;
        }
    }

    /// <summary>
    /// Sets <paramref name="board"/> SelectedPiece.IsSelected to false, and removes from board selection.
    /// </summary>
    /// /// <remarks>Opposite of <see cref="SelectSpace"/>.</remarks>
    public static void DeselectPeice(this IBoard board) {
        if (board.SelectedPiece != null) {
            board.SelectedPiece.IsSelected = false;
        }
        board.SelectedPiece = null;
    }

    public static ISpace GetSpaceAtPosition(Vector3 position) {
        Collider[] colliders = Physics.OverlapSphere(position, 1f);
        if (colliders.Length > 1) {
            foreach (var collider in colliders) {
                var go = collider.gameObject;
                if (go.TryGetComponent<ISpace>(out var space)) continue;
                return space;
            }
        }
        return null;
    }

    public static bool MoveTo(this IPiece piece, ISpace destination) {
        if(piece.CanMove && destination.CanMoveTo) {
            piece.SpaceOccupied = destination;
            return true;
        }

        return false;
    }

    public static ISpace[] GetNeighbouringSpaces(this IBoard board, ISpace targetSpace, bool includeDiagonal) {
        var k = includeDiagonal ? 8 : 4;

        ISpace[] neighbours = new ISpace[k];

        if(includeDiagonal) {
            Array.Copy(GetAdjacentSpaces(board, targetSpace), neighbours, 4);
            Array.Copy(GetDiagonalSpaces(board, targetSpace), 0, neighbours, 4, 4);
        } else {
            Array.Copy(GetAdjacentSpaces(board, targetSpace), neighbours, 4);
        }

        return neighbours;
    }

    private static ISpace[] GetAdjacentSpaces(IBoard board, ISpace targetSpace) {
        var spaces = new ISpace[4];

        int index = 0;
        for(int i = -1; i < 2; i++) {
            if (i != 0) {
                if(IsInBounds(targetSpace.X + i, targetSpace.Y, board)) {
                    spaces[index] = board.Spaces[targetSpace.X + i, targetSpace.Y];
                }

                if(IsInBounds(targetSpace.X, targetSpace.Y + i, board)) {
                    spaces[index + 1] = board.Spaces[targetSpace.X, targetSpace.Y + i];
                }

                index += 2;
            }
        }

        return spaces;
    }

    private static ISpace[] GetDiagonalSpaces(IBoard board, ISpace targetSpace) {
        var spaces = new ISpace[4];

        int index = 0;
        for (int x = -1; x < 2; x++) {
            if (x != targetSpace.X) {
                for(int y = -1; y < 2; y++) {
                    if(y != targetSpace.Y) {
                        if(IsInBounds(x, y, board)) {
                            spaces[index] = board.Spaces[x, y];
                        }
                        index++;
                    }
                }
            }
        }

        return spaces;
    }

    private static bool IsInBounds(int x, int y, IBoard board) {
        return x <= board.Spaces.GetLength(0)-1 && x >= 0 && y <= board.Spaces.GetLength(1)-1 && y >= 0;
    }
}
