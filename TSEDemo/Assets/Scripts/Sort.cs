using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

[Serializable]
public abstract class Sort
{
    public int[] array;
    public int max;
    public int correct;
    public int incorrect;
    public int[] resetArray;
    public int i;
    public int p;
    public Transform parentPanel;

    public Sort(int[] array, Transform parentPanel)
    {
        this.array = array;
        this.parentPanel = parentPanel;
        max = array.Length;
        resetArray = new int[array.Length];
        correct = 0;
        incorrect = 0;
        i = 0;
        p = 0;
        HighlightCurrent(false);
    }

    public abstract void NextStep(ref TextMeshProUGUI correctText, ref TextMeshProUGUI incorrectText, Transform parentPanel);

    protected bool CheckBoxes(int a, int b)
    {

        if (int.Parse(parentPanel.GetChild(a).GetComponentInChildren<TextMeshProUGUI>().text) == array[a])
        {
            if (int.Parse(parentPanel.GetChild(b).GetComponentInChildren<TextMeshProUGUI>().text) == array[b])
            {
                return true;
            }
        }

        return false;
    }


    public void BoxSwap(int namea, int nameb)
    {
        string temp = parentPanel.GetChild(namea).GetComponentInChildren<TextMeshProUGUI>().text;
        parentPanel.GetChild(namea).GetComponentInChildren<TextMeshProUGUI>().text = parentPanel.GetChild(nameb).GetComponentInChildren<TextMeshProUGUI>().text;
        parentPanel.GetChild(nameb).GetComponentInChildren<TextMeshProUGUI>().text = temp;
    }

    public void HighlightCurrent(bool finished)
    {
        if (finished)
        {
            foreach (Transform box in parentPanel)
            {
                box.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
            }
        }
        else
        {

            int index = i;
            if (i > max - 2) { index = 0; }

            foreach (Transform box in parentPanel)
            {
                if (int.Parse(box.name) == index)
                {
                    box.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                }
                else box.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
        }
    }

    public void ResetToArray()
    {
        for (int i = 0; i < max; i++)
        {
            parentPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = array[i].ToString();
        }
    }

    public void DestroyBoxes()
    {
        foreach(Transform box in parentPanel)
        {
            MonoBehaviour.Destroy(box.gameObject);
        }
    }

}

[Serializable]
public class BubbleSort : Sort
{
    public BubbleSort(int[] array, Transform parentPanel) : base(array, parentPanel) { }

    public override void NextStep(ref TextMeshProUGUI correctText, ref TextMeshProUGUI incorrectText, Transform parentPanel)
    {
        array.CopyTo(resetArray, 0);
        if (array[i] > array[i + 1])
        {
            (array[i + 1], array[i]) = (array[i], array[i + 1]);
        }

        if (CheckBoxes(i, i + 1)) 
        {
            correct++; 
            UIManager.Instance.UpdateText(correctText, "Correct: " + correct); 
            i++; 
            HighlightCurrent(p > max - 2);
        }
        else
        {
            incorrect++;
            UIManager.Instance.UpdateText(incorrectText, "Incorrect: " + incorrect);
            Array.Clear(array, 0, max);
            resetArray.CopyTo(array, 0);
            ResetToArray();
        }
    }

}