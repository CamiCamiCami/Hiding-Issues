using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AbstractManager;

public class GameManager : AbstractManager
{
    public static GameManager Instance { get; private set; }


    readonly GameManagerData gameData = new GameManagerData();

    void Awake()
    {
        Instance = this;
    }

    int tempo = 60;
    void Update()
    {
        tempo--;
        if (tempo == 0)
        {
            Debug.Log(this.gameData.ToString());
            tempo = 60;
        }
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

    public void SaveData(SceneData sceneData)
    {
        //SceneData data = (SceneData)Convert.ChangeType(sceneData, typeof(SceneData));
        gameData.SaveSceneData(sceneData);
    }

    public static void LoadGameManager()
    {
        const string name = "Game Manager";
        GameObject gameManager = new GameObject(name, new Type[1] {typeof(GameManager)});
        DontDestroyOnLoad(gameManager);
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
        public int PlayerAmount = 1;
        public (Vector3, Quaternion, Character)[] PlayersData = new (Vector3, Quaternion, Character)[1] {( new Vector3(0, 1, 0), new Quaternion(0, 0, 0, 0), Character.Emma)};
        public string[] SolvedPuzzles = new string[0];


        public override object Clone()
        {
            Debug.LogError("No se debe clonar GameManagerData puesto que es unico");
            throw new System.NotImplementedException();
        }

        public void SaveSceneData(SceneData data)
        {
            FieldInfo[] fields = data.GetType().GetFields();

            foreach (FieldInfo field in fields)
            {
                Debug.Log($"Guardando field {field.Name} de {data.GetType().Name}");
                if (field.FieldType == typeof((Vector3, Quaternion, Character)[]))
                {
                    (Vector3, Quaternion, Character)[] array = ((Vector3, Quaternion, Character)[])field.GetValue(data);
                    if (array != null)
                    {
                        (Vector3 vector, Quaternion _, Character _) = array[0];
                        Debug.Log($"Guardado vector: x={vector.x}, y={vector.y}, z={vector.z}");
                    }
                }
                SetField(this, data, field.Name);
            }
        }

        public SceneData GetSceneData(Type sceneDateType)
        {
            SceneData sceneData = (SceneData)Activator.CreateInstance(sceneDateType);
            FieldInfo[] fields = sceneDateType.GetFields();

            foreach (FieldInfo field in fields)
            {
                Debug.Log($"Cargando field {field.Name} de {sceneDateType.Name}");
                if (field.FieldType == typeof((Vector3, Quaternion, Character)[]))
                {
                    (Vector3, Quaternion, Character)[] array = ((Vector3, Quaternion, Character)[])this.GetType().GetField(field.Name).GetValue(this);
                    if (array != null)
                    {
                        (Vector3 vector, Quaternion _, Character _) = array[0];
                        Debug.Log($"Cargado vector: x={vector.x}, y={vector.y}, z={vector.z}");
                    }
                }
                SetField(sceneData, this, field.Name);
            }
            return sceneData;
        }

        private static void SetField(object to, object from, string fieldName)
        {
            object value = GetPossiblyClonedValue(from, fieldName);
            to.GetType().InvokeMember(fieldName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField, Type.DefaultBinder, to, new object[1] { value });
        }

        private static object GetPossiblyClonedValue(object from, string fieldName) 
        {
            Debug.Log($"Consiguiendo el valor posiblemente clonado de {fieldName}");
            const string ClonableInterface = nameof(ICloneable);
            FieldInfo field = from.GetType().GetField(fieldName);
            Type fieldType = field.FieldType;
            if (fieldType.GetInterface(ClonableInterface) != null)
            {
                Debug.Log($"Devolviendo el valor clonado de {fieldName}");
                ICloneable clonable = (ICloneable)field.GetValue(from);
                return clonable.Clone();
            } else
            {
                Debug.Log($"Devolviendo el valor sin clonar de {fieldName}");
                return field.GetValue(from);
            }
        }

        public override string ToString()
        {
            string playerDataString = "[";
            foreach ((Vector3 vector, Quaternion angle, Character character) in this.PlayersData)
            {
                playerDataString += $"(({vector.x}, {vector.y}, {vector.z}), ({angle.x}, {angle.y}, {angle.z}. {angle.w}), {character.ToString()}),";
            }
            playerDataString = playerDataString.TrimEnd(',') + "]";
            return $"\n(Vector3, Quaternion, Character)[] PlayersData = {playerDataString}";
        }
    }
}
