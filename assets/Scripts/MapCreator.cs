using UnityEngine;
using System.Collections;

public class MapCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [SerializeField]
    public int RawCount { get; set; }
    [SerializeField]
    public int ColumnCount { get; set; }
    [SerializeField]
    public float CellXSize { get; set; }
    [SerializeField]
    public float CellZSize { get; set; }
    [SerializeField]
    public Vector3 CenterPos { get; set; }
    [SerializeField]
    public bool MapCreated { get; set; }
}
