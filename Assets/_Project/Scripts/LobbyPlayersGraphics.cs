using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyPlayersGraphics : MonoBehaviour
{
    public Transform[] playersPositions;

    public PlayerCharacter[] AvatarConfiguration;

    List<GameObject> InstantiatedGameObjects;



    void Awake()
    {
        InstantiatedGameObjects = new List<GameObject>();
    }
    public void UpdatePlayer(NetworkList<LobbyPlayer> players)
    {
        HidePlayers();
        for (int i = 0; i < players.Count; i++)
        {
            if (CheckGameObjectSeat(players[i].SeatIndex))
            {

                foreach (GameObject obj in InstantiatedGameObjects)
                {

                    if (obj.GetComponent<PlayerIDHolder>().index == players[i].SeatIndex)
                    {
                        obj.SetActive(true);
                        obj.transform.position = playersPositions[i].position;
                    }

                }

            }
            else
            {
                GameObject newobj = Instantiate(AvatarConfiguration[players[i].SeatIndex].AvatarLobbyGraphics, playersPositions[i].position, transform.rotation);
                InstantiatedGameObjects.Add(newobj);
                newobj.GetComponent<PlayerIDHolder>().index = players[i].SeatIndex;
            }
            // foreach (GameObject newobj in InstantiatedGameObjects)
            // {
            //     if (player.PlayerID == newobj.GetComponent<PlayerIDHolder>().PlayerID)
            //     {
            //         newobj.SetActive(true);
            //     }
            // }

        }
    }

    public bool CheckGameObjectSeat(int index)
    {
        foreach (GameObject obj in InstantiatedGameObjects)
        {

            if (obj.GetComponent<PlayerIDHolder>().index == index)
            {
                return true;
            }

        }
        return false;

    }
    public void HidePlayers()
    {
        foreach (GameObject newobj in InstantiatedGameObjects)
        {

            newobj.SetActive(false);

        }

    }

}
