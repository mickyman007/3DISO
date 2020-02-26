using System;
using UnityEngine;

public class SpaceWall : Space {
    private bool isSelected;

    public new void Initialise(int x, int y) {
        base.Initialise(x, y);
        CanSelect = false;
        CanMove = false;
    }
}
