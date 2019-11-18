using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    protected GameObject obj;
    protected SpriteRenderer mRenderer;
    protected Entity parent;
    protected float z;
    public Vector3 localPosition;

    public void Init(Entity parent, string pathName, Vector2 size, Vector2 localPosition, int z)
    {
        obj = new GameObject("Icon");
        mRenderer = obj.AddComponent<SpriteRenderer>();
        this.parent = parent;
        this.z = z;
        if(parent != null)
            obj.transform.parent = parent.GetObject().transform;
        mRenderer.sprite = Resources.Load<Sprite>(pathName);
        Resize(size);
        SetLocation(localPosition);
        this.localPosition = obj.transform.localPosition;
    }
    void Update()
    {
        if(obj != null)
            localPosition = obj.transform.localPosition;
    }

    public Vector3 GetParentScale()
    {
        Vector3 result = new Vector3(1, 1, 1);
        Transform t = parent.GetObject().transform;
        while (t != null)
        {
            result = new Vector3(result.x * t.localScale.x, result.y * t.localScale.y, result.z * t.localScale.z);
            t = t.parent;
        }
        return result;
    }

    public void Resize(Vector2 size)
    {
        Resize(size.x, size.y);
    }

    public void Resize(float x, float y)
    {
        if (mRenderer.sprite == null)
            return;
        Vector3 parentScale = GetParentScale();
        float xScale = x / mRenderer.sprite.bounds.size.x / parentScale.x;
        float yScale = y / mRenderer.sprite.bounds.size.y / parentScale.y;
        obj.transform.localScale = new Vector3(xScale, yScale, 1 / parentScale.z);
    }

    public void SetLocation(float x, float y)
    {
        Vector3 parentScale = GetParentScale();
        obj.transform.localPosition = new Vector3(x / parentScale.x, y / parentScale.y, obj.transform.localPosition.z / parentScale.z);
        //Debug.Log(obj.transform.localPosition);
    }

    public void SetLocation(Vector2 loc)
    {
        SetLocation(loc.x, loc.y);
    }
}
