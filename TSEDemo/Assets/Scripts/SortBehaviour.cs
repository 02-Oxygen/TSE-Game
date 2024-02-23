using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class SortBehaviour : MonoBehaviour
{
    public static SortBehaviour Instance;
    public int[] array;
    public GameObject[] rockObjectArray;

    [SerializeField] Transform parentPanel;   

    public Sort bubblesort;
    public bool canContinue;
    public bool finished;


    private UIManager uiManager;
    private float time = 0f;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.Instance;
        StartSort();
    }

    public void StartSort()
    {
        time = 0f;
        canContinue = false;
        finished = false;
        if (bubblesort != null) { bubblesort.DestroyBoxes();}
        RandomiseArray();
        CreateArrayBoxes();
        bubblesort = new BubbleSort(array, rockObjectArray[0].GetComponentInChildren<TextMeshProUGUI>().color ,parentPanel);
        uiManager.correctText.text = "Correct: 0";
        uiManager.incorrectText.text = "Incorrect: 0";
    }

    public void ResetToArray()
    {
        bubblesort.ResetToArray();
    }


    void RandomiseArray()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < array.Length; i++) 
        {
            array[i] = rnd.Next(0, 1000);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        UIManager.Instance.UpdateTimer(time);
        if (Input.GetKeyDown(KeyCode.Backspace)) { bubblesort.ResetToArray(); }
        if (canContinue) { canContinue = false; NextElement();}
    }

    public void CheckCanContinue()
    {
        if (finished) { return; }
        canContinue = true;
    }

    void NextElement()
    {
        finished = bubblesort.NextStep(ref uiManager.correctText, ref uiManager.incorrectText, parentPanel);
        if (bubblesort.i > bubblesort.max - 2) { bubblesort.p++; bubblesort.i = 0;}
        if (finished) { bubblesort.HighlightCurrent(true); }
    }

    void CreateArrayBoxes()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < array.Length; i++)
        {
            int randNum = rnd.Next(0, rockObjectArray.Length);
            GameObject boxObject = Instantiate(rockObjectArray[randNum], parentPanel);
            boxObject.name = i.ToString();
            boxObject.GetComponent<SwappableObject>().value = array[i];
            boxObject.GetComponent<SwappableObject>().OnValueUpdate();
        }
    }
}
