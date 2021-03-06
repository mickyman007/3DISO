﻿using System;

public interface IPiece {
    ISpace SpaceOccupied { get; set; }

    bool IsSelected { get; set; }

    bool CanMove { get; set; }

    Rotation Rotation { get; set; }

    IMovementRules MovementRules { get; set; }

    string Team { get; set; }

    ISpace[] MoveableSpaces { get; set; }

    event EventHandler OnSelection;

    void RefreshMoveableSpaces(IBoard board);

    void Initialise(ISpace space);
}
