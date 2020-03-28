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
        return new Vector3(space.X, 1, space.Y);
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
    /// <param name="board">The board.</param>
    /// <param name="piece">The piece to select.</param>
    /// <remarks>Opposite of <see cref="DeselectSpace"/>.</remarks>
    public static void SelectPiece(this IBoard board, IPiece piece) {
        board.SelectedPiece = piece;
        piece.IsSelected = true;

        piece.MoveableSpaces = piece.MovementRules.GetLegalMoves(board, piece);
        piece.MoveableSpaces.SetCanMoveOnSpaces(true);
    }

    /// <summary>
    /// Sets <paramref name="board"/> SelectedPiece.IsSelected to false, and removes from board selection.
    /// </summary>
    /// /// <remarks>Opposite of <see cref="SelectSpace"/>.</remarks>
    public static void DeselectPeice(this IBoard board) {
        var piece = board.SelectedPiece;

        if (piece != null) {
            piece.IsSelected = false;
            piece.MoveableSpaces.SetCanMoveOnSpaces(false);
        }
        board.SelectedPiece = null;
    }

    public static void SetCanMoveOnSpaces(this ISpace[] spaces, bool canMove) {
        foreach (var space in spaces) {
            space.CanMoveTo = canMove;
        }
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
            piece.MoveableSpaces.SetCanMoveOnSpaces(false);
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

    public static bool IsInBounds(this IBoard board, int x, int y) {
        return x <= board.Spaces.GetLength(0) - 1 && x >= 0 && y <= board.Spaces.GetLength(1) - 1 && y >= 0;
    }

    public static int[] GetDirectionFromRotation(this Rotation rotation) {
        switch (rotation) {
            case Rotation.North:
                return new[] { 1, 0 };
            case Rotation.East:
                return new[] { 0, 1 };
            case Rotation.South:
                return new[] { -1, 0 };
            case Rotation.West:
                return new[] { 0, -1 };
        }

        return new[] { 0, 0 };
    }

    private static ISpace[] GetAdjacentSpaces(IBoard board, ISpace targetSpace) {
        var spaces = new ISpace[4];

        int index = 0;
        for(int i = -1; i < 2; i++) {
            if (i != 0) {
                if(board.IsInBounds(targetSpace.X + i, targetSpace.Y)) {
                    spaces[index] = board.Spaces[targetSpace.X + i, targetSpace.Y];
                }

                if(board.IsInBounds(targetSpace.X, targetSpace.Y + i)) {
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
                        if(board.IsInBounds(x, y)) {
                            spaces[index] = board.Spaces[x, y];
                        }
                        index++;
                    }
                }
            }
        }

        return spaces;
    }
}
