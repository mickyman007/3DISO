using UnityEngine;

/// <summary>
/// Interface for setting boards.
/// </summary>
public interface IBoardSetter {

    /// <summary>
    /// Gets or sets the board to be set.
    /// </summary>
    IBoard Board {get; set;}

    /// <summary>
    /// Sets up the board.
    /// </summary>
    void SetupBoard();

    /// <summary>
    /// Sets a peice on the space.
    /// </summary>
    /// <param name="peice">The peice prefab to be set.</param>
    /// <param name="space">The space where the peice will be set.</param>
    /// <returns>The set peice.</returns>
    GameObject SetPiece(GameObject peice, ISpace space);

    /// <summary>
    /// Sets a space at the x and y values.
    /// </summary>
    /// <param name="space">The space prefab to set.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>The set space.</returns>
    GameObject SetSpace(GameObject space, int x, int y);
}
