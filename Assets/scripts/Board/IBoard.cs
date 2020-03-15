public interface IBoard {

    ISpace[,] Spaces { get; }

    /// <summary>
    /// The current <see cref="ISpace"/> selection.
    /// </summary>
    ISpace SelectedSpace { get; set; }

    /// <summary>
    /// The current <see cref="IPeice"/> selection.
    /// </summary>
    IPiece SelectedPiece { get; set; }

    void Set(IPiece piece);

    void Set(ISpace space);
}
