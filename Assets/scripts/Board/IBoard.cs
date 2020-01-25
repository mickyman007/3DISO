public interface IBoard {

    ISpace[,] Spaces { get; }

    ISpace SelectedSpace { get; }

    void Set(ISpace space);
}
