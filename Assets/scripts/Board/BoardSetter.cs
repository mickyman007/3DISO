using System.Collections.Generic;
using UnityEngine;

public class BoardSetter : MonoBehaviour {

    public char[,] boardLayout = {
        { 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
        { 'W', 'S', 'S', 'S', 'S', 'S', 'S', 'W'},
        { 'W', 'S', 'S', 'S', 'S', 'S', 'S', 'W'},
        { 'W', 'S', 'S', 'S', 'S', 'S', 'S', 'W'},
        { 'W', 'W', 'W', 'W', 'W', 'W', ' ', 'W'}
    };

    public int GridSizeX;
    public int GridSizeY;
    public IBoard Board;
    public GameObject EmptySpace;
    public GameObject Space;
    public GameObject Wall;

    public Dictionary<char, GameObject> spaceDictionary;

    private void SetupBoard() {
        Board = new Board(GridSizeX, GridSizeY);

        for(int x = 0; x < GridSizeX; x++) {
            for (int y = 0; y < GridSizeY; y++) {
                var spaceGo = SetSpace(x, y);
                Board.Set(spaceGo.GetComponent<ISpace>());
            }
        }
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

        EmptySpace = Resources.Load("prefabs/EmptySpace") as GameObject;
        Space = Resources.Load("prefabs/Space") as GameObject;
        Wall = Resources.Load("prefabs/WallSpace") as GameObject;



        spaceDictionary = new Dictionary<char, GameObject> {
            { 'S', Space},
            { 'W', Wall}
        };

        SetupBoard();
    }
}
