using UnityEngine;
using System.Collections;

public class MouseCollider : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;
    private Transform _lastHittedTransform;
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.name == "cell")
                {
                    Debug.Log("Click");
                }

            }
            if (hit.transform != _lastHittedTransform && hit.transform!=null)
            {
                if (hit.transform.name == "cell")
                {
                    //hit.transform.GetComponent<GridCell>().HighLight();
                    Debug.Log("High");
                    _lastHittedTransform = hit.transform;
                }
                else
                {
                    if (_lastHittedTransform != null)
                    {
                        Debug.Log("Normal");
                        //_lastHittedTransform.GetComponent<GridCell>().HighLight(false);
                        _lastHittedTransform = null;
                    }
                }
            }
            
        }
        else
        {
            if (_lastHittedTransform != null)
            {
                Debug.Log("Normal");
                //_lastHittedTransform.GetComponent<GridCell>().HighLight(false);
                _lastHittedTransform = null;
            }
        }
    }
}
