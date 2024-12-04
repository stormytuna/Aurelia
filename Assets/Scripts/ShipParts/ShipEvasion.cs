using DataStructures;
using UnityEngine;

namespace ShipParts
{
	public abstract class ShipEvasion : MonoBehaviour, IShipPart
	{
		public ShipData InitialiseShipData(ShipData shipData) {
			shipData.ShipEvasion = this;
			return shipData;
		}

		public abstract ShipData UpdateShipData(ShipData shipData, ShipInputData inputData);
	}
}
