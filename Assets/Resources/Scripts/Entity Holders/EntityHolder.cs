using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHolder<E> : MonoBehaviour where E : Entity {
    GameObject holder;

    public EntityHolder<E> Init() {
        holder = new GameObject("Holder");
        return this;
    }

    public F AddComponent<F>() where F : E {

        return holder.AddComponent<F>();
    }

    public void DestroyChildren(Entity parent) {
        foreach (Component c in holder.GetComponents<Entity>())
            if ((c as Entity).GetParent() == parent)
                (c as Entity).Die();
    }

    public void SetPlayerOwned(Entity parent) {
        foreach (Component c in holder.GetComponents<Entity>())
            if ((c as Entity).GetParent() == parent)
                (c as Entity).SetPlayerOwned(true);
    }
}
