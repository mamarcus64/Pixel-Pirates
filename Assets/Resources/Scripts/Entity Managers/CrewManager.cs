using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : EntityManager<CrewMember> {
	public CrewManager(Entity owner) : base(owner, 8) {

	}
}
