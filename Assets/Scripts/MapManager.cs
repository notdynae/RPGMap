using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
	// ---------------------------------- References
	// public MapLayout mapLayout;
	public Camera myCam;

	public Tilemap groundTileMap;
	public Tilemap decoTileMap;
	
	public TileBase roadTile;
	public TileBase grassTile;
	
	public TileBase sidewalkTile;
	public TileBase sidewalkTileN;
	public TileBase sidewalkTileNE;
	public TileBase sidewalkTileE;
	public TileBase sidewalkTileSE;
	public TileBase sidewalkTileS;
	public TileBase sidewalkTileSW;
	public TileBase sidewalkTileW;
	public TileBase sidewalkTileNW;
	public TileBase sidewalkInnerNE;
	public TileBase sidewalkInnerSE;
	public TileBase sidewalkInnerSW;
	public TileBase sidewalkInnerNW;

	public TileBase npcTile;
	public TileBase borderTile;
	public TileBase garbageTile;
	
	// ---------------------------------- Consts / Variables
	const int borderOffset = 10;
	
	const char waterChar = 'w';
	const char obstChar = 'X';
	const char npcChar = '@';

	// ----------------------------------- Class Functions
	
	public void StringToMap(string mapData) {
		Debug.Log(mapData);
		int mapWidth = mapData.IndexOf('\n');
		int mapHeight = mapWidth;
		DrawBaseMap(mapHeight, mapWidth);
		int index = 0;
		for (int y = 0; y < mapHeight + 1; y++) {
			for (int x = 0; x < mapWidth + 1; x++) {
				char tileChar = mapData[index];
				Vector3Int position = new Vector3Int(x + borderOffset, y + borderOffset, 0);

				switch (tileChar)
				{
					case waterChar:
						decoTileMap.SetTile(position, borderTile);
						break;
					case obstChar:
						decoTileMap.SetTile(position, garbageTile);
						break;
					case npcChar:
						decoTileMap.SetTile(position, npcTile);
						break;
					default:
						break;
				}

				index++;
			}
		}
	}

	public string GenerateStringMap(int width, int height) {
		string stringMap = "";

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				if (x == 0 || x == width - 1 || y == 0 || y == height - 1) stringMap += "w";
				else if (UnityEngine.Random.Range(0f, 1f) < 0.2) stringMap += "X";
				else if (UnityEngine.Random.Range(0f, 1f) < 0.05) stringMap += "@";
				else stringMap += " ";
			}
			stringMap += "\n";
		}
		return stringMap;
	}

	public void LoadMap(string mapFilePath)
	{
		string mapData = File.ReadAllText(mapFilePath);
		StringToMap(mapData);
	}

	public void DrawBaseMap(int width, int height) {

		int baseWidth = width-2 ;
		int baseHeight = height -2;
		for (int y = 0; y < baseHeight + borderOffset*2 +1; y++) {
			for (int x = 0; x < baseWidth + borderOffset*2 +1; x++) {

				// -------------------------------- outer grass / inner road
				
				if (x < borderOffset || x > baseWidth + borderOffset || y < borderOffset || y > baseWidth + borderOffset ) groundTileMap.SetTile(new Vector3Int(x, y, 0), grassTile);
				else groundTileMap.SetTile(new Vector3Int(x, y, 0), roadTile);
				
				TileBase drawTile = null;
				
				// -------------------------------- outer sidewalks
				if (y == borderOffset) {
					if (x == borderOffset) drawTile = sidewalkTileSW;
					else if (x == baseWidth + borderOffset) drawTile = sidewalkTileSE;
					else if (x > borderOffset && x < baseWidth + borderOffset) drawTile = sidewalkTileS;
				} 
				else if (y == baseHeight + borderOffset) {
					if (x == borderOffset) drawTile = sidewalkTileNW;
					else if (x == baseWidth + borderOffset) drawTile = sidewalkTileNE;
					else if (x > borderOffset && x < baseWidth + borderOffset) drawTile = sidewalkTileN;
				}
				else if (x == borderOffset) {
					if (y  > borderOffset && y < baseHeight + borderOffset) drawTile = sidewalkTileW;
				} 
				else if (x == baseWidth + borderOffset) {
					if (y > borderOffset && y < baseHeight + borderOffset) drawTile = sidewalkTileE;
				}
				
				// ------------------------------------ inner sidewalks
				
				if (y == borderOffset + 1) {
					if (x == borderOffset + 1) drawTile = sidewalkInnerNE;
					else if (x == baseWidth + borderOffset - 1) drawTile = sidewalkInnerNW;
					else if (x > borderOffset + 1 && x < baseWidth + borderOffset - 1) drawTile = sidewalkTileN;
				} 
				else if (y == baseWidth + borderOffset - 1) {
					if (x == borderOffset + 1) drawTile = sidewalkInnerSE;
					else if (x == baseWidth + borderOffset - 1) drawTile = sidewalkInnerSW;
					else if (x > borderOffset + 1 && x < baseWidth + borderOffset - 1) drawTile = sidewalkTileS;
				}
				else if (x == borderOffset + 1) {
					if (y > borderOffset && y < baseHeight + borderOffset) drawTile = sidewalkTileE;
				} 
				else if (x == baseWidth + borderOffset - 1) {
					if (y > borderOffset && y < baseHeight + borderOffset) drawTile = sidewalkTileW;
				}
				if (drawTile) groundTileMap.SetTile(new Vector3Int(x, y, 0), drawTile);
			}
		}
	}
	
	
	// ----------------------------------------- Unity Functions
	public void OnGUI() {
		Vector3 mouseWorldPosition = myCam.ScreenToWorldPoint(Input.mousePosition);
		GUI.Label(new Rect(5, 5, 600, 30), $"Mouse: {Input.mousePosition} In cell space: {groundTileMap.WorldToCell(mouseWorldPosition)}");
	}

	public void Start() {
	//	LoadMap(Application.dataPath + "/Maps/gameMap.txt");
		StringToMap(GenerateStringMap(20, 20));
	}
}
