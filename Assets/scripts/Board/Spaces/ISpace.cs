public interface ISpace {
    int X { get; }
    int Y { get; }
    bool IsSelected { get; set; }
    bool IsMoveable { get; set; }

    //SpaceType Type{ get; set; }
}
