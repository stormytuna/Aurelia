using DataStructures;
using ShipController;
using UnityEngine;

namespace ShipParts
{
	public class ShipFrame : ShipPart
	{
		[SerializeField] private float _mass = 1;
		[SerializeField] private int _health = 5;

		public override void Equip(ref ShipData shipData, ShipPartsManager shipPartsManager) {
			shipData.Mass += _mass;
			shipData.Health.BaseValue = _health;
		}
	}
}
