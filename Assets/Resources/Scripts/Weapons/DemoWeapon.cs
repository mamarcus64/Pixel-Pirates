using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWeapon : Weapon
{
    public DemoWeapon Init(Vector2 location, Entity parent)
    {
        Init("Sprites/Weapons/demo cannon", new Vector2(1.5f, 1), location, 2, parent);
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
            obj.AddComponent<DemoCannonball>().Init(new Vector2(obj.transform.position.x, obj.transform.position.y), clickedEntity,
                obj.transform.parent.GetComponent<EntityProxy>().GetEntity() as Ship);
            //using absolute position, not localPosition for projectile location b/c ball is not tied to a parent ship
        }
    }
}
