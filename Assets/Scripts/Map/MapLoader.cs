using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public TextAsset map;                   // Text asset getting map .txt file
    public MapDescriptor descriptor;        // Descriptor object to save map datas on load game
    public GameObject cellPrefab;           // Prefab for cell: useful for building map
    public Sprite[] spriteSheet;            // Table to store all sprites

    const int NAME = 0;                     // Const to get row num where the name of the map is located
    const int SIZE = 1;                     // Same for the size of the map
    const int GRID = 2;                     // Same for the starting line of the grid

    private string[] lines;                 // Table to get all the lines of the map .txt file
    private string[] columns;               // Table to get all the data in columns of the map .txt file
    private string[] size;                  // Table to get size of map .txt file located on line 1

    private int width;                      // Width of the map
    private int height;                     // Height of the map

    private const float CELL_WIDTH = 32f;   // Default cell width
    private const float CELL_HEIGHT = 32f;  // Default cell height
    private float MAP_MAX_WIDTH;            // Map max width           
    private float MAP_MAX_HEIGHT;           // Map max height

    void Start()
    {
        MapDescriptor descriptor = ReadMap();
        BuildMap(descriptor);
    }

    void Update()
    {
        
    }

    /* Function to read datas on the map and save it in MapDescriptor object */
    private MapDescriptor ReadMap()
    {
        Debug.Log(map.text);

        MapDescriptor tempDescriptor;

        lines = map.text.Split('\n');                                       // Count a new line each time a backslash is detected

        //Debug.Log("Name: " + lines[NAME]);
        tempDescriptor.name = lines[NAME];                                  // Giving name to descriptor

        size = lines[SIZE].Split(',');                                      // New value each time a "," is encoutered on the line 
        width = int.Parse(size[0]);                                         // First data of size table
        height = int.Parse(size[1]);                                        // Second data of size table
        //Debug.Log("Size: " + width + "x" + height);
        tempDescriptor.size = new int[] { width, height };                  // Giving size to descriptor


        tempDescriptor.grid = new int[width, height];                       // Giving map datas to descriptor

        Debug.Assert(lines.Length - GRID == height,                         //
            "Mismatch on height (got " + (lines.Length - GRID) +            // Verify if the number of datas on grid is the same as the size precised on line 1
            " lines, expected " + height + ")");                            //

        /* Read datas of the map .txt file */
        for (int i = GRID; i < lines.Length; ++i)                           // Start for at line 2 of the map .txt file
        {
            int gridLine = i - GRID;
            columns = lines[i].Split(',');                                  // New column each time a "," is encountered

            Debug.Assert(columns.Length == width,                           //
                "Mismatch on width on line " + gridLine + " (got " +        // Verify if the number of datas on grid is the same as the size precised on line 1
                columns.Length + " columns, expected " + width + ")");      //

            for (int j = 0; j < columns.Length; ++j)
            {
                //Debug.Log(columns[j]);
                tempDescriptor.grid[j, gridLine] = int.Parse(columns[j]);       // Insert data of cell in grid
            }
        }

        return tempDescriptor;
    }

    /* Function to drawing map */
    private void BuildMap(MapDescriptor pDescriptor)
    {
        MAP_MAX_WIDTH = pDescriptor.width * CELL_WIDTH;
        MAP_MAX_HEIGHT = pDescriptor.height * CELL_HEIGHT;
        float left = -MAP_MAX_WIDTH * 0.5f;
        float up = MAP_MAX_HEIGHT * 0.5f;

        for (int c = 0; c < pDescriptor.width; ++c)         // Columns
        {
            for (int l = 0; l < pDescriptor.height; l++)    // Lines
            {
                int cellValue = pDescriptor.grid[c, l];
                Vector3 cellPosition = new Vector3(
                    c * CELL_WIDTH,
                    l * CELL_HEIGHT,
                    0f
                );

                /* Creating object */
                CreateCell(c, l, cellValue, cellPosition);
            }
        }
    }

    /* Function to create cells of the map based on prefab */
    private GameObject CreateCell(int pCol, int pLine, int pVal, Vector3 pPos)
    {
        GameObject cell = GameObject.Instantiate(cellPrefab, pPos, Quaternion.identity);        // Create game object

        cell.name = string.Format("[{0}, {1}]Cell({2})", pCol, pLine, pVal);                    // Name of the game object
        cell.GetComponent<SpriteRenderer>().sprite = spriteSheet[pVal];

        return cell;
    }
}
