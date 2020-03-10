using System.Collections.Generic;
using UnityEngine;

public class BoardSetter : MonoBehaviour {

    public char[,] boardLayout;

    public int GridSizeX;
    public int GridSizeY;
    public IBoard Board;
    public GameObject EmptySpace;
    public GameObject Space;
    public GameObject Wall;

    public GameObject GenericPiece;

    public Dictionary<char, GameObject> spaceDictionary;

    private void SetupBoard() {
        Board = new Board(GridSizeX, GridSizeY);

        for(int x = 0; x < GridSizeX; x++) {
            for (int y = 0; y < GridSizeY; y++) {
                var spaceGo = SetSpace(x, y);
                Board.Set(spaceGo.GetComponent<ISpace>());
            }
        }

        var pieceGo = SetPiece(Board.Spaces[Random.Range(0, GridSizeX), Random.Range(0, GridSizeY)]);
        Board.Set(pieceGo.GetComponent<IPiece>());
    }

    private GameObject SetPiece(ISpace space) {
        GameObject pieceGo;
        pieceGo = Instantiate(GenericPiece, space.GetWorldCoords(), Quaternion.Euler(0, 0, 0)) ;
        pieceGo.GetComponent<IPiece>().Initialise(space);
        return pieceGo;
    }

    private GameObject SetSpace(int x, int y) {
        GameObject spaceGo;
        try {
            spaceGo = Instantiate(spaceDictionary[boardLayout[x, y]], BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        } catch {
            spaceGo = Instantiate(EmptySpace, BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        }
        spaceGo.GetComponent<ISpace>().Initialise(x, y);
        return spaceGo;
    }

    void Start() {
        boardLayout = BoardReader.GetSpaces("Assets/Resources/testBoard.txt");

        GridSizeX = boardLayout.GetLength(0);
        GridSizeY = boardLayout.GetLength(1);

        EmptySpace = Resources.Load("prefabs/Spaces/EmptySpace") as GameObject;
        Space = Resources.Load("prefabs/Spaces/Space") as GameObject;
        Wall = Resources.Load("prefabs/Spaces/WallSpace") as GameObject;

        GenericPiece = Resources.Load("prefabs/Pieces/Piece") as GameObject;

        spaceDictionary = new Dictionary<char, GameObject> {
            { 'S', Space},
            { 'W', Wall}
        };

        SetupBoard();
    }
}
