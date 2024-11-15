using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
	// ---------------------------------- References
	// public MapLayout mapLayout;
	public Camera myCam;

	public Tilemap groundTileMap;
	public Tilemap decoTileMap;
	
	public TileBase sidewalkTile;
	public TileBase roadTile;
	public TileBase grassTile;
	
	// ---------------------------------- Consts / Variables
	public const int mapWidth = 30;
	public const int mapHeight = 30;
	public const int borderOffset = 10;

	
	const char grassChar = '^';
	const char sidewalkChar = '-';
	const char roadChar = '=';
	const char fenceChar = '|';

	public int[,] groundMap = new int[mapWidth + borderOffset * 2, mapHeight + borderOffset * 2];
	public int[,] decoMap = new int[mapWidth, mapHeight];

	// ----------------------------------- Class Functions
	public void StringToMap(string mapString) {

	}

	public string GenerateStringMap(int width, int height) {
		string stringMap = "";

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				stringMap += "=";
			}
			stringMap += "\n";
		}
		return stringMap;
	}

	public string LoadMap(string mapPath) {
		return "map";
	}

	public void DrawTileMap() {
		
		for (int y = 0; y < groundMap.GetLength(1); y++) {
			for (int x = 0; x < groundMap.GetLength(0); x++) {
				groundTileMap.SetTile(new Vector3Int(x, y, 0), grassTile);
				if (x is > borderOffset and < mapWidth + borderOffset && y is > borderOffset and < mapWidth + borderOffset) groundTileMap.SetTile(new Vector3Int(x, y, 0), sidewalkTile);
			}
		}
	}
	
	
	// ----------------------------------------- Unity Functions
	public void OnGUI() {
		Vector3 mouseWorldPosition = myCam.ScreenToWorldPoint(Input.mousePosition);
		GUI.Label(new Rect(5, 5, 600, 30), $"Mouse: {Input.mousePosition} In cell space: {groundTileMap.WorldToCell(mouseWorldPosition)}");
	}

	public void Start() {
		// GenerateStringMap(mapWidth, mapHeight);
		DrawTileMap();
	}
}
