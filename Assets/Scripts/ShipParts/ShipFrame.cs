using DataStructures;
using UnityEngine;

namespace ShipParts
{
	public class ShipFrame : MonoBehaviour, IShipPart
	{
		[SerializeField] private float _mass = 1;
		[SerializeField] private int _health = 5;

		public ShipData InitialiseShipData(ShipData shipData) {
			shipData.Mass += _mass;
			shipData.Health += _health;
			return shipData;
		}
		
		public ShipData UpdateShipData(ShipData shipData, ShipInputData inputData) => shipData;
	}
}
