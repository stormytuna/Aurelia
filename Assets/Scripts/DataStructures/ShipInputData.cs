using UnityEngine;

namespace DataStructures
{
	public record ShipInputData
	{
		public Vector2 MovementInput;
		public float DesiredAngle;
		public bool EvasionInput;
		public bool PrimaryInput;
	}
}
