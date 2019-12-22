using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWeapon : Weapon
{
    public DemoWeapon Init(Vector2 location, Entity parent)
    {
        Init(SpritePath.demoWeapon, new Vector2(1.5f, 1), location, 2, parent);
        cooldown = 2f;
        return this;
    }

    void Update()
    {

        if (!ShipFightManager.paused)
            WeaponUpdate();
       
    }

    public override void OnFocusClick(Entity entity)
    {
        if (entity != null && entity.GetType().IsSubclassOf(typeof(Room)) && ShipFightManager.GetEnemyShip().GetRooms().Contains(entity as Room) && WeaponLoaded())
        {
            if (cooldownTimer >= cooldown)
                cooldownTimer = 0;
            ShipFightManager.paused = true;
            obj.AddComponent<SideSweep>().Init(this, entity);
        } 
    }

    public override void Fire(Entity target, float result)
    {
        ShipFightManager.paused = false;
        cooldownTimer = 0;
        if (result == 1)
        {
            obj.AddComponent<DemoCannonball>().Init(new Vector2(obj.transform.position.x, obj.transform.position.y), target,
                parent as Ship);
            //using absolute position, not relative position for projectile location b/c ball is not tied to a parent ship
        }
    }
}
