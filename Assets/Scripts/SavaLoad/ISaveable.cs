using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    void SaveableRegister()
    {
        SaveLoadMgr.Instance.Register(this);
    }

    GameSaveData GenerateSaveData();

    void RestoreGameData(GameSaveData saveData);
}