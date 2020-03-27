using System;

public interface ISpace {
    int X { get; }

    int Y { get; }

    bool IsSelected { get; set; }

    bool IsHighlighted { get; set; }

    bool CanSelect { get; set; }

    bool CanMoveTo { get; set; }

    IBoard Board { get; set; }

    event EventHandler OnSelection;

    void Initialise(int x, int y);
}
