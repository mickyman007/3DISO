using UnityEngine;

public class BoardSetter : MonoBehaviour {

    public int GridSizeX;
    public int GridSizeY;
    public IBoard Board;
    public GameObject EmptySpace;

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
        var spaceGo = Instantiate(EmptySpace, BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        spaceGo.AddComponent<Space>().Initialise(x, y);
        return spaceGo;
    }

    void Start() {
        SetupBoard();
    }

    void Update() {
        
    }
}
