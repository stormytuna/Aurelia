using UnityEngine;

namespace ShipController.EnemyBoxStates
{
	public class ShootAndSpin : StateBasedInputProvider
	{
		public override bool TryGetNextState(out StateBasedInputProvider nextState) {
			nextState = null;
			return false;
		}

		public override Vector2 GetMovementInput() {
			return Vector2.zero;
		}

		public override float GetDesiredAngle(float currentAngle) {
			return currentAngle + 179;
		}

		public override bool GetBoostInput() {
			return false;
		}

		public override bool GetShootInput() {
			return true;
		}
	}
}
