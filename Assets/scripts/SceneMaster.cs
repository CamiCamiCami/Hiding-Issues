using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMaster : MonoBehaviour
{

    int playerAmount = 1;
    List<Vector3> playerPositions = new List<Vector3>();
    List<Quaternion> playerRotations = new List<Quaternion>();
    List<Character?> playerCharacters = new List<Character?>();
    public void setUpGameManager(GameManager manager)
    {
        manager.PreparePlayers(playerAmount);
        for (int i = 0; i < manager.PlayerList.Count; i++)
        {
            Vector3 setPosition = playerPositions.Count > i && playerPositions[i] != null ? playerPositions[i] : manager.PlayerList[i].transform.position;
            Quaternion setRotation = playerRotations.Count > i && playerRotations[i] != null ? playerRotations[i] : manager.PlayerList[i].transform.rotation;
            manager.PlayerList[i].transform.SetLocalPositionAndRotation(setPosition, setRotation);
            manager.PlayerList[i].character = playerCharacters[i].HasValue ? playerCharacters[i].Value : manager.PlayerList[i].character;
        }
    }

    public void saveGameManager(GameManager manager)
    {
        playerPositions.Clear();
        playerRotations.Clear();
        playerCharacters.Clear();
        for (int i = 0; i < manager.PlayerList.Count; i++)
        {
            playerPositions.Add(manager.PlayerList[i].transform.position);
            playerRotations.Add(manager.PlayerList[i].transform.rotation);
            playerCharacters.Add(manager.PlayerList[i].character);
        }
    }


    



}
