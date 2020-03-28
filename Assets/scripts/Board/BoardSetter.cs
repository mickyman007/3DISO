using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardSetter : MonoBehaviour {

    private char[,] boardLayout;
    private string[,] pieceLayout;

    private int gridSizeX;
    private int gridSizeY;
    private IBoard board;
    private GameObject emptySpace;

    private Dictionary<char, GameObject> spaceDictionary;
    private Dictionary<string, GameObject> pieceDictionary;

    private void SetupBoard() {
        board = new Board(gridSizeX, gridSizeY);

        for(int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                var spaceGo = SetSpace(x, y);
                board.Set(spaceGo.GetComponent<ISpace>());

                var pieceGo = SetPiece(board.Spaces[x, y]);
                if (pieceGo != null) {
                    board.Set(pieceGo.GetComponent<IPiece>());
                }
            }
        }
    }

    private GameObject SetPiece(ISpace space) {
        GameObject pieceGo = null;
        try {
            pieceGo = Instantiate(pieceDictionary[pieceLayout[space.X, space.Y]], space.GetWorldCoords(),
                Quaternion.Euler(0, 0, 0));
            var piece = pieceGo.GetComponent<IPiece>();
            piece.Initialise(space);
            piece.MoveableSpaces = piece.MovementRules.GetLegalMoves(board, piece);
            piece.Rotation = (space.Y % 2 == 0)? Rotation.West : Rotation.East;
        } catch {
            Debug.Log("Did not find any piece at " + space.X + ", " + space.Y);
        }
        
        return pieceGo;
    }

    private GameObject SetSpace(int x, int y) {
        GameObject spaceGo;
        try {
            spaceGo = Instantiate(spaceDictionary[boardLayout[x, y]], BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        } catch {
            spaceGo = Instantiate(emptySpace, BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        }
        spaceGo.GetComponent<ISpace>().Initialise(x, y);
        return spaceGo;
    }

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
