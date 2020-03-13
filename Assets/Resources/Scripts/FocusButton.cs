using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusButton : Entity
{
    Entity focus;
    KeyCode keyCode;
    public FocusButton Init(Vector2 size, Vector2 pos, Entity focus, string key) {
        return Init(size, pos, focus, key, null);     
    }

    public FocusButton Init(Vector2 size, Vector2 pos, Entity focus, string key, Entity parent) {
        base.Init(SpritePath.redButton, size, pos, "UIDisplay.1", 1, parent);
        this.focus = focus;
        keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key.ToUpper());
        playerOwned = true;
        wantsFocus = true;
        return this;
    }

    private void Update() {
        if (Input.GetKeyDown(keyCode))
            InputManager.GiveFocus(this);
    }

    public override void OnFocusGained() {
        Debug.Log("Giving focus: " + focus);
        InputManager.GiveFocus(focus);
    }

    public override void OnFocusLost(Entity entity) {
        focus.OnFocusLost(entity);
    }
    public override string GetDisplayName() {
        return "Focus Button";
    }
}
