using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TilemapStuff : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform highlight;

    public Tile flower;
    public CinemachineImpulseSource impulseSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPos = tilemap.WorldToCell(mousePos); //this shows the sprite at the bottom left position of the tile grid

        //to make it follow the mouse exactly or show up at the centre of the tile grid
        Vector3 pos = tilemap.GetCellCenterWorld(cellPos);
        
        //Debug.Log(mousePos + " is at cell: " +  cellPos);

        highlight.position = pos;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log(tilemap.GetTile(cellPos));
            tilemap.SetTile(cellPos, flower);
            impulseSource.
        }
    }
}
