using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwappableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int value;
    public Sprite originalImage;
    public Image image;
    public bool hoverOver = false;

    [SerializeField] TextMeshProUGUI valueText;

    private void Start()
    {
        originalImage = image.sprite;
    }

    private void Update()
    {
        Click();
    }

    public void SetOrignalImage(Sprite sprite)
    {
        originalImage = sprite;
    }

    public void ChangeValue(int value)
    {
        this.value = value;
        OnValueUpdate();
    }

    public void ChangeImage(Sprite sprite)
    {
        image.sprite = sprite;
        OnValueUpdate();
    }

    public void Click()
    {
        if (!hoverOver) { return; }
        UserControl.Instance.ClickOnInteractable(gameObject);
    }

    public void OnValueUpdate()
    {
        image.sprite = image.sprite;
        valueText.text = value.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOver = false;
    }
}
