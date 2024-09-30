using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static GameManager;


public class RoundManager : AbstractManager
{
    public static RoundManager Instance;

    public double HidingTime;
    public GameObject Players;
    public GameObject Scene;
    public GameObject playerPrefab;
    private List<Player> playerList = new List<Player>();
    private List<Room> roomList = new List<Room>();

    private double RemainingTime;
    private bool IsTimeOver;


    private void Awake()
    {
        Instance = this;

        if (GameManager.Instance == null)
        {
            GameManager.LoadGameManager();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RemainingTime = HidingTime;
        IsTimeOver = false;
        roomList = Scene.GetComponentsInChildren<Room>().ToList();

        RoundManagerData load = GameManager.Instance.LoadData<RoundManagerData>();
        this.SetSceneData(load);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTimeOver)
        {
            RemainingTime -= Time.deltaTime;
            IsTimeOver = RemainingTime <= 0;
        } else
        {
            EndOfRound();
            // Temporal: Resetea la ronda
            RemainingTime = HidingTime;
            IsTimeOver = false;
        }
        //Debug.Log(GameManager.Instance);
    }

    void EndOfRound()
    {
        foreach (Player player in playerList)
        {
            if (!player.IsHiding())
            {
                Debug.Log(player);
            }
            else
            {
                int securityPercentage = player.GetCurrentRoom().SecurityPercentage;
                int randInt = (int)(UnityEngine.Random.value * 100f);
                bool dead = securityPercentage < randInt;
                Debug.Log("Security = " + securityPercentage);
                Debug.Log("Random = " + randInt);
                if (dead) {
                    Debug.Log(player + " ):");
                }
                else
                {
                    Debug.Log(player + " (:");
                }
            }
        }
    }

    public bool SeAcaboElTiempoDeEscondite()
    {
        return IsTimeOver;
    }

    public void OpenPuzzleScene(SceneAsset scene)
    {
        GameManager.Instance.SaveData(this.GetSceneData());
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }

    /* Methodos heredados */

    public override SceneData GetSceneData()
    {
        RoundManagerData data = new RoundManagerData();
        data.PlayerAmount = this.playerList.Count;

        data.PlayersData = new (Vector3, Quaternion, Character)[this.playerList.Count];
        for (int i = 0; i < this.playerList.Count; i++)
        {
            Player player = this.playerList[i];
            data.PlayersData[i] = (player.transform.position, player.transform.rotation, player.character);
        }

        List<string> solved = new List<string>();
        foreach (Room room in this.roomList)
        {
            if (room.puzzle != null && room.puzzle.isSolved)
            {
                solved.Add(room.puzzle.getName());
            }
        }
        data.SolvedPuzzles = solved.ToArray();

        return data;
    }

    public override void SetSceneData(SceneData sceneData)
    {
        const string playerObjectName = "Player";
        RoundManagerData data = (RoundManagerData) sceneData;
        for (int i = 0; i < data.PlayerAmount; i++)
        {
            (Vector3 position, Quaternion rotation, Character character) = data.PlayersData[i];
            GameObject playerObject = Instantiate(playerPrefab, position, rotation, this.Players.transform);
            playerObject.name = playerObjectName + i;
            Player player = playerObject.GetComponent<Player>();
            player.character = character;
            playerList.Add(player);
        }

        foreach(Room room in this.roomList)
        {
            if (room.puzzle != null)
            {
                string name = room.puzzle.getName();
                if (data.SolvedPuzzles.Contains(name))
                {
                    room.puzzle.isSolved = true;
                }
            }
        }
    }

    public override Type GetSceneDataType()
    {
        return typeof(RoundManagerData);
    }

    /* Scene Data class */

    public class RoundManagerData : SceneData
    {
        public int PlayerAmount;
        public (Vector3, Quaternion, Character)[] PlayersData;
        public string[] SolvedPuzzles;

        public override object Clone()
        {
            RoundManager.RoundManagerData clone = new RoundManager.RoundManagerData();
            clone.PlayerAmount = PlayerAmount;
            clone.PlayersData = new (Vector3, Quaternion, Character)[PlayerAmount];
            for (int i = 0; i < PlayersData.Length; i++)
            {
                clone.PlayersData[i] = PlayersData[i];
            }

            return clone;
        }
    }
}
