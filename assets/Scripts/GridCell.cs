using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GridCell : MonoBehaviour
{
    private GridCell _script;
    
    public bool Busy;
    //private SpriteRenderer _spriteRenderer;
    //private Sprite _normalSprite;
    //public Sprite HighlitedSprite;
    //private bool _isHighlited;
    public GameObject GO;
    public EnumCellState State;
    public bool Selected;
    public Color OriginalColor { get; set; }

    void Start()
    {
        AddTriger(gameObject);
    }
    public GridCell(Vector3 position)
    {
        var go = Skin.GetPrefab(Skin.GamePrefabs.Cell);
        GO = Instantiate(go, position, go.transform.rotation) as GameObject;
        OriginalColor = GO.GetComponent<SpriteRenderer>().color;
        _script = GO.AddComponent<GridCell>();
        _script.GO = GO;
        _script.OriginalColor = OriginalColor;

    }

    private void AddTriger(GameObject go)
    {
        var trigger = go.AddComponent<EventTrigger>();
        EventTrigger.Entry entry1;
        entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerClick;
        entry1.callback = new EventTrigger.TriggerEvent();
        entry1.callback.AddListener(listener => OnClick());
        var entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback = new EventTrigger.TriggerEvent();
        entry2.callback.AddListener(listener => OnHower());
        var entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.PointerExit;
        entry3.callback = new EventTrigger.TriggerEvent();
        entry3.callback.AddListener(listener => OnOut());
        trigger.triggers = new List<EventTrigger.Entry> { entry1, entry2, entry3 };
    }

    private void OnOut()
    {
        Debug.Log("Out");
    }

    private void OnHower()
    {
        Debug.Log("Hower");
    }

    private void OnClick()
    {
        Debug.Log("Click");
    }

    public enum EnumCellState
    {
        Buildable,
        Walkable,
        Blocked,
        Path
    }
}
