using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Entity : MonoBehaviour
{
    public static float bufferTime = 0.0001f;
    public static float epsilon = 0.01f;
    protected GameObject obj;
    protected SpriteRenderer mRenderer;
    protected Collider2D mCollider;
    protected string spritePath;
    protected string layer;
    protected SpriteOutline outline;
    protected float width;
    protected float height;
    protected int health;
    protected Vector3 localPosition;
    protected bool playerOwned = false;
    public bool wantsFocus = false;
    public static float GetZPosition(string name)
    {
        switch(name)
        {
            case "Shields": return 0;
            case "Ships": return -1;
            case "Rooms": return -2;
            case "Weapons": return -3;
            case "Crew": return -4;
            case "Red Bar": return -5;
            case "Green Bar": return -6;
            case "Projectiles": return -7;
            case "Aim Games": return -8;
            default: Debug.Log("ERROR: object layer not found: " + name); return 1;
        }
    }

    public void EntityStart()
    {
        if(obj != null)
            localPosition = obj.transform.localPosition;
        obj = new GameObject(this.GetType().Name);
        if (DebugToggler.entityCreated)
            Debug.Log("New " + this.GetType().Name + " created");
        mRenderer = obj.AddComponent<SpriteRenderer>();
        mRenderer.sprite = Resources.Load<Sprite>(spritePath);
        Resize(width, height);
        mCollider = obj.AddComponent<PolygonCollider2D>();
        mCollider.isTrigger = true; //is unnecessary??
        obj.AddComponent<EntityProxy>().ChangeEntity(this);
        outline = obj.AddComponent<SpriteOutline>();
        mRenderer.material = Resources.Load<Material>("Sprites/SpritesOutline");
        mRenderer.material.SetColor(0, new Color(mRenderer.material.color.r, mRenderer.material.color.g, mRenderer.material.color.b, 1));
        outline.color = Color.blue;
        outline.outlineSize = 10;
        outline.enabled = false;
        mRenderer.enabled = false;
        obj.AddComponent<Rigidbody2D>().gravityScale = 0;
        StartCoroutine(SetZ());
        StartCoroutine(EnableRenderer());
    }

    IEnumerator EnableRenderer()
    {
        yield return new WaitForSeconds(Entity.bufferTime * 2);
        mRenderer.enabled = true;
    }
    IEnumerator SetZ()
    {
        yield return new WaitForSeconds(Entity.bufferTime);
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, GetZPosition(layer));
    }

    public void EntityUpdate()
    {
        if (obj != null) {

            localPosition = obj.transform.localPosition;
            Vector3 parentScale = this.GetParentScale();
            localPosition = new Vector3(localPosition.x * parentScale.x, localPosition.y * parentScale.y, localPosition.z * parentScale.z);
        }
    }

    public void Resize(float x, float y)
    {
        float xScale = x / mRenderer.sprite.bounds.size.x;
        float yScale = y / mRenderer.sprite.bounds.size.y;
        obj.transform.localScale = new Vector3(xScale, yScale, 1);
    }

    public static void Resize(float x, float y, GameObject obje)
    {
        Vector3 parentScale = GetParentScale(obje);
        float xScale = x / obje.GetComponent<SpriteRenderer>().sprite.bounds.size.x 
            / parentScale.x;
        float yScale = y / obje.GetComponent<SpriteRenderer>().sprite.bounds.size.y
            / parentScale.y;
        obje.transform.localScale = new Vector3(xScale, yScale, 1 / parentScale.z);
    }

    public static Vector3 GetParentScale(GameObject obj)
    {
        Vector3 result = new Vector3(1, 1, 1);
        Transform t = obj.transform.parent;
        while(t != null)
        {
            result = new Vector3(result.x * t.localScale.x, result.y * t.localScale.y, result.z * t.localScale.z);
            t = t.parent;
        }
        return result;
    }

    public Vector3 GetParentScale()
    {
        Vector3 result = new Vector3(1, 1, 1);
        Transform t = obj.transform.parent;
        while (t != null)
        {
            result = new Vector3(result.x * t.localScale.x, result.y * t.localScale.y, result.z * t.localScale.z);
            t = t.parent;
        }
        return result;
       
    }

    public GameObject GetObject()
    {
        return obj;
    }

    public void SetLocation(float x, float y)
    {
        Vector3 parentScale = GetParentScale();
        obj.transform.localPosition = new Vector3(x / parentScale.x, y / parentScale.y, localPosition.z / parentScale.z);
    }

    public void SetLocation(float x, float y, float z, GameObject obj)
    {
        Vector3 parentScale = GetParentScale(obj);
        obj.transform.localPosition = new Vector3(x / parentScale.x, y / parentScale.y, z / parentScale.z);
    }

    public void SetLocation(float x, float y, float z)
    {
        Vector3 parentScale = GetParentScale();
        obj.transform.localPosition = new Vector3(x / parentScale.x, y / parentScale.y, z / parentScale.z);
    }

    public void SetLocation(Vector2 position)
    {
        Vector3 parentScale = GetParentScale();
        obj.transform.localPosition = new Vector3(position.x / parentScale.x, position.y / parentScale.y, this.localPosition.z / parentScale.z);
    }

    public void SetLocation(Vector3 position)
    {
        Vector3 parentScale = GetParentScale();
        obj.transform.localPosition = new Vector3(position.x / parentScale.x, position.y / parentScale.y, position.z / parentScale.z);
    }

    public Vector3 GetAbsolutePosition()
    {
        return obj.transform.position;
    }

    public Vector2 GetLocalPosition()
    {
        return new Vector2(localPosition.x, localPosition.y);
    }

    public void SetParent(Entity entity)
    {
        this.obj.transform.parent = entity.GetObject().transform;
    }

    public void SetOutline(bool set)
    {
        if(mRenderer.material != Resources.Load<Material>("Sprites/SpritesOutline"))
            mRenderer.material = Resources.Load<Material>("Sprites/SpritesOutline");
        outline.enabled = set;
    }

    public virtual void SetGrayScale(bool set)
    {
        if (set) {
        if (mRenderer.material != Resources.Load<Material>("Sprites/GrayScale"))
            mRenderer.material = Resources.Load<Material>("Sprites/GrayScale");
         }
        else
        {
            if (mRenderer.material != Resources.Load<Material>("Sprites/SpritesOutline"))
                mRenderer.material = Resources.Load<Material>("Sprites/SpritesOutline");
        }
    }

    public float GetWidth()
    {
        return width;
    }

    public bool PlayerOwned()
    {
        return playerOwned;
    }

    public void SetPlayerOwned(bool input)
    {
        playerOwned = input;
        Entity[] children = obj.GetComponentsInChildren<Entity>();
        foreach (Entity child in children)
        {
            child.SetPlayerOwned(input);
        }

    }
    public void SetWantsFocus(bool input)
    {
        wantsFocus = input;
    }

    public float GetHeight()
    {
        return height;
    }

    public virtual void GrayScale()
    {
        mRenderer.material = Resources.Load<Material>("Sprites/GrayScale");
        Entity[] children = obj.GetComponentsInChildren<Entity>();
        foreach (Entity child in children)
        {
            child.GrayScale();
        }
    }

    public virtual void EndGrayScale()
    {
        if (mRenderer == null)
            return;
        mRenderer.material = Resources.Load<Material>("Sprites/SpritesOutline");
        Entity[] children = obj.GetComponentsInChildren<Entity>();
        foreach (Entity child in children)
        {
            child.EndGrayScale();
        }
    }

    public virtual void OnFocusClick(Entity entity)
    {

    }
    public virtual void TakeDamage(int damage)
    {
        Debug.Log("Entity.TakeDamage method called. This shouldn't happen.");
    }

    public int GetHealth()
    {
        return health;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void Die()
    {
        Destroy(obj);
        Destroy(this);
    }

    override
    public string ToString()
    {
        return this.GetType().Name + " at position: " + this.localPosition;
    }
}

class EntityProxy : MonoBehaviour
{
    protected Entity entity;
    public void ChangeEntity(Entity newEntity)
    {
        entity = newEntity;
    }

    public Entity GetEntity()
    {
        return entity;
    }

}
