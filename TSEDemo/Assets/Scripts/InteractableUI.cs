using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ButtonType buttonType;
    public enum ButtonType 
    {
        None,
        Swappable,
    }

    public bool hoverOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonType == ButtonType.Swappable) { if (hoverOver) { UserControl.Instance.ClickOnInteractable(gameObject); } }
    }

    public void OnPressConfirm()
    {
        SortBehaviour.Instance.CheckCanContinue();
        UserControl.Instance.ClearSwap();
    }

    public void OnSwapCancel()
    {
        UserControl.Instance.ClearSwap();
    }

    public void OnReset()
    {
        SortBehaviour.Instance.ResetToArray();
        UserControl.Instance.ClearSwap();
    }

    public void OnRenew()
    {
        SortBehaviour.Instance.StartSort();
        UserControl.Instance.ClearSwap();
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
