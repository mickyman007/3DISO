using System;
using System.Collections.Generic;
using System.Linq;

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
        space.OnSelection += SpaceSelected;
    }

    private void SpaceSelected(object sender, EventArgs e) {
        Spaces.Where(x => x.IsSelected && x != sender).ToList<ISpace>().ForEach(x => x.IsSelected = false);
    }
}
