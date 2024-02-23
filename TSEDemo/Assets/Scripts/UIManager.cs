using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject touchScreenUI;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI incorrectText;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(TextMeshProUGUI textObject, string text)
    {
        textObject.text = text;
    }

    public void updateTouchScreenUI()
    {
        touchScreenUI.SetActive(GameManager.Instance.touchscreen);
    }

    public void UpdateTimer(float time)
    {
        UpdateText(timer, Mathf.Floor(time).ToString());
    }

}
