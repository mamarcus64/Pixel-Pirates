using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallRoom : Room
{
    public override void OnFocusLost(Entity entity)
    {

    }

    // Start is called before the first frame update
    public TallRoom Init(Vector2 location, Entity parent)
    {
        base.Init(SpritePath.demoRoom, location, parent);
        return this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShipFightManager.paused)
            RoomUpdate();
    }


}
