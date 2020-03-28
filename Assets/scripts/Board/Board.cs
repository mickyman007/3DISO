﻿using System;

public class Board : IBoard {
    public ISpace[,] Spaces { get; private set; }

    public Board(int boardSizeX, int boardSizeY) {
        Spaces = new ISpace[boardSizeX, boardSizeY];
    }

    /// <summary>
    /// The current <see cref="ISpace"/> selection.
    /// </summary>
    public ISpace SelectedSpace { get; set; }

    /// <summary>
    /// UThe current <see cref="IPiece"/> selection.
    /// </summary>
    public IPiece SelectedPiece { get; set; }

    public void Set(IPiece piece) {
        piece.OnSelection += OnPieceIsSelectedChanged;
    }

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
            SelectedPiece.MoveTo(SelectedSpace);
            SelectedPiece.RefreshMoveableSpaces(this);
            SelectedPiece.MoveableSpaces.SetCanMoveOnSpaces(true);
        }
    }
}
