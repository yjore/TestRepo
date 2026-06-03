using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Initiaload : MonoBehaviour
{
    public AssetReference presistenscene;

    private void Awake()
    {
        Addressables.LoadSceneAsync(presistenscene);
    }
}
