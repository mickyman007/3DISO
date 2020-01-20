using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSetter : MonoBehaviour {

    public int GridSizeX;
    public int GridSizeY;
    public Board Board;
    public GameObject EmptySpace;

    private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);

        for (int i = 0; i < GridSizeX; i++) {
            for (int j = 0; j < GridSizeY; j++) {
                Gizmos.DrawCube(
                    new Vector3(i, 1, j), 
                    new Vector3(1, 1, 1));
            }
        }
    }

    private void SetupBoard() {
        for(int i = 0; i < GridSizeX; i++) {
            for (int j = 0; j < GridSizeY; j++) {
                var space = Instantiate(EmptySpace, CoordToWorld(i, j), Quaternion.Euler(90, 0, 0));
                spaces.Add(space.GetComponent<ISpace>());
            }
        }
    }

    private Tuple<int, int> WorldToCoord(Vector3 position) {
        return new Tuple<int, int>((int)position.x, (int)position.z);
    }

    private Vector3 CoordToWorld(int x, int y) {
        return new Vector3(x, 0, y);
    }

    // Start is called before the first frame update
    void Start() {
        SetupBoard();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
