using System;

public class Board : IBoard {
    private ISpace selectedSpace;

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

    public void Set(ISpace space) {
        Spaces[space.X, space.Y] = space;
        space.OnSelection += SpaceSelected;
    }

    private void SpaceSelected(object sender, EventArgs e) {
        if(selectedSpace != null && selectedSpace != sender) {
            selectedSpace.IsSelected = false;
        }
        selectedSpace = (ISpace)sender;
    }
}
