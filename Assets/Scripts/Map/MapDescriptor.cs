using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Structure to save map on start to avoid loading map during game */
public struct MapDescriptor
{
    public string name;         // Name of the map
    public int[] size;          // Size of the map
    public int[,] grid;         // Grid of the map

    public int width
    {
        get { return size[0]; }
    }

    public int height
    {
        get { return size[1]; }
    }

    public override string ToString()
    {
        return string.Format("Name: {0} \n Size: {1}x{2} \n Grid: {3} elements", name, width, height, grid.Length);
    }
}
