using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CustomEditor(typeof(MapCreator))]
public class MapCreatorEditor : Editor
{
    private int _widthCount;
    private int _drumsCount;
    private string _createOrDestroyButtonText;
    private MapCreator _mapCreator;
    private Ray _ray;
    private bool _isPicking;
    private GameObject _lastSelctedCell;
    private GridCell.EnumCellState _currentPickMode;
    private bool _shiftPressed;
    private bool _editCurrentMap;
    // Use this for initialization
    public override void OnInspectorGUI()
    {
        if (Event.current != null && Event.current.isKey) { _shiftPressed = true;}

        _mapCreator = FindObjectOfType<MapCreator>();
        EditorGUILayout.LabelField("Current map");
        _editCurrentMap = EditorGUILayout.Toggle("Ceurrent Map", _editCurrentMap);
        // _createOrDestroyButtonText = _slotsCreator.CreateButtonText;
        EditorGUILayout.LabelField("RawsCount");
        _mapCreator.RawCount = EditorGUILayout.IntField(_mapCreator.RawCount);
        EditorGUILayout.LabelField("ColumnsCount");
        _mapCreator.ColumnCount = EditorGUILayout.IntField(_mapCreator.ColumnCount);
        EditorGUILayout.LabelField("Cell Width");
        _mapCreator.CellXSize = EditorGUILayout.FloatField(_mapCreator.CellXSize);
        EditorGUILayout.LabelField("Cell Height");
        _mapCreator.CellZSize = EditorGUILayout.FloatField(_mapCreator.CellZSize);
        EditorGUILayout.LabelField("CenterPos");
        _mapCreator.CenterPos = EditorGUILayout.Vector3Field("Vector3", _mapCreator.CenterPos);
        if (GUILayout.Button("CreateMap"))
        {
            new Grid(_mapCreator.RawCount, _mapCreator.ColumnCount, _mapCreator.CellXSize, _mapCreator.CellZSize, _mapCreator.CenterPos);
            new Map();
            _mapCreator.MapCreated = true;
        }
        if (_editCurrentMap)_mapCreator.MapCreated = true;
        if (_mapCreator.MapCreated)
        {
            EditorGUILayout.LabelField("Add lines");

                if (GUILayout.Button("Select Build Zone"))
                {
                    _currentPickMode = GridCell.EnumCellState.Buildable;
                    _isPicking = true;
                    
                }
                if (GUILayout.Button("Select Walkable Zone"))
                {
                    _currentPickMode = GridCell.EnumCellState.Walkable;
                    _isPicking = true;
                }
                if (GUILayout.Button("Select Blocked Zone"))
                {
                    _currentPickMode = GridCell.EnumCellState.Blocked;
                    _isPicking = true;
                }
                if (GUILayout.Button("Select Enemies Path"))
                {
                    _currentPickMode = GridCell.EnumCellState.Path;
                    _isPicking = true;
                }
            if (_isPicking)
            {
                if (GUILayout.Button("Stop select"))
                {
                    _isPicking = false;

                }
            }
            
            //    if (_slotsCreator.Lines != null && _slotsCreator.Lines.Any())
            //    {
            //        foreach (var line in _slotsCreator.Lines)
            //        {
            //            if (line.Nodes.Count > 0)
            //                EditorGUILayout.LabelField("Line" +
            //                                           line.Nodes.Aggregate("",
            //                                               (s, n) => s + "," + n.ToString().Replace(".0", "")));
            //            else EditorGUILayout.LabelField("Pick the line");
            //            line.LineColor = EditorGUILayout.ColorField("Color", line.LineColor);
            //            if (!line.StartPicking)
            //            {
            //                if (line.Nodes.Count > 0)
            //                {
            //                    if (GUILayout.Button("Destroy Line"))
            //                    {
            //                        line.DestroyGraphics();
            //                        _lineForDestroy = line;
            //                        DestroyImmediate(_lineForDestroy.Start.transform.parent.gameObject);
            //                    }
            //                }
            //                if (GUILayout.Button("Pick"))
            //                {
            //                    _isPicking = true;
            //                    _newline = true;
            //                    foreach (var slotsLine in _slotsCreator.Lines)
            //                    {
            //                        //if (_slotsCreator.CurrentLine.Nodes.Count > 0 && slotsLine.StartPicking == false) slotsLine.Nodes.Clear();
            //                        slotsLine.StartPicking = false;
            //                    }
            //                    line.StartPicking = true;
            //                    if (line.Nodes.Count > 0)
            //                    {
            //                        line.Nodes.Clear();
            //                        line.DestroyGraphics();
            //                        var lineTr = line.Start.transform.parent;
            //                        line.Start.transform.SetParent(lineTr.parent.parent);
            //                        line.End.transform.SetParent(lineTr.parent.parent);
            //                        DestroyImmediate(lineTr.gameObject);
            //                    }
            //                    //if(_slotsCreator.CurrentLine.Nodes.Count>0)
            //                    //_slotsCreator.CurrentLine.Nodes.Clear();
            //                    _slotsCreator.CurrentLine = line;
            //                }
            //            }
            //            if (line.StartPicking)
            //            {
            //                if (GUILayout.Button("StopPick"))
            //                {
            //                    _isPicking = false;
            //                    if (_slotsCreator.LineOtlined) _slotsCreator.Outline(line.GraphicsNodes);
            //                    line.Nodes = _slotsCreator.CurrentLine.Nodes;
            //                    var lineGO =
            //                        new GameObject("Line" +
            //                                       line.Nodes.Aggregate("",
            //                                           (s, m) => s + "," + m.ToString().Replace(".0", "")));
            //                    lineGO.transform.SetParent(GameObject.Find("Lines").transform);

            //                    var outlineGO = new GameObject("Outline");

            //                    var defaultLineGO = new GameObject("Line");

            //                    defaultLineGO.transform.SetParent(lineGO.transform);
            //                    outlineGO.transform.SetParent(lineGO.transform);

            //                    defaultLineGO.transform.SetAsLastSibling();
            //                    foreach (var image in line.LineGraphics)
            //                    {
            //                        if (image.name.Contains('.')) image.transform.SetParent(outlineGO.transform);
            //                        else image.transform.SetParent(defaultLineGO.transform);
            //                    }
            //                    line.Start.transform.SetParent(lineGO.transform);
            //                    line.End.transform.SetParent(lineGO.transform);
            //                    line.Start.GetComponent<Image>().color = line.LineColor;
            //                    line.End.GetComponent<Image>().color = line.LineColor;
            //                    _slotsCreator.SetStartAndEndPosition(line);
            //                    //_slotsCreator.CurrentLine.Nodes.Clear();
            //                    foreach (var o in _slotsCreator.SymbolsList)
            //                    {
            //                        o.GetComponent<SlotsCell>().ResetColor();
            //                        o.GetComponent<SlotsCell>().Selected = false;
            //                        o.GetComponent<SlotsCell>().Picked = false;
            //                    }

            //                    line.StartPicking = false;
            //                    line.LineID = _slotsCreator.Lines.Count;
            //                    //_slotsCreator.MainGameController.Lines = _slotsCreator.Lines;

            //                    var lineController = defaultLineGO.transform.parent.gameObject.AddComponent<LineController>();
            //                    lineController.LineColor = line.LineColor;
            //                    lineController.Nodes = line.Nodes;
            //                    lineController.GraphicsNodes = line.GraphicsNodes;
            //                    lineController.StartPicking = line.StartPicking;
            //                    lineController.LineGraphics = line.LineGraphics;
            //                    lineController.Start = line.Start;
            //                    lineController.End = line.End;
            //                    lineController.LineID = line.LineID;

            //                    MyWindow.ShowWindow();
            //                }

            //            }

            //        }
            //        _slotsCreator.Lines.Remove(_lineForDestroy);
            //        _slotsCreator.BlinkColor = EditorGUILayout.ColorField("Blink color", _slotsCreator.BlinkColor);
            //        EditorGUILayout.LabelField("Blinking speed");
            //        _slotsCreator.BlinkingSpeed = EditorGUILayout.FloatField(_slotsCreator.BlinkingSpeed);

            //        //Debug.Log(Event.current.mousePosition);
            //    }
        }

    }




