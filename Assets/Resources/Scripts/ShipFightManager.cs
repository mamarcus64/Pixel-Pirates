using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFightManager : MonoBehaviour
{
    static Ship playerShip;
    static Ship enemyShip;
    public static bool paused;
    void Start()
    {
        paused = false;
        playerShip = gameObject.AddComponent<BasicShip>().Init(new Vector2(0, 2));

        enemyShip = gameObject.AddComponent<BasicShip>().Init(new Vector2(0, -2.5f));
        //playerShip.SetPlayerOwned(true);
        StartCoroutine(Load());
    }

    public static IEnumerator Pause(float time)
    {
        paused = true;
        yield return new WaitForSeconds(time);
        paused = false;
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(Entity.bufferTime);
        //playerShip.SetLocation(0, 2);
        playerShip.SetPlayerOwned(true);
        //enemyShip.SetLocation(0, -2.5f);
    }

    void Update()
    {
        
    }

    public static List<Room> GetEnemyRooms()
    {
        return enemyShip.getRooms();
    }

    public static List<Room> GetPlayerRooms()
    {
        return playerShip.getRooms();
    }

    public static void GrayScale()
    {
        playerShip.GrayScale();
        enemyShip.GrayScale();
    }

    public static void EndGrayScale()
    {
        playerShip.EndGrayScale();
        enemyShip.EndGrayScale();
    }

}
