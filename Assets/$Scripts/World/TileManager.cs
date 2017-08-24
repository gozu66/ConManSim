/**** Tile Manager (monoB) created from world manager ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    public TileManager(Grid grid)                                               // global tile grid
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("2 or more tile managers in scene");
            Debug.Break();
        }
        CreateTiles(grid);                                                      // create grid
    }

    public static TileManager _instance;                                        // singleton

    private Tile[,] tiles;
    public Tile[,] getTiles
    {
        get
        {
            return tiles;
        }
    }
    int worldX, worldY;

    public void CreateTiles(Grid pathfindingGrid)
    {
        Sprite _sprite = Resources.Load<Sprite>("Sprites\\Enviro\\Prototype\\s_FloorTiles_Ceramic");

        Vector2 worldSize = pathfindingGrid.getWorldSize;
        worldX = (int)worldSize.x;
        worldY = (int)worldSize.y;

        tiles = new Tile[worldX, worldY];

        GameObject tileParent = new GameObject();
        tileParent.name = "Tile Holder";

        Vector3 worldBottomLeft = WorldManager._instance.gameObject.transform.position - Vector3.right * worldX /
                                                          2 - WorldManager._instance.gameObject.transform.position - Vector3.forward * worldY / 2;
        for (int x = 0; x < worldX; x++)
        {
            for (int y = 0; y < worldY; y++)
            {
                GameObject newTile = new GameObject();
                tiles[x, y] = newTile.AddComponent<Tile>();
                tiles[x, y].getSetCoordX = x;
                tiles[x, y].getSetCoordY = y;

                SpriteRenderer newSprite = newTile.AddComponent<SpriteRenderer>();
                newSprite.sprite = _sprite;
                newSprite.sortingLayerName = "Background";

                Vector2 topRight = new Vector2(-worldX / 2, worldY / 2);
                Vector3 worldPosition = worldBottomLeft + Vector3.right * (x * 1.0f + 0.5f) + Vector3.forward * (y * 1.0f + 0.5f);
                newTile.transform.position = worldPosition;
                newTile.transform.Rotate(-90, 0, 0);
                newTile.transform.parent = tileParent.transform;
            }
        }

    }

    public Tile getTileFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = worldPosition.x / worldX + 0.5f;      
        float percentY = worldPosition.z / worldY + 0.5f;      

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((worldX - 1) * percentX);
        int y = Mathf.RoundToInt((worldY - 1) * percentY);

        return tiles[x, y];
    }

    public Tile[] getTileNeighbours4Way(Tile t)
    {
        int WorldSquare = (int)Mathf.Sqrt(TileManager._instance.getTiles.Length);

        List<Tile> neighbours = new List<Tile>();

        if (t.getSetCoordY <= WorldSquare) { neighbours.Add(TileManager._instance.getTiles[t.getSetCoordX, t.getSetCoordY + 1]);    }
        if (t.getSetCoordX <= WorldSquare) { neighbours.Add(TileManager._instance.getTiles[t.getSetCoordX + 1, t.getSetCoordY]);    }
        if (t.getSetCoordY > 0) {    neighbours.Add(TileManager._instance.getTiles[t.getSetCoordX, t.getSetCoordY - 1]);    }
        if (t.getSetCoordX > 0) {    neighbours.Add(TileManager._instance.getTiles[t.getSetCoordX - 1, t.getSetCoordY]);    }

        return neighbours.ToArray();
    }

}