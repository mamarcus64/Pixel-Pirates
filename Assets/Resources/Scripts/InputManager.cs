using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static Entity focus;
    void Start()
    {

    }

    void Update()
    {
        if (!ShipFightManager.paused)
        {
            RaycastHit2D[] hits;
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = Camera.main.transform.position.z;
            hits = Physics2D.RaycastAll(point, Vector3.forward, 100);
            if (hits.Length != 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    InputManager.GiveFocus(hits[0].collider.gameObject.GetComponent<EntityProxy>().GetEntity());
                    if(DebugToggler.inputClick)
                        Debug.Log(hits[0].collider.gameObject.GetComponent<EntityProxy>().GetEntity().GetType().Name + " pressed at position: "
                            + hits[0].collider.gameObject.transform.position);
                }
            }
            else if(Input.GetMouseButton(0))
            {
                GiveFocus(null);
            }
        }
    }

    public static void GiveFocus(Entity entity)
    {
        if (focus != null)
        {
            focus.OnFocusClick(entity);
            focus.SetOutline(false);
            focus = null;
        }
        if (focus == null && entity != null && entity.PlayerOwned() && entity.wantsFocus)
        {
            focus = entity;
            focus.SetOutline(true);
        }
    }
}
