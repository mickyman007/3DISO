public interface IBoard {

    ISpace[,] Spaces { get; }

    ISpace SelectedSpace { get; }

    void Set(IPiece piece);

    void Set(ISpace space);
}
