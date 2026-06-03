using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image HealthFillGreen;
    public Image HealthFillRed;
    public Image HealthFillYellow;

    public float ExertionSpeed;

    public static PlayerStatBar Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (HealthFillGreen.fillAmount < HealthFillRed.fillAmount)
        {
            HealthFillRed.fillAmount -= Time.deltaTime;
        }
        if (HealthFillYellow.fillAmount < 1)
        {
            HealthFillYellow.fillAmount += Time.deltaTime*ExertionSpeed;
        }
    }
    public void OnHealthChange(float persentage)
    {
        HealthFillGreen.fillAmount = persentage;
    }
    public void OnPowerExertion(float persentage)
    {
        HealthFillYellow.fillAmount -= persentage;
    }
}
