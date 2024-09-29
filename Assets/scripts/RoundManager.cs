using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameManager;


public class RoundManager : AbstractManager
{

    public double HidingTime;
    public GameObject Players;
    public GameObject playerPrefab;
    public List<Player> PlayerList { get; private set; } = new List<Player>();
    private double RemainingTime;
    private bool IsTimeOver;


    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.LoadGameManager();
            for (int i = 0; i < 10000000 && GameManager.Instance == null; i++) {
                Debug.Log(i);
            }
        }
        //RoundManagerData load = GameManager.Instance.LoadData<RoundManagerData>();
        //this.SetSceneData(load);
    }

    // Start is called before the first frame update
    void Start()
    {
        RemainingTime = HidingTime;
        IsTimeOver = false;
        PlayerList = Players.GetComponentsInChildren<Player>().ToList();
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
        Debug.Log(GameManager.Instance);
    }

    void EndOfRound()
    {
        foreach (Player player in PlayerList)
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

    bool SeAcaboElTiempoDeEscondite()
    {
        return IsTimeOver;
    }

    /* Methodos heredados */

    public override SceneData GetSceneData()
    {
        RoundManagerData data = new RoundManagerData();
        data.playerAmount = this.PlayerList.Count;
        data.playersData = new (Vector3, Quaternion, Character)[this.PlayerList.Count];
        for (int i = 0; i < this.PlayerList.Count; i++)
        {
            Player player = this.PlayerList[i];
            data.playersData[i] = (player.transform.position, player.transform.rotation, player.character);
        }

        return data;
    }

    public override void SetSceneData(SceneData sceneData)
    {
        RoundManagerData data = (RoundManagerData) sceneData;
        
        for (int i = 0; i < data.playerAmount; i++)
        {
            (Vector3 position, Quaternion rotation, Character character) = data.playersData[i];
            GameObject playerObject = Instantiate(playerPrefab);
            playerObject.transform.SetParent(this.Players.transform);
            playerObject.transform.SetPositionAndRotation(position, rotation);
            Player player = playerObject.GetComponent<Player>();
            player.character = character;
            PlayerList.Add(player);
        }
    }

    public override Type GetSceneDataType()
    {
        return typeof(RoundManagerData);
    }

    /* Scene Data class */

    public class RoundManagerData : SceneData
    {
        public int playerAmount;
        public (Vector3, Quaternion, Character)[] playersData;

        public override object Clone()
        {
            RoundManager.RoundManagerData clone = new RoundManager.RoundManagerData();
            clone.playerAmount = playerAmount;
            clone.playersData = new (Vector3, Quaternion, Character)[playerAmount];
            for (int i = 0; i < playersData.Length; i++)
            {
                clone.playersData[i] = playersData[i];
            }

            return clone;
        }
    }
}
