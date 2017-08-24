using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structural_Wall : MonoBehaviour
{
    public MaterialType wallMaterial;
    private string materialString = "";
    private Object[] _sprites;


    private void Start()
    {
        Tile t = TileManager._instance.getTileFromWorldPoint(transform.position);
        t.isWall = true;
        t.getSetWall = this;
        PickSprite();
        NeighboursCheckSprites();
    }

    public void SetWallMaterial(MaterialType wallMat)
    {
        wallMaterial = wallMat;
        switch(wallMaterial)
        {
            case MaterialType.Wood:
                materialString = "Wood";
                break;

            case MaterialType.Brick:
                materialString = "Brick";
                break;

            case MaterialType.Metal:
                materialString = "Metal";
                break;
        }            
    }
    
    public void PickSprite()
    {      
        Tile t = TileManager._instance.getTileFromWorldPoint(transform.position);
        t.isWall = true;
        Tile[] tArray = TileManager._instance.getTileNeighbours4Way(t);

        int index = 0;

        if (tArray[0].isWall) { index += 2; }
        if (tArray[1].isWall) { index += 4; }
        if (tArray[2].isWall) { index += 8; }
        if (tArray[3].isWall) { index += 1; }

        Object[] sprites = Resources.LoadAll("Sprites\\Enviro\\Prototype\\ss_Wall_" + materialString);
        _sprites = sprites;
        Sprite _sprite = (Sprite)sprites[index + 1];
        this.GetComponent<SpriteRenderer>().sprite = _sprite;        
    }

    private void NeighboursCheckSprites()
    {
        Tile tile = TileManager._instance.getTileFromWorldPoint(transform.position);
        Tile[] tArray = TileManager._instance.getTileNeighbours4Way(tile);
        foreach(Tile t in tArray)
        {
            if (t.isWall) { t.getSetWall.PickSprite(); }
        }
    }
}