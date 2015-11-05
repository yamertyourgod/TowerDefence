using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map:MonoBehaviour
{
    private static Map _instance;
    private Grid _grid = Grid.GetInstance();
    private GameObject _mapGO; 
    public List<GridCell> Cells;
   
    // Use this for initialization
    void Start ()
	{
	    _instance = this;
        Cells = new List<GridCell>();
        BuildMap();

    }
	
	// Update is called once per frame
    void Update()
    {
       
	}

    public Map()
    {
        if (_instance != null) return;
        _instance = this;
        Cells = new List<GridCell>();
        BuildMap();
    }

    public  static Map GetInstance()
    {
        return _instance;
    }

    public void BuildMap()
    {
        _mapGO = new GameObject("Map");
        foreach (var node in _grid.Nodes)
        {
            var cell = new GridCell(node);
            Cells.Add(cell);
            cell.GO.transform.SetParent(_mapGO.transform);
            cell.State = GridCell.EnumCellState.Blocked;
        }
    }
}
