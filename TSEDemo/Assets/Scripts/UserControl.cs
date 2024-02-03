using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    public static UserControl Instance;

    [SerializeField] GameObject first;
    [SerializeField] GameObject second;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    public void ClickOnInteractable(GameObject interactable)
    {
        if (first == interactable || second == interactable) { return; }

        if (first == null) { first = interactable; }
        else if (second == null)
        { 
            second = interactable;
            SortBehaviour.Instance.bubblesort.BoxSwap(int.Parse(first.transform.parent.name), int.Parse(second.transform.parent.name));
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

    void ClearSwap()
    {
        first = null;
        second = null;
    }
}
