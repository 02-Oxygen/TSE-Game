using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableUI : MonoBehaviour
{
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

}
