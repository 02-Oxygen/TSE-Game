using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserControl : MonoBehaviour
{
    public static UserControl Instance;

    [SerializeField] GameObject first;
    [SerializeField] GameObject second;

    private GameControls gamecontrols;


    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        gamecontrols = GameManager.Instance.gameControls;
    }

    public void ClickOnInteractable(GameObject interactable)
    {
        if (!gamecontrols.Gameplay.Interact.WasPerformedThisFrame()) { return; }
        if (first == interactable || second == interactable) { return; }

        if (first == null) { first = interactable; }
        else if (second == null)
        { 
            second = interactable;
            SortBehaviour.Instance.bubblesort.BoxSwap(first, second);
            ClearSwap();
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ClearSwap();
        }
    }

    public void Confirm(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed) { return; }
        SortBehaviour.Instance.CheckCanContinue();
        Instance.ClearSwap();
    }

    public void Cancel(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed) { return; }
        ClearSwap();
    }

    public void ResetArray(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed) { return; }
        SortBehaviour.Instance.ResetToArray();
        Instance.ClearSwap();
    }

    public void Newarray(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed) { return; }
        SortBehaviour.Instance.StartSort();
        Instance.ClearSwap();
    }

    public void ClearSwap()
    {
        first = null;
        second = null;
    }
}
