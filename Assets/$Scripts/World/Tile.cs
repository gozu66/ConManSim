using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int coordX, coordY;
    
    public bool isWall, isWalkable;
    private Structural_Wall wall;
    public Structural_Wall getSetWall
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