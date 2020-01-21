using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSetter : MonoBehaviour {

    public int GridSizeX;
    public int GridSizeY;
    public Board Board;
    public GameObject EmptySpace;

    public GameObject GetSpaceAtPosition(Vector3 position) {
        Collider[] colliders = Physics.OverlapSphere(position, 1f);
        if (colliders.Length > 1) {
            foreach (var collider in colliders) {
                var go = collider.gameObject;
                if (go == gameObject) continue;
                return gameObject;
            }
        }
        return null;
    }

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
        for(int x = 0; x < GridSizeX; x++) {
            for (int y = 0; y < GridSizeY; y++) {
                var spaceGo = CreateSpace(x, y);
                Board.Add(spaceGo.GetComponent<ISpace>());
            }
        }
    }

    private GameObject CreateSpace(int x, int y) {
        var spaceGo = Instantiate(EmptySpace, CoordToWorld(x, y), Quaternion.Euler(90, 0, 0));
        spaceGo.AddComponent<Space>().Initialise(x, y);
        return spaceGo;
    }

    private Tuple<int, int> WorldToCoord(Vector3 position) {
        return new Tuple<int, int>((int)position.x, (int)position.z);
    }

    private Vector3 CoordToWorld(int x, int y) {
        return new Vector3(x, 0, y);
    }

    // Start is called before the first frame update
    void Start() {
        Board = new Board();
        SetupBoard();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
