using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWeapon : Weapon
{
    void Start()
    {
        health = 2;
        width = 1.5f;
        height = 1;
        spritePath = "Sprites/Weapons/demo cannon";
        WeaponStart();
        cooldown = 2f;
    }

    void Update()
    {

        if (!ShipFightManager.paused)
            WeaponUpdate();
       
    }

    public override void OnFocusClick(Entity entity)
    {
        clickedEntity = entity;
        if (entity != null && entity.GetType().IsSubclassOf(typeof(Room)) && ShipFightManager.GetEnemyRooms().Contains(entity as Room))
        {
            if (cooldownTimer >= cooldown)
                cooldownTimer = 0;
            ShipFightManager.paused = true;
            ShipFightManager.GrayScale();
            obj.AddComponent<SideSweep>().SetWeapon(this);

        } 
    }

    public override void AimGameResults(float result)
    {
        ShipFightManager.paused = false;
        ShipFightManager.EndGrayScale();
        if (result == 1)
        {
            DemoCannonball ball = obj.AddComponent<DemoCannonball>();
            StartCoroutine(LoadProjectile(ball, clickedEntity));
        }
    }
}
