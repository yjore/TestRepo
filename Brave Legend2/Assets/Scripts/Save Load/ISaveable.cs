using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public interface IsSaveable
{
    DataDefination GetDatatID();
    void RegisterSaveData() => DataManager.instance.RegisterSaveData(this);
    void UnRegisterSaveData() => DataManager.instance.UnRegisterSaveData(this);

    void GetSaveData(Data data);
    void LoadSaveData(Data data);
}
