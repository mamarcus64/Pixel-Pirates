using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    public override void Play(Ship playerShip, Ship enemyShip)
    {
        if (playerShip != null)
            foreach (Weapon weapon in enemyShip.GetWeapons())
                if (weapon.WeaponLoaded()) {

                    weapon.Fire(playerShip.GetRooms()[Random.Range(0, playerShip.GetRooms().Count)], 1);
                }
    }
}
