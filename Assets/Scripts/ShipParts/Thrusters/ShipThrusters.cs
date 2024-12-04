using DataStructures;
using ShipController;
using UnityEngine;

namespace ShipParts
{
	public class ShipThrusters : ShipPart
	{
		[SerializeField] private float _thrust = 10f;
		[SerializeField] private float _rotationSpeed = 0.2f;

		private ShipPartsManager _shipPartsManager;

		public override void Equip(ref ShipData shipData, ShipPartsManager shipPartsManager) {
			shipData.Thrust.BaseValue = _thrust;
			shipData.RotationSpeed.BaseValue = _rotationSpeed;
			_shipPartsManager = shipPartsManager;
		}

		public void FixedUpdate() {
			Vector2 movementDirection = ShipInputProvider.GetMovementInput().normalized;
			float thrust = _shipPartsManager.ShipData.Thrust.Value;
			ShipRigidbody.AddForce(movementDirection * thrust);

			float desiredAngle = ShipInputProvider.GetDesiredAngle(ShipRigidbody.rotation);
			ShipRigidbody.MoveRotation(Quaternion.Slerp(Quaternion.Euler(0f, 0f, ShipRigidbody.rotation), Quaternion.Euler(0f, 0f, desiredAngle), _shipPartsManager.ShipData.RotationSpeed.Value));
		}
	}
}
