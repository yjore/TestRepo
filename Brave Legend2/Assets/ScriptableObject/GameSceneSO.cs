using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName ="GameScenes/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType SceneType;
    public AssetReference sceneReference;
}
