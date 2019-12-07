using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimGame : MonoBehaviour
{
    protected Icon grayScreen;
    protected Weapon weapon;
    protected Vector2 weaponPosition;

    public void SetWeapon(Weapon input)
    {
        weapon = input;
    }

    public void AimGameStart()
    {
        weaponPosition = weapon.GetAbsolutePosition();
        grayScreen = weapon.GetObject().AddComponent<Icon>().Init("Sprites/Misc/gray bar", Camera.main.ScreenToWorldPoint
            (new Vector2(Screen.width, Screen.height)),
            new Vector2(-weaponPosition.x, -weaponPosition.y), "Aim Games", weapon);
        Debug.Log("Weapon pos: " + weaponPosition);
        grayScreen.SetOpacity(0.5f);
        
        //grayScreen.transform.position = new Vector3(weaponPosition.x, weaponPosition.y, Entity.GetZPosition("Aim Games"));
    }

    public void Finish(float result)
    {
        weapon.AimGameResults(result);
        grayScreen.Die();
        Destroy(this);
    }

    public static void Resize(float x, float y, GameObject obje)
    {
        float xScale = x / obje.GetComponent<SpriteRenderer>().sprite.bounds.size.x / obje.transform.localScale.x;
        float yScale = y / obje.GetComponent<SpriteRenderer>().sprite.bounds.size.y / obje.transform.localScale.y;
        obje.transform.localScale = new Vector3(xScale, yScale, 1);
    }
}
