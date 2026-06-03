using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour , IsSaveable
{
    [Header("事件监听")]
    public VoidEventSO NewGanmeEvent;
    [Header("基础属性")]
    public float MaxHealth;
    public float CurrentHealth;
    [Header("无敌属性")]
    public float InvulnerableDuration;
    private float InvulnerableCount;
    public bool Invulnerable;

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OntakeDamage;
    
    public UnityEvent OnDead;
    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke(this);
    }
    private void NewGame()
    {
        CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke(this);
    }
    private void OnEnable()
    {
        NewGanmeEvent.OnEventRaised += NewGame;
        IsSaveable saveable = this;
        saveable.RegisterSaveData();

    }
    private void OnDisable()
    {
        NewGanmeEvent.OnEventRaised -= NewGame;
        IsSaveable saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void Update()
    {
        if (Invulnerable)
        {
            InvulnerableCount -= Time.deltaTime;
            if (InvulnerableCount <= 0)
            {
                Invulnerable = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            CurrentHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDead?.Invoke();
        }
    }
    public void TakeDamage(Attack attacker)
    {
        if (Invulnerable)
        {
            return;
        }
        if (CurrentHealth-attacker.Damage > 0)
        {
            CurrentHealth -= attacker.Damage;
            InvulnerableFuc();
            OntakeDamage?.Invoke(attacker.transform);
        }
        else if(CurrentHealth-attacker.Damage <= 0)
        {
            CurrentHealth = 0;
            OnDead?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }

    public void InvulnerableFuc()
    {
        if (!Invulnerable)
        {
            Invulnerable = true;
            InvulnerableCount = InvulnerableDuration;
        }
    }

    public DataDefination GetDatatID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.chararcterPosDict.ContainsKey(GetDatatID().ID))
        {
            data.chararcterPosDict[GetDatatID().ID] = transform.position;
            data.floatSaveData[GetDatatID().ID + "Health"] = this.CurrentHealth;
        }
        else
        {
            data.chararcterPosDict.Add(GetDatatID().ID, transform.position);
            data.floatSaveData.Add(GetDatatID().ID + "Health", this.CurrentHealth);
        }
    }

    public void LoadSaveData(Data data)
    {
        if (data.chararcterPosDict.ContainsKey(GetDatatID().ID))
        {
            transform.position = data.chararcterPosDict[GetDatatID().ID];
            CurrentHealth = data.floatSaveData[GetDatatID().ID + "Health"];
        }
        OnHealthChange?.Invoke(this);
    }
}
