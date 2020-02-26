using System;

public class Board : IBoard {
    private ISpace selectedSpace;
    private IPiece selectedPiece;

    public ISpace[,] Spaces { get; private set; }

    public Board(int boardSizeX, int boardSizeY) {
        Spaces = new ISpace[boardSizeX, boardSizeY];
    }

    public ISpace SelectedSpace { 
        get { return selectedSpace; }
        set { 
            selectedSpace.IsSelected = false;
            selectedSpace = value;
        }
    }

    public void Set(IPiece piece) {
        piece.OnSelection += PieceSelected;
    }

    public void Set(ISpace space) {
        Spaces[space.X, space.Y] = space;
        space.OnSelection += SpaceSelected;
    }

    private void PieceSelected(object sender, EventArgs e) {
        selectedPiece = (IPiece)sender;

        if (selectedSpace != null) {
            selectedPiece.MoveTo(selectedSpace);
        }
    }

    private void SpaceSelected(object sender, EventArgs e) {
        if(selectedSpace != null && selectedSpace != sender) {
            selectedSpace.IsSelected = false;
        }
        selectedSpace = (ISpace)sender;
        selectedPiece?.MoveTo(selectedSpace);
        var adjacentSpaces = this.GetNeighbouringSpaces(selectedSpace, false);
    }
}
