using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO GameSceneToGo;
    public Vector3 PositionToGo;
    public void TriggerAction()
    {
        Debug.Log("´«ËÍ");
        loadEventSO.RaiseLoadRequestEvent(GameSceneToGo,PositionToGo,true);
    }
}
