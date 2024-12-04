using DataStructures;
using UnityEngine;

namespace ShipParts
{
	public class ShipThrusters : MonoBehaviour, IShipPart
	{
		[SerializeField] private float _thrust = 10f;
		[SerializeField] private float _rotationSpeed = 0.2f;
		
		public ShipData InitialiseShipData(ShipData shipData) {
			shipData.Thrust += _thrust;
			shipData.RotationSpeed += _rotationSpeed;
			return shipData;
		}

		public ShipData UpdateShipData(ShipData shipData, ShipInputData inputData) => shipData;
	}
}
