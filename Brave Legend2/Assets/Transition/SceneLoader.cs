using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour , IsSaveable
{
    public Vector3 firstPosition;
    private Vector3 PositionToGo;
    public Vector3 MenuPosition;
    public float fadeDuration;

    public Transform PlayerPosition;
    [Header("事件的监听")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO newGameEvent;
    public VoidEventSO backToMenuEvent;
    [Header("广播")]
    public SceneLoadEventSO unLoadEvent;
    public GameSceneSO menuScene;
    public GameSceneSO firstGameScene;
    public FadeEventSO FadeEventSO;
    private GameSceneSO currentScene;
    public VoidEventSO AfterLoadingEvent;
    private GameSceneSO SceneToLoad;


    private bool fadeScreen;
    
    private void Awake()
    {
    }
    private void Start()
    {
        /*OnNewGame();*/
        loadEventSO.RaiseLoadRequestEvent(menuScene,MenuPosition,true);


        IsSaveable saveable = this;
        saveable.RegisterSaveData();

    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
        newGameEvent.OnEventRaised += OnNewGame;
        backToMenuEvent.OnEventRaised += backEvent;
        


    }

    

    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
        newGameEvent.OnEventRaised -= OnNewGame;
        backToMenuEvent.OnEventRaised -= backEvent;

        IsSaveable saveable = this;
        saveable.UnRegisterSaveData();
    }
    private void OnNewGame()
    {
        SceneToLoad = firstGameScene;
        /*OnLoadRequestEvent(SceneToLoad,firstPosition,true);*/
        loadEventSO.RaiseLoadRequestEvent(SceneToLoad,firstPosition,true);
    }
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        SceneToLoad = locationToLoad;
        PositionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        if (currentScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            loadNewScene();
        }
        
    }
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO;
            FadeEventSO.FadeIn(fadeDuration);
            
        }
        yield return new WaitForSeconds(fadeDuration);
        unLoadEvent.RaiseLoadRequestEvent(SceneToLoad,PositionToGo,true);
        yield return currentScene.sceneReference.UnLoadScene();
        PlayerPosition.gameObject.SetActive(false);
        loadNewScene();
        
    }
    private void loadNewScene()
    {
        var lodingOption = SceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
        lodingOption.Completed += OnPrepare;
    }

    private void backEvent()
    {
        SceneToLoad = menuScene;
        loadEventSO.LoadRequestEvent(SceneToLoad,MenuPosition,true);
    }
    private void OnPrepare(AsyncOperationHandle<SceneInstance> handle)
    {
        currentScene = SceneToLoad;
        if (fadeScreen)
        {
            //TODO;
            FadeEventSO.FadeOut(fadeDuration);
        }
        PlayerPosition.position = PositionToGo;
        PlayerPosition.gameObject.SetActive(true);
        if(currentScene.SceneType != SceneType.Menu)
        AfterLoadingEvent.RaiseEvent();
    }

    public DataDefination GetDatatID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        data.SaveGameScene(currentScene);
    }

    public void LoadSaveData(Data data)
    {
        var playerID = PlayerPosition.GetComponent<DataDefination>().ID;
        if (data.chararcterPosDict.ContainsKey(playerID))
        {
            PositionToGo = data.chararcterPosDict[playerID];
            SceneToLoad = data.GetSavedScene();

            OnLoadRequestEvent(SceneToLoad, PositionToGo, true);
        }
        
    }
}
