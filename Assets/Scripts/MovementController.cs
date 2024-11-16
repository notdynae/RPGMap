using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector3 targetPosition;
    
    
    public Tilemap decoTileMap;
    public TileBase npcTile;
    public TileBase borderTile;
    public TileBase garbageTile;
    
    
    
    bool IsCollidingWithWall(Vector3 position)
    {

        Vector3Int tilePosition = decoTileMap.WorldToCell(position);
        TileBase tile = decoTileMap.GetTile(tilePosition);
        
        if (tile == npcTile || tile == borderTile || tile == garbageTile) return true;
        else return false;
    }

    
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0).normalized;
            
        targetPosition = transform.position + movement * moveSpeed * Time.deltaTime;
        if (!IsCollidingWithWall(targetPosition)) transform.position = targetPosition;
    }
}
