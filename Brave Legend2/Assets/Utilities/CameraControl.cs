using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
    public VoidEventSO AfterLoadingEventSO;
    public CinemachineConfiner2D Confiner;
    public CameraEventSO CameraEventSO;
    public CinemachineImpulseSource CinemachineImpulseSource;

    private void Awake()
    {
        Confiner = GetComponent<CinemachineConfiner2D>();

    }
    
    private void OnEnable()
    {
        CameraEventSO.OnEventRaised += OnCameraShake;
        AfterLoadingEventSO.OnEventRaised += OnGetBounds;
    }
    private void OnDisable()
    {
        CameraEventSO.OnEventRaised -= OnCameraShake;
        AfterLoadingEventSO.OnEventRaised -= OnGetBounds;
    }

    private void OnGetBounds()
    {
        GetNewCameraBounds();
    }

    private void OnCameraShake()
    {
        CinemachineImpulseSource.GenerateImpulse();
    }

    public void GetNewCameraBounds()
    {

        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj==null)
            return;
        Confiner.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        
        Confiner.InvalidateCache();
    }
}
