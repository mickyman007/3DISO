/// <summary>
/// Interface for a board.
/// </summary>
public interface IBoard {

    /// <summary>
    /// Coordinates of spaces on board.
    /// </summary>
    ISpace[,] Spaces { get; }

    /// <summary>
    /// Coordinates of pieces on board.
    /// </summary>
    IPiece[,] Pieces { get; }

    /// <summary>
    /// The current <see cref="ISpace"/> selection.
    /// </summary>
    ISpace SelectedSpace { get; set; }

    /// <summary>
    /// The current <see cref="IPeice"/> selection.
    /// </summary>
    IPiece SelectedPiece { get; set; }

    /// <summary>
    /// Places the peice on the board.
    /// </summary>
    /// <param name="piece"></param>
    void Set(IPiece piece);

    /// <summary>
    /// Places the space on the board.
    /// </summary>
    /// <param name="space"></param>
    void Set(ISpace space);
}
