using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) { Instance = this;}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(TextMeshProUGUI textObject, string text)
    {
        textObject.text = text;
    }

}
