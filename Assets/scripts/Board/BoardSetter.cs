using System.Collections.Generic;
using UnityEngine;

public class BoardSetter : MonoBehaviour {

    public char[,] myBoard = {
        { 'W', 'W', 'W', 'W', 'W'},
        { 'W', 'S', 'S', 'S', 'W'},
        { 'W', 'S', 'S', 'S', 'W'},
        { 'W', 'S', 'S', 'S', 'W'},
        { 'W', 'W', 'W', 'W', 'W'}
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
                var spaceGo = CreateSpace(x, y);
                Board.Set(spaceGo.GetComponent<ISpace>());
            }
        }
    }

    private GameObject CreateSpace(int x, int y) {
        var space = myBoard[x, y];
        var spaceGo = Instantiate(spaceDictionary[myBoard[x,y]], BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        spaceGo.AddComponent<Space>().Initialise(x, y);
        return spaceGo;
    }

    void Start() {
        GridSizeX = myBoard.GetLength(0);
        GridSizeY = myBoard.GetLength(1);

        Space = Resources.Load("prefabs/Space") as GameObject;
        Wall = Resources.Load("prefabs/WallSpace") as GameObject;

        spaceDictionary = new Dictionary<char, GameObject> {
            { 'S', Space},
            { 'W', Wall}
        };

        SetupBoard();
    }
}
