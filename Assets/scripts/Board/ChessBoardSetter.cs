using System.Collections.Generic;
using UnityEngine;

public class ChessBoardSetter : MonoBehaviour, IBoardSetter {

    private char[,] boardLayout;
    private string[,] pieceLayout;

    private int gridSizeX;
    private int gridSizeY;
    private GameObject emptySpace;

    private Dictionary<char, GameObject> spaceDictionary;
    private Dictionary<string, GameObject> pieceDictionary;

    /// <summary>
    /// Gets or sets the board to be set.
    /// </summary>
    public IBoard Board { get; set; }

    /// <summary>
    /// Sets up the board.
    /// </summary>
    public void SetupBoard() {
        Board = new Board(gridSizeX, gridSizeY);

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                var spaceGo = SetSpace(spaceDictionary[boardLayout[x, y]], x, y);
                var space = spaceGo.GetComponent<ISpace>();
                Board.Set(space);

                try { 
                    var pieceGo = SetPiece(pieceDictionary[pieceLayout[space.X, space.Y]], space);
                    if (pieceGo != null) {
                        Board.Set(pieceGo.GetComponent<IPiece>());
                    }
                } catch {
                    Debug.Log("No piece found at " + space.X + ", " + space.Y);
                }            
            }
        }
    }

    /// <summary>
    /// Sets a peice on the space.
    /// </summary>
    /// <param name="peice">The peice prefab to be set.</param>
    /// <param name="space">The space where the peice will be set.</param>
    /// <returns>The set peice.</returns>
    public GameObject SetPiece(GameObject peice, ISpace space) {
        GameObject pieceGo = null;
        try {
            pieceGo = Instantiate(peice, space.GetWorldCoords(),
                Quaternion.Euler(0, 0, 0));
            var piece = pieceGo.GetComponent<IPiece>();
            piece.Initialise(space);
            piece.MoveableSpaces = piece.MovementRules.GetLegalMoves(Board, piece);
            if(pieceLayout[space.X, space.Y].StartsWith("W")) {
                piece.Rotation = Rotation.South;
            } else {
                piece.Rotation = Rotation.North;
            }
        } catch {
            Debug.Log("Error setting piece at " + space.X + ", " + space.Y);
        }

        return pieceGo;
    }

    /// <summary>
    /// Sets a space at the x and y values.
    /// </summary>
    /// <param name="space">The space prefab to set.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>The set space.</returns>
    public GameObject SetSpace(GameObject space, int x, int y) {
        GameObject spaceGo;
        try {
            spaceGo = Instantiate(space, BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
            spaceGo.GetComponent<ISpace>().Initialise(x, y);
        } catch {
            spaceGo = Instantiate(emptySpace, BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        }
        
        return spaceGo;
    }

    /// <summary>
    /// Runs on startup.
    /// </summary>
    void Start() {
        boardLayout = BoardReader.GetSymbols("Assets/Resources/chessBoard.txt");
        pieceLayout = BoardReader.GetSymbols("Assets/Resources/testPieces.txt", ',');

        gridSizeX = boardLayout.GetLength(0);
        gridSizeY = boardLayout.GetLength(1);

        emptySpace = Resources.Load("prefabs/Spaces/EmptySpace") as GameObject;
        var whiteSpace = Resources.Load("prefabs/Spaces/WhiteSpace") as GameObject;
        var blackSpace = Resources.Load("prefabs/Spaces/BlackSpace") as GameObject;

        var pawnPiece = Resources.Load("prefabs/Pieces/Pawn") as GameObject;
        var bishopPiece = Resources.Load("prefabs/Pieces/Bishop") as GameObject;

        spaceDictionary = new Dictionary<char, GameObject> {
            { 'W', whiteSpace},
            { 'B', blackSpace}
        };

        pieceDictionary = new Dictionary<string, GameObject> {
            { "WP", pawnPiece},
            { "BP", pawnPiece},
            { "WB", bishopPiece },
            { "BB", bishopPiece }
        };

        SetupBoard();
    }
}
