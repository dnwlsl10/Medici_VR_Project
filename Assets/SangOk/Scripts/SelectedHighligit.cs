using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedHighligit : MonoBehaviour
{
    [SerializeField] string seletableTag = "buttons";

    private Transform _selection;
    
    void Update()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material.color = Color.white;
            _selection = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag(seletableTag))
            {
                var selectionRendere = selection.GetComponent<Renderer>();
                if (selectionRendere != null)
                {
                    selectionRendere.material.color = Color.yellow;
                    if(selectionRendere.material.color == Color.green)
                    {
                        
                    }
                }

                _selection = selection;
            }
        }
    }
}
