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
        greenBar = obj.AddComponent<Icon>();
        greenBar.Init(this, "Sprites/Misc/green bar", new Vector2(1f, cooldownHeight), new Vector2(), Entity.GetZPosition("Green Bar"));
        redBar = obj.AddComponent<Icon>();
        redBar.Init(this, "Sprites/Misc/red bar", new Vector2(cooldownWidth, cooldownHeight), new Vector2(0, 1.5f), Entity.GetZPosition("Red Bar"));
        greenBar.SetLocation(redBar.localPosition.x, redBar.localPosition.y);
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
        //redBar.SetLocation(redBar.localPosition.x, redBar.localPosition.y);
        //Debug.Log(greenBar.localPosition);
        //greenBar.Resize(cooldownWidth * (cooldownTimer / cooldown), cooldownHeight);
        greenBar.SetLocation(redBar.transform.localPosition.x - cooldownWidth / 2
         + (cooldownWidth * (cooldownTimer / cooldown) / 2), redBar.localPosition.y);
        greenBar.SetLocation(-100, -100);
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
