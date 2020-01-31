using System;

public interface ISpace {
    int X { get; }

    int Y { get; }

    bool IsSelected { get; set; }

    bool CanSelect { get; set; }

    bool CanMove { get; set; }

    event EventHandler OnSelection;

    //SpaceType Type{ get; set; }
}
