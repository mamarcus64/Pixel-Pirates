using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimGame : MonoBehaviour
{
    protected GameObject parent;
    protected Weapon weapon;

    public void SetWeapon(Weapon input)
    {
        weapon = input;
    }

    public void AimGameStart()
    {
        parent = new GameObject("Aim Game");
        Vector3 weaponPosition = weapon.GetAbsolutePosition();
        parent.transform.position = new Vector3(weaponPosition.x, weaponPosition.y, Entity.GetZPosition("Aim Games"));
    }

    public void Finish(float result)
    {
        weapon.AimGameResults(result);
        Destroy(parent);
        Destroy(this);
    }

    public static void Resize(float x, float y, GameObject obje)
    {
        float xScale = x / obje.GetComponent<SpriteRenderer>().sprite.bounds.size.x / obje.transform.localScale.x;
        float yScale = y / obje.GetComponent<SpriteRenderer>().sprite.bounds.size.y / obje.transform.localScale.y;
        obje.transform.localScale = new Vector3(xScale, yScale, 1);
    }
}
