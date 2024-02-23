using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameControls gameControls;
    public bool touchscreen = false;


    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        gameControls = new GameControls();
    }

    private void Start()
    {
        UIManager.Instance.updateTouchScreenUI();
        OnControlsChanged(GetComponent<PlayerInput>());
    }

    public void OnControlsChanged(PlayerInput playerInput)
    {
        touchscreen = playerInput.currentControlScheme == "TouchScreen";
        if (UIManager.Instance == null) { return; }
        UIManager.Instance.updateTouchScreenUI();
    }

    private void OnEnable()
    {
        gameControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        gameControls.Gameplay.Disable();
    }


}
