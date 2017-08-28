using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int coordX, coordY;
    
    public bool isWall, isWalkable;
    private StructuralWall wall;
    public StructuralWall getSetWall
    {
        get
        {
            return wall;
        }
        set
        {
            wall = value;
        }
            
    }

    public int getSetCoordX
    {
        get
        {
            return coordX;
        }

        set
        {
            coordX = value;
        }
    }

    public int getSetCoordY
    {
        get
        {
            return coordY;
        }

        set
        {
            coordY = value;
        }
    }

}