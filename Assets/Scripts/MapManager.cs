using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
	public MapLayout mapLayout;
	public Camera myCam;

	public Tilemap tileMap;
	public TileBase sidewalkTile;
	public TileBase roadTile;
	
	public const int mapWidth = 27;
	public const int mapHeight = 16;

	public const char roadChar = '=';
	public const char grassChar = '^';
	public const char buildChar = '#';

	public int[,] gameMap = new int[mapWidth, mapHeight];

	void StringToMap(string mapString) {

	}

	string GenerateStringMap(int width, int height) {
		string stringMap = "";

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				stringMap += "=";
			}
			stringMap += "\n";
		}
		return stringMap;
	}

	string LoadMap(string mapPath) {
		return "map";
	}

	void DrawTileMap() {
		for (int y = 0; y < gameMap.GetLength(1); y++) {
			for (int x = 0; x < gameMap.GetLength(0); x++) {
				gameMap[x, y] = (Random.Range(0, 5)) == 1 ? 1 : 0;
				if (gameMap[x, y] == 1) tileMap.SetTile(new Vector3Int(x, y, 0), roadTile);
				else tileMap.SetTile(new Vector3Int(x, y, 0), sidewalkTile);
			}
		}
	}

	public void OnGUI() {
		Vector3 mouseWorldPosition = myCam.ScreenToWorldPoint(Input.mousePosition);
		GUI.Label(new Rect(5, 5, 600, 30), $"Mouse: {Input.mousePosition} In cell space: {tileMap.WorldToCell(mouseWorldPosition)}");
	}

	public void Start() {
		GenerateStringMap(mapWidth, mapHeight);
		DrawTileMap();
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
		mapLayout.GenerateRoads(0,0);
	}
}
