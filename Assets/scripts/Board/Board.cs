﻿using System;

public class Board : IBoard {

    /// <summary>
    /// Constructor for the board.
    /// </summary>
    /// <param name="boardSizeX">The board height.</param>
    /// <param name="boardSizeY">The board width.</param>
    public Board(int boardSizeX, int boardSizeY) {
        Spaces = new ISpace[boardSizeX, boardSizeY];
        Pieces = new IPiece[boardSizeX, boardSizeY];
    }

    /// <summary>
    /// Coordinates of spaces on board.
    /// </summary>
    public ISpace[,] Spaces { get; }

    /// <summary>
    /// Coordinates of spaces on board.
    /// </summary>
    public IPiece[,] Pieces { get; }

    /// <summary>
    /// The current <see cref="ISpace"/> selection.
    /// </summary>
    public ISpace SelectedSpace { get; set; }

    /// <summary>
    /// The current <see cref="IPeice"/> selection.
    /// </summary>
    public IPiece SelectedPiece { get; set; }

    /// <summary>
    /// Places the peice on the board.
    /// </summary>
    /// <param name="piece"></param>
    public void Set(IPiece piece) {
        Pieces[piece.SpaceOccupied.X, piece.SpaceOccupied.Y] = piece;
        piece.OnSelection += OnPieceIsSelectedChanged;
    }

    /// <summary>
    /// Places the space on the board.
    /// </summary>
    /// <param name="space"></param>
    public void Set(ISpace space) {
        Spaces[space.X, space.Y] = space;
        space.OnSelection += OnSpaceIsSelectedChanged;
    }

    private void OnPieceIsSelectedChanged(object sender, EventArgs e) {
        var senderPeice = (IPiece)sender;

        if (SelectedPiece == senderPeice) {
            this.DeselectSpace();
            this.DeselectPeice();
            return;
        }

        this.DeselectPeice();
        this.SelectPiece(senderPeice);
        this.SelectSpace(SelectedPiece.SpaceOccupied);
    }

    private void OnSpaceIsSelectedChanged(object sender, EventArgs e) {
        var senderSpace = (ISpace)sender;
        if (SelectedSpace == senderSpace) {
            this.DeselectSpace();
            return;
        }

        this.DeselectSpace();
        this.SelectSpace(senderSpace);

        if (SelectedPiece != null) {
            Pieces[SelectedPiece.SpaceOccupied.X, SelectedPiece.SpaceOccupied.Y] = null;
            SelectedPiece.MoveTo(SelectedSpace);
            Pieces[SelectedSpace.X, SelectedSpace.Y] = SelectedPiece;
            SelectedPiece.RefreshMoveableSpaces(this);
            SelectedPiece.MoveableSpaces.SetCanMoveOnSpaces(true);
        }
    }
}
