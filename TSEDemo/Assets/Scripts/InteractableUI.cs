using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hoverOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hoverOver) { if (Input.GetMouseButtonDown(0)) { UserControl.Instance.ClickOnInteractable(gameObject); } }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOver = false;
    }
}
