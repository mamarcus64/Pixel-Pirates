using System.Collections;
using UnityEngine;

public abstract class Weapon : Entity
{
    protected float cooldown;
    protected float cooldownTimer = 0;
    protected Icon greenBar;
    protected Icon redBar;
    protected Entity clickedEntity;
    public static float cooldownWidth = 0.625f;
    public static float cooldownHeight = 0.5f;

    public Weapon Init(string spritePath, Vector2 size, Vector2 location, int health, Entity parent)
    {
        base.Init(spritePath, size, location, "Weapons", health, parent);
        wantsFocus = true;
        Vector2 barLoc = new Vector2(0, 1); //relative to parent, which in for green/red bar's case is Weapon
        greenBar = obj.AddComponent<Icon>().Init("Sprites/Misc/green bar", 
            new Vector2(1f, cooldownHeight), barLoc, "Green Bar", this);
        redBar = obj.AddComponent<Icon>().Init("Sprites/Misc/red bar", 
            new Vector2(cooldownWidth, cooldownHeight), barLoc, "Red Bar", this);
        return this;
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
        greenBar.Resize(cooldownWidth * (cooldownTimer / cooldown), cooldownHeight);
        greenBar.SetLocation(redBar.GetLocalPosition().x - cooldownWidth / 2
         + (cooldownWidth * (cooldownTimer / cooldown) / 2), redBar.GetLocalPosition().y);
        //redBar.SetLocation
        //greenBar.transform.position = new Vector3( greenBar.transform.position.z);
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            SetGrayScale(true);
    }


    public abstract void AimGameResults(float result);
}
