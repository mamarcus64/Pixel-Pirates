using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : Entity
{
   

    public Icon Init(string pathName, Vector2 size, Vector2 localPosition, string layer, Entity parent)
    {
        base.Init(pathName, size, localPosition, layer, 0, parent);
        return this;
    }
    void Update()
    {
        if (!ShipFightManager.paused)
        {
            EntityUpdate();
        }
    }
}
