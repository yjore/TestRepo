using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float persentage;
    public float ControlExertion;
    [Header("¼àÌý")]
    public CharacterEventSO healthEvent;
    public PlayerStatBar PlayerStatBar;
    public SceneLoadEventSO UnloadedSceneEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO GameOverEvent;
    public VoidEventSO BackEvent;
    public FloatEventSO syncVolumneEvent;
    [Header("¹ã²¥")]
    public VoidEventSO PauseEvent;

    [Header("×é¼þ")]
    public GameObject GameOverPanel;
    public GameObject restartBtn;
    public Button settingBtr;
    public GameObject pausePanel;
    public Slider volumeSlider;
    private void Awake()
    {
        settingBtr.onClick.AddListener(TogglePausePanel);
    }

    private void TogglePausePanel()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            PauseEvent.OnEventRaised();
            
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        healthEvent.OnEventExertion += OnPhysicalExertion;
        UnloadedSceneEvent.LoadRequestEvent += UIEvent;
        loadDataEvent.OnEventRaised += OnloadDataEvent;
        GameOverEvent.OnEventRaised += OnGameOverEvent;
        BackEvent.OnEventRaised += OnloadDataEvent;
        syncVolumneEvent.OnEventRaised += OnSyncVolumeEvent;
    }

    

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        healthEvent.OnEventExertion -= OnPhysicalExertion;
        UnloadedSceneEvent.LoadRequestEvent -= UIEvent;
        loadDataEvent.OnEventRaised -= OnloadDataEvent;
        GameOverEvent.OnEventRaised -= OnGameOverEvent;
        BackEvent.OnEventRaised -= OnloadDataEvent;
        syncVolumneEvent.OnEventRaised -= OnSyncVolumeEvent;
    }
    private void OnSyncVolumeEvent(float amount)
    {
        volumeSlider.value = (amount + 80)/100;
    }
    private void OnGameOverEvent()
    {
        GameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartBtn);
    }

    private void OnloadDataEvent()
    {
        GameOverPanel.SetActive(false);
    }

    private void UIEvent(GameSceneSO SceneToGo, Vector3 arg1, bool arg2)
    {
        var isMenu = SceneToGo.SceneType == SceneType.Menu;
        PlayerStatBar.gameObject.SetActive(!isMenu);
    }

    private void OnPhysicalExertion(PlayerControl playerControl)
    {
        if (playerControl.isSprint)
        {
            PlayerStatBar.OnPowerExertion(persentage);
        }
    }

    

    private void OnHealthEvent(Character character)
    {
        var persentage = character.CurrentHealth / character.MaxHealth;
        PlayerStatBar.OnHealthChange(persentage);
    }
}
