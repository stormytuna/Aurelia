using DataStructures;
using ShipParts;
using UnityEngine;
using UnityEngine.Events;

namespace ShipController
{
	public class ShipPartsManager : MonoBehaviour
	{
		public ShipData ShipData;
		public UnityEvent<ShipData> OnShipDataInitialised = new();

		private ShipPart[] _shipParts;

		private void Awake() {
			_shipParts = transform.Find("ShipPartsContainer").GetComponentsInChildren<ShipPart>();
		}

		private void Start() {
			ShipData = new ShipData();
			foreach (var shipPart in _shipParts) {
				shipPart.Equip(ref ShipData, this);
			}

			OnShipDataInitialised.Invoke(ShipData);
		}
	}
}
