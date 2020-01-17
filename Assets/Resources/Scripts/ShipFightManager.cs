using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFightManager : MonoBehaviour {
	static Ship playerShip;
	static Ship enemyShip;
    public static WeaponHolder weaponHolder;
    public static CrewHolder crewHolder;

	public static bool paused;
	public static bool userPaused;
	private static Icon pausedIcon;

    static List<CrewMember> SpawnCrewRealQuick() {
        List<CrewMember> list = new List<CrewMember>();
        list.Add(crewHolder.AddComponent<BasicCrew>().Init(null, null));
        list.Add(crewHolder.AddComponent<BasicCrew>().Init(null, null));
        return list;
    }

    static List<Weapon> SpawnWeaponsRealQuick() {
        List<Weapon> weapons = new List<Weapon>();
        weapons.Add(weaponHolder.AddComponent<CannonMkI>().Init(Vector2.zero, null));
        weapons.Add(weaponHolder.AddComponent<CannonMkII>().Init(Vector2.zero, null));
        return weapons;
    }
	void Start() {
		paused = false;
        weaponHolder = gameObject.AddComponent<WeaponHolder>().Init() as WeaponHolder;
        crewHolder = gameObject.AddComponent<CrewHolder>().Init() as CrewHolder;
        playerShip = gameObject.AddComponent<BasicShip>().Init(new Vector2(0, 2), SpawnWeaponsRealQuick(), SpawnCrewRealQuick(), new User()); ;
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -50);
		enemyShip = gameObject.AddComponent<BasicShip>().Init(new Vector2(0, -2.5f), SpawnWeaponsRealQuick(), SpawnCrewRealQuick(), new Enemy());
		playerShip.SetPlayerOwned(true);
		//StartCoroutine(Load());

		pausedIcon = gameObject.AddComponent<Icon>().Init(SpritePath.paused, new Vector2(4, 1), new Vector2(0, -3.5f), "Textbox", null, false);
		pausedIcon.setVisible(false);
	}

	public static IEnumerator Pause(float time) {
		paused = true;
		yield return new WaitForSeconds(time);
		paused = false;
	}

	void Update() {
		if (enemyShip == null) {
			Debug.Log("do the mario swing your arms from side to side");
		}

		pausedIcon.setVisible(userPaused);
	}

	public static Ship GetEnemyShip() {
		return enemyShip;
	}

	public static Ship GetPlayerShip() {
		return playerShip;
	}

	public static void GrayScale() {
		playerShip.GrayScale();
		enemyShip.GrayScale();
	}

	public static void EndGrayScale() {
		playerShip.EndGrayScale();
		enemyShip.EndGrayScale();
	}

}
