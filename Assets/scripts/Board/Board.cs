using System;

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
    /// UThe current <see cref="IPeice"/> selection.
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
        this.DeselectSpace();

        if (SelectedPiece == senderPeice) {
            this.DeselectPeice();
            return;
        }

        this.DeselectPeice();
        this.SelectPeice(senderPeice);
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

        SelectedPiece?.MoveTo(SelectedSpace);
        var adjacentSpaces = this.GetNeighbouringSpaces(SelectedSpace, false);
    }
}
