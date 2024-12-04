using ShipParts;
using UnityEngine;

namespace DataStructures
{
	public struct ShipData
	{
		// Frame
		public float Mass;
		public int Health;
		
		// Thrusters
		public float Thrust;
		public float RotationSpeed;
		
		// Weapons
		// Not currently, but will eventually need to manage its own state like Evasion below
		public float ShootSpeed;
		public float MaxShootDelay;
		public int ShootLayer;
		public GameObject BulletPrefab;
		public Transform[] ShootPoints;
		
		// Evasion
		// Currently complex enough to need its own class for handling state
		public ShipEvasion ShipEvasion;
	}
}
