using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Entity : MonoBehaviour {
	public static float bufferTime = 0.0001f;
	protected GameObject obj;
	protected SpriteRenderer mRenderer;
	protected Collider2D mCollider;
	protected SpriteOutline outline;
	protected Entity parent;
	protected float width;
	protected float height;
	protected int health;
	protected Vector3 relativePosition;
	protected bool playerOwned = false;
	public bool wantsFocus = false;
	public static float GetZPosition(string name) {
		switch (name) {
			case "Background":
				return 1;
			case "Shields":
				return 0;
			case "Ships":
				return -1;
			case "Rooms":
				return -2;
			case "Rooms.1":
				return -2.1f;
			case "Weapons":
				return -3;
			case "Crew":
				return -4;
			case "Red Bar":
				return -5;
			case "Green Bar":
				return -6;
			case "Projectiles":
				return -7;
			case "Aim Games":
				return -8;
			case "Aim Games.1":
				return -8.1f;
			case "Aim Games.2":
				return -8.2f;
			case "Aim Games.3":
				return -8.3f;
			case "Textbox":
				return -9;
			case "Text":
				return -10;

			default:
				Debug.Log("ERROR: object layer not found: " + name);
				return 1;
		}
	}

	public Entity Init(string spritepath, Vector2 size, Vector2 location, string layer) {
		return Init(spritepath, size, location, layer, 1);
	}
	public Entity Init(string spritepath, Vector2 size, Vector2 location, string layer, int health) {
		return Init(spritepath, size, location, layer, health, null);
	}

	public Entity Init(string spritepath, Vector2 size, Vector2 location, string layer, int health, Entity parent) {
		return Init(spritepath, size, location, layer, health, parent, true);
	}

	public Entity Init(string spritepath, Vector2 size, Vector2 location, string layer, int health, Entity parent, bool wantsCollider) {
		obj = new GameObject(this.GetType().Name);
		if (DebugToggler.entityCreated)
			Debug.Log("New " + this.GetType().Name + " created");
		mRenderer = obj.AddComponent<SpriteRenderer>();
		if (spritepath != "")
			mRenderer.sprite = Resources.Load<Sprite>(spritepath);
		else {
			mRenderer.sprite = Resources.Load<Sprite>(SpritePath.grayBar); //can't have sprite be empty, bc other functions refer to sprite
			SetOpacity(0); //make it invisible instead
		}
		width = size.x;
		height = size.y;
		Resize(width, height);
		if (parent != null) {
			SetParent(parent);
			SetLocation(location.x, location.y, GetZPosition(layer) - parent.GetAbsolutePosition().z);
			//subtract parent's  absolute z-position to achieve correct z-position at GetZPosition(layer)
		} else {
			SetLocation(location.x, location.y, GetZPosition(layer));
		}
		this.health = health;
		if (wantsCollider) {
			mCollider = obj.AddComponent<PolygonCollider2D>();
			mCollider.isTrigger = true; //is unnecessary??
		}
		obj.AddComponent<EntityProxy>().Init(this);
		outline = obj.AddComponent<SpriteOutline>();
		mRenderer.material = Resources.Load<Material>("Sprites/SpritesOutline");
		mRenderer.material.SetColor(0, new Color(mRenderer.material.color.r, mRenderer.material.color.g, mRenderer.material.color.b, 1));
		outline.color = Color.blue;
		outline.outlineSize = 10;
		outline.enabled = false;
		mRenderer.enabled = false;
		obj.AddComponent<Rigidbody2D>().gravityScale = 0;
		relativePosition = obj.transform.position - (parent != null ? parent.GetAbsolutePosition() : Vector3.zero);
		StartCoroutine(EnableRenderer());
		return this;
	}

	public void ChangeSprite(string spritepath) {
		mRenderer.sprite = Resources.Load<Sprite>(spritepath);
		Resize(width, height);
		SetOpacity(1);
	}

	IEnumerator EnableRenderer() {
		yield return new WaitForSeconds(Entity.bufferTime);
		mRenderer.enabled = true;
	}

	public void EntityUpdate() {
		relativePosition = obj.transform.position - (parent != null ? parent.GetAbsolutePosition() : Vector3.zero);
	}
	public Vector3 GetParentScale() {
		Vector3 result = new Vector3(1, 1, 1);
		Transform t = obj.transform.parent;
		while (t != null) {
			result = new Vector3(result.x * t.localScale.x, result.y * t.localScale.y, result.z * t.localScale.z);
			t = t.parent;
		}
		return result;
	}
	public void Resize(float x, float y) {
		Vector3 parentScale = GetParentScale();
		float xScale = x / mRenderer.sprite.bounds.size.x / parentScale.x;
		float yScale = y / mRenderer.sprite.bounds.size.y / parentScale.y;
		obj.transform.localScale = new Vector3(xScale, yScale, 1 / parentScale.z);
	}

	public void SetOpacity(float a) {
		mRenderer.color = new Color(1, 1, 1, a);
	}

	public void Move(Vector2 direction) {
		SetLocation(new Vector2(relativePosition.x + direction.x, relativePosition.y + direction.y));
		relativePosition = obj.transform.position - (parent != null ? parent.GetAbsolutePosition() : Vector3.zero);
	}

	public void Move(float x, float y) {
		Move(new Vector2(x, y));
	}

	public GameObject GetObject() {
		return obj;
	}

	public void SetLocation(float x, float y) {
		SetLocation(x, y, relativePosition.z);
	}

	public void SetLocation(float x, float y, float z) {
		obj.transform.position = new Vector3(x, y, z) + (parent != null ? parent.GetAbsolutePosition() : Vector3.zero);
	}

	public void SetLocation(Vector2 position) {
		SetLocation(position.x, position.y, relativePosition.z);
	}

	public Vector3 GetAbsolutePosition() {
		return obj.transform.position;
	}

	public Vector3 GetRelativePosition() {
		return relativePosition;
	}

	public void SetParent(Entity entity) {
		this.obj.transform.parent = entity.GetObject().transform;
		this.parent = entity;
	}

	public Entity GetParent() {
		return parent;
	}

	public void SetOutline(bool set) {
		if (mRenderer.material != Resources.Load<Material>("Sprites/SpritesOutline"))
			mRenderer.material = Resources.Load<Material>("Sprites/SpritesOutline");
		outline.enabled = set;
	}

	public virtual void SetGrayScale(bool set) {
		if (set) {
			if (mRenderer.material != Resources.Load<Material>("Sprites/GrayScale"))
				mRenderer.material = Resources.Load<Material>("Sprites/GrayScale");
		} else {
			if (mRenderer.material != Resources.Load<Material>("Sprites/SpritesOutline"))
				mRenderer.material = Resources.Load<Material>("Sprites/SpritesOutline");
		}
	}

	public float GetWidth() {
		return width;
	}

	public bool PlayerOwned() {
		return playerOwned;
	}

	public void SetPlayerOwned(bool input) {
		playerOwned = input;
		Entity[] children = obj.GetComponentsInChildren<Entity>();
		foreach (Entity child in children) {
			child.SetPlayerOwned(input);
		}

	}
	public void SetWantsFocus(bool input) {
		wantsFocus = input;
	}

	public float GetHeight() {
		return height;
	}

	public virtual void GrayScale() {
		mRenderer.material = Resources.Load<Material>("Sprites/GrayScale");
		Entity[] children = obj.GetComponentsInChildren<Entity>();
		foreach (Entity child in children) {
			child.GrayScale();
		}
	}

	public virtual void EndGrayScale() {
		if (mRenderer == null)
			return;
		mRenderer.material = Resources.Load<Material>("Sprites/SpritesOutline");
		Entity[] children = obj.GetComponentsInChildren<Entity>();
		foreach (Entity child in children) {
			child.EndGrayScale();
		}
	}

	public virtual void OnFocusLost(Entity entity) {
	}

	public virtual void OnFocusGained(Entity entity) {
	}

	public virtual void TakeDamage(int damage) {
		Debug.Log("Entity.TakeDamage method called. This shouldn't happen.");
	}

	public int GetHealth() {
		return health;
	}
	public void OnCollisionEnter2D(Collision2D collision) {
		//check to see if necessary 
	}

	public void Die() {
		Destroy(this);
		Destroy(obj);
	}

	override
	public string ToString() {
		return this.GetType().Name + " at position: " + this.relativePosition;
	}
}

class EntityProxy : MonoBehaviour {
	protected Entity entity;
	public EntityProxy Init(Entity newEntity) {
		entity = newEntity;
		return this;
	}

	public Entity GetEntity() {
		return entity;
	}

}
