using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
    private ISpace selectedSpace;

    public IList<ISpace> Spaces;

    public Board() {
        Spaces = new List<ISpace>();
    }

    public ISpace SelectedSpace { 
        get { return selectedSpace; }
        set { 
            selectedSpace.IsSelected = false;
            selectedSpace = value;
        }
    }

    public void Add(ISpace space) {
        Spaces.Add(space);
    }
}
