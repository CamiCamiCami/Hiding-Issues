using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : AbstractManager
{
    public static GameManager Instance { get; private set; }


    GameManagerData gameData = new GameManagerData();

    void Start()
    {
        Instance = this;
    }

    public T LoadData<T>()
    {
        if (!typeof(T).IsSubclassOf(typeof(SceneData)))
        {
            Debug.LogError("LoadData fue llamado con un tipo invalido");
            throw new ArgumentException("LoadData fue llamado con un tipo invalido");
        }

        SceneData data = gameData.GetSceneData(typeof(T));
        return (T)Convert.ChangeType(data, typeof(T));
    }

    public void SaveData<T>(T sceneData)
    {
        if (!typeof(T).IsSubclassOf(typeof(SceneData)))
        {
            Debug.LogError("SaveData fue llamado con un tipo invalido");
            throw new ArgumentException("SaveData fue llamado con un tipo invalido");
        }

        SceneData data = (SceneData)Convert.ChangeType(sceneData, typeof(SceneData));
        gameData.SaveSceneData(data);
    }

    public static void LoadGameManager()
    {
        SceneManager.LoadScene("Game Manager", LoadSceneMode.Additive);
    }

    /* Metodos Heredados */

    public override Type GetSceneDataType()
    {
        return typeof(GameManagerData);
    }

    public override SceneData GetSceneData()
    {
        throw new NotImplementedException();
    }

    /* Scene Data class */

    private class GameManagerData : SceneData
    {

        /* Defaults Acá */
        int playerAmount = 1;
        (Vector3, Quaternion, Character)[] playersData = new (Vector3, Quaternion, Character)[1] {( new Vector3(0, 0, 0),
                                                                                                    new Quaternion(0, 0, 0, 0),
                                                                                                    Character.Tyrone)};


        public override object Clone()
        {
            Debug.LogError("No se debe clonar GameManagerData puesto que es unico");
            throw new System.NotImplementedException();
        }

        public void SaveSceneData(SceneData data)
        {
            PropertyInfo[] properties = data.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                SetProperty(this, data, property);
            }
        }

        public SceneData GetSceneData(Type sceneDateType)
        {
            SceneData sceneData = (SceneData)Activator.CreateInstance(sceneDateType);

            PropertyInfo[] properties = sceneDateType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                SetProperty(sceneData, this, property);
            }

            return sceneData;
        }

        private static void SetProperty(object to, object from, PropertyInfo property)
        {
            object value = property.GetValue(from);
            to.GetType().InvokeMember(property.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, to, new object[1] { value });
        }
    }
}
