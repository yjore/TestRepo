using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    [Header("¼àÌý")]
    public VoidEventSO saveEvent;
    public VoidEventSO loadDataEvent;
    public static DataManager instance;
    public List<IsSaveable> saveableList = new List<IsSaveable>();
    private Data saveData;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        saveData = new Data();
    }
    private void OnEnable()
    {
        saveEvent.OnEventRaised += save;
        loadDataEvent.OnEventRaised += Load;
    }
    private void OnDisable()
    {
        saveEvent.OnEventRaised -= save;
        loadDataEvent.OnEventRaised += Load;
    }
    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Load();
        }
    }
    public void RegisterSaveData(IsSaveable saveable)
    {
        
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }
    public void UnRegisterSaveData(IsSaveable saveable)
    {
        saveableList.Remove(saveable);
    }
    public void save()
    {
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(saveData);
        }
        foreach(var item in saveData.chararcterPosDict)
        {
            Debug.Log(item.Key+"    "+item.Value);
        }
    }
    public void Load()
    {
        foreach (var saveable in saveableList)
        {
            saveable.LoadSaveData(saveData);
        }
    }
}
