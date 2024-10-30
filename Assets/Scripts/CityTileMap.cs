using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CityTileMap : MonoBehaviour
{
    public Tilemap myTileMap;
    public Camera myCam;
    public TileBase testTileBase;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        TryGetComponent<Tilemap>(out myTileMap);
        myTileMap.SetTile(new Vector3Int(-6, -4, 0), null);
    }

	public void OnGUI() {
        Vector3 mouseWorldPosition = myCam.ScreenToWorldPoint(Input.mousePosition);
        GUI.Label(new Rect(5, 5, 600, 30), $"Mouse: {Input.mousePosition} In cell space: {myTileMap.WorldToCell(Input.mousePosition)}");
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
