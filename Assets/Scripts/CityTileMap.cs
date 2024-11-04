using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CityTileMap : MonoBehaviour
{
    public Tilemap myTileMap;
    public TileBase liveTile;
    public Camera myCam;
    public int[,] gameMap = new int [27, 27];
    public int[,] newGameMap = new int [27, 27];
    


    // Start is called before the first frame update
    void Start()
    {
        GenerateTileMap();
        DrawTileMap();
		InvokeRepeating("DrawTileMap", 1f, 1f);
	}

    void GenerateTileMap () {
		for (int y = 1; y < newGameMap.GetLength(1) - 1; y++) {
			for (int x = 1; x < newGameMap.GetLength(0) - 1; x++) {
				newGameMap[x, y] = (UnityEngine.Random.Range(0, 2));
			}
		}
		gameMap = newGameMap;

	}

    void DrawTileMap() {
        LifeCheck();
		for (int y = 1; y < newGameMap.GetLength(1) - 1; y++) {
			for (int x = 1; x < newGameMap.GetLength(0) - 1; x++) {
				if (newGameMap[x, y] == 1) myTileMap.SetTile(new Vector3Int(x, y, 0), liveTile);
				else myTileMap.SetTile(new Vector3Int(x, y, 0), null);
			}
		}
        gameMap = newGameMap;
	}


    public void LifeCheck() {
		for (int y = 1; y < gameMap.GetLength(1) - 1; y++) {
			for (int x = 1; x < gameMap.GetLength(0) - 1; x++) {
                int neighbours = GetNeighbourCount(x, y);
                if (gameMap[x, y] == 1) {
					if (neighbours < 2 || neighbours > 3) {
						newGameMap[x, y] = 0;
					}
                }
                else if (gameMap[x, y] == 0) {
                    if (neighbours == 3) {
                        newGameMap[x, y] = 1;
					}
				}
			}
		}
	}

    public int GetNeighbourCount(int xCoords, int yCoords) {

        int neighbours = 0;

        for (int x = -1; x < 2; x++) {
			for (int y = -1; y < 2; y++) {
                if (gameMap[xCoords + x, yCoords + y] == 1) neighbours++;
			}
		}
        if (gameMap[xCoords, yCoords] == 1) neighbours--;
		return neighbours;
    }

	public void OnGUI() {
        Vector3 mouseWorldPosition = myCam.ScreenToWorldPoint(Input.mousePosition);
        GUI.Label(new Rect(5, 5, 600, 30), $"Mouse: {Input.mousePosition} In cell space: {myTileMap.WorldToCell(mouseWorldPosition)}");
	}

	// Update is called once per frame
	float elapsed = 0f;
	void Update() {
		elapsed += Time.deltaTime;
		if (elapsed >= 1f) {
			elapsed = elapsed % 1f;
			DrawTileMap();
		}
	}



}
