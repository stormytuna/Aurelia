using DataStructures;
using ShipController;
using UnityEngine;

namespace ShipParts
{
	public abstract class ShipPart : MonoBehaviour
	{
		protected Rigidbody2D ShipRigidbody;
		protected IInputProvider ShipInputProvider;

		public virtual void Equip(ref ShipData shipData, ShipPartsManager shipPartsManager) { }

		public virtual void Unequip(ref ShipData shipData) { }

		private void Awake() {
			ShipRigidbody = GetComponentInParent<Rigidbody2D>();
			ShipInputProvider = GetComponentInParent<IInputProvider>();
		}
	}
}
