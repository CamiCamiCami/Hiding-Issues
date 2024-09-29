using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AbstractManager : MonoBehaviour
{
    public abstract class SceneData : ICloneable
    {
        public abstract object Clone();
    }

    public abstract Type GetSceneDataType();

    public abstract SceneData GetSceneData();

    public virtual void SetSceneData(SceneData sceneData)
    {
        return;
    }

    static public void ChangeScene(AbstractManager save, SceneAsset load)
    {
        GameManager.Instance.SaveData(save.GetSceneData());
        UnityEngine.SceneManagement.SceneManager.LoadScene(load.name);
    }
}
