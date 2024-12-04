using UnityEngine;

namespace ShipController
{
	public abstract class StateBasedInputProvider : MonoBehaviour, IInputProvider
	{
		public abstract bool TryGetNextState(out StateBasedInputProvider nextState);
		public abstract Vector2 GetMovementInput();
		public abstract float GetDesiredAngle(float currentAngle);
		public abstract bool GetBoostInput();
		public abstract bool GetShootInput();
	}
}
