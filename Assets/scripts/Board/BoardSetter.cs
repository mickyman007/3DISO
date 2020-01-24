using UnityEngine;

public class BoardSetter : MonoBehaviour {

    public int GridSizeX;
    public int GridSizeY;
    public Board Board;
    public GameObject EmptySpace;

    private void SetupBoard() {
        for(int x = 0; x < GridSizeX; x++) {
            for (int y = 0; y < GridSizeY; y++) {
                var spaceGo = CreateSpace(x, y);
                Board.Add(spaceGo.GetComponent<ISpace>());
            }
        }
    }

    private GameObject CreateSpace(int x, int y) {
        var spaceGo = Instantiate(EmptySpace, BoardUtilities.CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        spaceGo.AddComponent<Space>().Initialise(x, y);
        return spaceGo;
    }

    void Start() {
        Board = new Board();
        SetupBoard();
    }

    void Update() {
        
    }
}
