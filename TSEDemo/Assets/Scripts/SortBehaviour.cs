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

    [SerializeField] TextMeshProUGUI correctText;
    [SerializeField] TextMeshProUGUI incorrectText;

   public Sort bubblesort;


    bool canContinue;
    public bool finished;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSort();
    }

    void StartSort()
    {
        canContinue = false;
        finished = false;
        if (bubblesort != null) { bubblesort.DestroyBoxes();}
        RandomiseArray();
        CreateArrayBoxes();
        bubblesort = new BubbleSort(array, rockObjectArray[0].GetComponentInChildren<TextMeshProUGUI>().color ,parentPanel);
        correctText.text = "Correct: 0";
        incorrectText.text = "Incorrect: 0";
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
        if (Input.GetKeyDown(KeyCode.R)) { StartSort(); }
        if (Input.GetKeyDown(KeyCode.Backspace)) { bubblesort.ResetToArray(); }
        if (Input.GetKeyDown(KeyCode.Return) && !finished) { canContinue = true; }
        if (canContinue) { canContinue = false; NextElement();}
    }
    void NextElement()
    {
        finished = bubblesort.NextStep(ref correctText, ref incorrectText, parentPanel);
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
            boxObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = array[i].ToString();
        }
    }
}