    void OnSceneGUI()
    {
        if (_isPicking)
        {
            var r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 10000))
            {
                var go = hit.collider.gameObject;
                var cell = go.GetComponent<GridCell>();
                if (cell != null && !cell.Selected)
                {
                    if (_lastSelctedCell != null)
                    {
                        _lastSelctedCell.GetComponent<SpriteRenderer>().color = _lastSelctedCell.GetComponent<GridCell>().OriginalColor;
                    }

                    go.GetComponent<SpriteRenderer>().color = Color.yellow;
                    cell.Selected = true;
                    _lastSelctedCell = go;
                    _lastSelctedCell.GetComponent<GridCell>().Selected = false;
                    //MyWindow.ShowWindow();
                }
                if (Event.current != null && Event.current.isKey) Debug.Log("KeyDOwn");
                if ((Event.current.type == EventType.MouseDown&&Event.current.button == 1)|| _shiftPressed)
                {
                        var color = new Color();
                        switch (_currentPickMode)
                        {
                            case GridCell.EnumCellState.Buildable:
                                color = Color.green;
                                break;
                            case GridCell.EnumCellState.Walkable:
                                color = Color.white;
                                break;
                            case GridCell.EnumCellState.Blocked:
                                color = Color.red;
                                break;
                            case GridCell.EnumCellState.Path:
                                color = Color.cyan;
                                var controller = FindObjectOfType<GameController>();
                                if (controller.EnemyPath==null)controller.EnemyPath = new List<Vector3>();
                                controller.EnemyPath.Add(go.transform.position);
                                break;
                    }
                    if (cell != null)
                    {
                        cell.State = _currentPickMode;
                        cell.GetComponent<SpriteRenderer>().color = color;
                        cell.OriginalColor = color;
                    }
                    _shiftPressed = false;
                    //var list = new List<GameObject>() {GameObject.Find("MapCreator")};
                    //Selection.objects = list.ToArray();
                    //go.GetComponent<Image>().color = _slotsCreator.CurrentLine.LineColor;

                    //MyWindow.ShowWindow();
                    //_slotsCreator.Pick(go.GetComponent<SlotsCell>().PositionInField, false, _newline, updateCoords);
                    //_newline = false;
                }
            }

        }

    }

}

public class MyWindow : EditorWindow
{
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyWindow)).Close();
    }

    public static void DestroyWindow()
    {

    }

}
