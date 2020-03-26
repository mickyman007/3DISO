using System;

public interface IPiece {
    ISpace SpaceOccupied { get; set; }

    bool IsSelected { get; set; }

    bool CanMove { get; set; }

    int[,] Movement { get; }

    event EventHandler OnSelection;

    void Initialise(ISpace space);
}
