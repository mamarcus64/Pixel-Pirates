using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallRoom : Room
{
    public override void OnFocusClick(Entity entity)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        spritePath = "Sprites/Misc/demo room";
        RoomStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShipFightManager.paused)
            RoomUpdate();
    }


}
