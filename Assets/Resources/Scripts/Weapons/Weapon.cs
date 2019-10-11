using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Entity
{
    protected float cooldown;
    protected float cooldownTimer = 0;
    protected GameObject greenBar;
    protected GameObject redBar;
    protected Entity clickedEntity;

    public static float cooldownWidth = 0.625f;
    public static float cooldownHeight = 0.25f;

    public void WeaponStart()
    {
        layer = "Weapons";
        EntityStart();
        wantsFocus = true;
        greenBar = new GameObject();
        redBar = new GameObject();
        greenBar.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/green bar");
        redBar.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/red bar");
        greenBar.transform.parent = obj.transform;
        redBar.transform.parent = obj.transform;
        greenBar.transform.localPosition = new Vector3(greenBar.transform.localPosition.x, greenBar.transform.localPosition.y, 
            Entity.GetZPosition("Green Bar"));
        redBar.transform.localPosition = new Vector3(redBar.transform.localPosition.x, redBar.transform.localPosition.y,
           Entity.GetZPosition("Red Bar"));
        Resize(0.1f, cooldownHeight, greenBar);
        Resize(cooldownWidth, cooldownHeight, redBar);
        redBar.transform.position = new Vector3(localPosition.x, localPosition.y + 0.5f, redBar.transform.localPosition.z);
        greenBar.transform.position = new Vector3(redBar.transform.position.x, redBar.transform.position.y, greenBar.transform.localPosition.z);    
    }

    public void WeaponUpdate()
    {
        EntityUpdate();
        if (cooldownTimer < cooldown)
            cooldownTimer += Time.deltaTime;
        DrawCooldownBar();
        if (GetHealth() <= 0)
            SetGrayScale(true);
        else
            SetGrayScale(false);
    }
    
    public void DrawCooldownBar()
    {
        Resize(cooldownWidth * (cooldownTimer / cooldown), cooldownHeight, greenBar);
        greenBar.transform.position = new Vector3(redBar.transform.position.x - cooldownWidth / 2
         +  (cooldownWidth * (cooldownTimer / cooldown)/2), redBar.transform.position.y, greenBar.transform.position.z);
    }

    public IEnumerator LoadProjectile(Projectile projectile, Entity target)
    {
        yield return new WaitForSeconds(Entity.bufferTime);
        
        projectile.SetLocation(obj.transform.position.x, obj.transform.position.y); //not localPosition b/c not in relation to weapon
        projectile.SetTarget(target);
        projectile.SetShooter(obj.transform.parent.GetComponent<EntityProxy>().GetEntity() as Ship);
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            SetGrayScale(true);
    }


    public abstract void AimGameResults(float result);
}
