using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UIElements.Image;

[Serializable]
public abstract class Sort
{
    public int[] array;
    public int[] sortedArray;
    public int max;
    public int correct;
    public int incorrect;
    public int[] resetArray;
    public int i;
    public int p;
    public Transform parentPanel;
    Color textColour;

    public Sort(int[] array, Color textColour,Transform parentPanel)
    {
        this.array = array;
        this.parentPanel = parentPanel;
        this.textColour = textColour;
        max = array.Length;
        resetArray = new int[array.Length];
        SortArray();
        correct = 0;
        incorrect = 0;
        i = 0;
        p = 0;
        HighlightCurrent(false);
    }

    public abstract bool NextStep(ref TextMeshProUGUI correctText, ref TextMeshProUGUI incorrectText, Transform parentPanel);
    public abstract void SortArray();

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


    public void BoxSwap(int first, int second)
    {
        SwappableObject firstObject = parentPanel.GetChild(first).GetComponent<SwappableObject>();
        SwappableObject secondObject = parentPanel.GetChild(second).GetComponent<SwappableObject>();

        int firstValue = firstObject.value;
        firstObject.ChangeValue(secondObject.value);
        secondObject.ChangeValue(firstValue);

        Sprite firstSprite = firstObject.image.sprite;
        firstObject.ChangeImage(secondObject.image.sprite);
        secondObject.ChangeImage(firstSprite);
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
                else box.GetComponentInChildren<TextMeshProUGUI>().color = textColour;
            }
        }
    }

    public void ResetToArray()
    {
        for (int i = 0; i < max; i++)
        {
            parentPanel.GetChild(i).GetComponent<SwappableObject>().ChangeValue(array[i]);
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
    public BubbleSort(int[] array, Color textColour,Transform parentPanel) : base(array, textColour ,parentPanel) { }

    public override bool NextStep(ref TextMeshProUGUI correctText, ref TextMeshProUGUI incorrectText, Transform parentPanel)
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
            HighlightCurrent(false);
        }
        else
        {
            incorrect++;
            UIManager.Instance.UpdateText(incorrectText, "Incorrect: " + incorrect);
            Array.Clear(array, 0, max);
            resetArray.CopyTo(array, 0);
            ResetToArray();
        }

        return Enumerable.SequenceEqual(array, sortedArray);
    }

    public override void SortArray()
    {
        sortedArray = new int[max];
        array.CopyTo(sortedArray, 0);
        Array.Sort(sortedArray);
    }

}
