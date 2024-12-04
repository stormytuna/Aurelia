using UnityEngine;

namespace ShipController
{
	public interface IInputProvider
	{
		Vector2 GetMovementInput();
		float GetDesiredAngle(float currentAngle);
		bool GetBoostInput();
		bool GetShootInput();
	}
}
