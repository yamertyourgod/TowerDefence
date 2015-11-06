using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public int RawsCount;
    public int ColumnsCount;
    public float CellXSize;
    public float CellZSize;
    public Vector3 GridCenter;
    public Vector3[,] Nodes;


    private static Grid _instance;

	// Use this for initialization
	void Start ()
	{
	    _instance = this;
        Nodes = new Vector3[RawsCount,ColumnsCount];
        CreateGrid(RawsCount, ColumnsCount, CellXSize, CellZSize, GridCenter);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Grid(int rawsCount, int columnsCount, float cellXSize, float cellZSize, Vector3 gridCenter)
    {
        if (_instance != null) return;
        _instance = this;
        RawsCount = rawsCount;
        ColumnsCount = columnsCount;
        CellXSize = cellXSize;
        CellZSize = cellZSize;
        GridCenter = gridCenter;
        Nodes = new Vector3[RawsCount, ColumnsCount];
        CreateGrid(rawsCount, columnsCount, cellXSize, cellZSize, gridCenter);
    }

    public Grid()
    {
        
    }
    public static Grid GetInstance()
    {
        return _instance;
    }

    void CreateGrid(int raws, int columns, float xSize, float zSize, Vector3 center)
    {
        for (var r = 0; r < raws; r++)
        {
            for (var c = 0; c < columns; c++)
            {
                Nodes[r,c] = new Vector3(xSize*r+center.x, center.y, zSize*c+center.z);
            }
        }        
    }
}
