using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDisplay : Entity
{
    Ship ship;
    public ShipDisplay Init(Vector2 size, Vector2 pos, Ship ship) {
        base.Init(SpritePath.woodTextbox, size, pos, "UIDisplay");
        this.ship = ship;
        PopulateDisplay();
        return this;
    }

    void PopulateDisplay() {
        float currentY = height / 2.25f;
        GameManager.MakeText("Weapons", new Vector3(0, currentY, 0) + GetAbsolutePosition(), new Vector2(1.5f, 1), TextAnchor.MiddleCenter);
        List<string> haha = new List<string>();
        haha.Add("W");
        haha.Add("D");
        haha.Add("E");
        haha.Add("A");
        float deltaY = 0.75f;
        foreach (Weapon weapon in ship.GetWeapons()) {
            currentY -= deltaY;
            obj.AddComponent<FocusButton>().Init(new Vector2(1, 1), new Vector3(-width / 2.75f, currentY, 0), weapon, haha[0], this);
            haha.RemoveAt(0);
            obj.AddComponent<Icon>().Init(weapon.GetSpritepath(), new Vector2(0.75f, 0.75f), new Vector2(width / 2.6f, currentY), "UIDisplay.2", this);
            GameManager.MakeText(weapon.GetDisplayName(), new Vector3(0, currentY, 0) + GetAbsolutePosition(), new Vector2(1.5f, 1), TextAnchor.MiddleCenter, 9);

        }

        currentY -= deltaY * 2;
        GameManager.MakeText("Crew", new Vector3(0, currentY, 0) + GetAbsolutePosition(), new Vector2(1.5f, 1), TextAnchor.MiddleCenter);
        foreach (CrewMember crew in ship.GetCrewMembers()) {
            currentY -= deltaY;
            obj.AddComponent<FocusButton>().Init(new Vector2(1, 1), new Vector3(-width / 2.75f, currentY, 0), crew, haha[0], this);
            haha.RemoveAt(0);
            obj.AddComponent<Icon>().Init(crew.GetSpritepath(), new Vector2(0.75f, 0.75f), new Vector2(width / 2.6f, currentY), "UIDisplay.2", this);
            GameManager.MakeText(crew.GetDisplayName(), new Vector3(0, currentY, 0) + GetAbsolutePosition(), new Vector2(1.5f, 1), TextAnchor.MiddleCenter, 9);

        }

    }

    void Update() {
        if (!GameManager.paused) {
            EntityUpdate();
        }
    }
    public override string GetDisplayName() {
        return "Ship Display";
    }
}
