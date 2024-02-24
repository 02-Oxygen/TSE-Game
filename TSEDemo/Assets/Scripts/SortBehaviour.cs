using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class SortBehaviour : MonoBehaviour
{
    public static SortBehaviour Instance;
    public int[] array;
    public int arrayMax;
    public GameObject[] rockObjectArray;

    [SerializeField] Transform parentPanel;   

    public Sort sort;
    public bool canContinue;
    public bool finished;


    private float time = 0f;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {
        arrayMax = array.Length;
        StartSort();
    }

    public void StartSort()
    {
        time = 0f;
        canContinue = false;
        finished = false;
        sort?.DestroyBoxes();
        RandomiseAmountofElements();
        RandomiseArray();
        CreateArrayBoxes();
        sort = new BubbleSort(array, rockObjectArray[0].GetComponentInChildren<TextMeshProUGUI>().color ,parentPanel);
    }

    public void ResetToArray()
    {
        sort.ResetToArray();
    }

    public void RandomiseAmountofElements()
    {
        System.Random rnd = new();
        int randNum = rnd.Next(5, arrayMax);
        Array.Clear(array, 0, array.Length);
        array = new int[randNum];
    }


    void RandomiseArray()
    {
        System.Random rnd = new();
        for (int i = 0; i < array.Length; i++) 
        {
            array[i] = rnd.Next(0, 1000);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished) { time += Time.deltaTime; }
        UIManager.Instance.UpdateTimer(time);
        if (Input.GetKeyDown(KeyCode.Backspace)) { sort.ResetToArray(); }
        if (canContinue) { canContinue = false; NextElement();}
    }


    public void CheckCanContinue()
    {
        if (finished) { return; }
        canContinue = true;
    }

    void NextElement()
    {
        finished = sort.NextStep(parentPanel);
        if (sort.i > sort.max - 2) { sort.p++; sort.i = 0;}
        if (finished) { sort.HighlightCurrent(true); GameStats.Instance.SetScore(sort.correct, sort.incorrect, time); }
    }

    void CreateArrayBoxes()
    {
        System.Random rnd = new();
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
