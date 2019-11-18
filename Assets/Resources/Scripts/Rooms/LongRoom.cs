using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRoom : Room
{
    public override void OnFocusClick(Entity entity)
    {

    }

    // Start is called before the first frame update
    public LongRoom Init(Vector2 location, Entity parent)
    {
        base.Init("Sprites/Misc/demo room", location, parent);
        return this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShipFightManager.paused)
            RoomUpdate();
    }


}
