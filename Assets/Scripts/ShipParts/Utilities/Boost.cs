using DataStructures;
using DefaultNamespace;
using ShipController;
using UnityEngine;
using UnityEngine.Events;

namespace ShipParts.Utilities
{
	public class Boost : ShipPart
	{
		[SerializeField] private float _boostThrustMultiplier = 1f;
		[SerializeField] public float MaxBoostTime = 0f;
		[SerializeField] private float _boostRechargeDelay = 0f;
		[SerializeField] private float _boostRechargeRate = 1f;

		private StatModifier _thrustModifier;
		private float _currentBoostTime;
		private float _currentBoostRechargeDelay;

		public UnityEvent<float> OnBoostChanged = new();

		private void Start() {
			_currentBoostTime = MaxBoostTime;
		}

		public override void Equip(ref ShipData shipData, ShipPartsManager shipPartsManager) {
			_thrustModifier = new StatModifier(_boostThrustMultiplier, StatModifierType.Additive, this);
			shipData.Thrust.AddModifier(_thrustModifier);

			_thrustModifier.Disable();
		}

		private void FixedUpdate() {
			RegainBoost();
			UseBoost();
		}

		private void RegainBoost() {
			if (MaxBoostTime <= 0f || _currentBoostTime >= MaxBoostTime) {
				return;
			}

			if (_currentBoostRechargeDelay > 0f) {
				_currentBoostRechargeDelay -= Time.fixedDeltaTime;
				return;
			}

			_currentBoostTime += Time.fixedDeltaTime * _boostRechargeRate;
			if (_currentBoostTime >= MaxBoostTime) {
				_currentBoostTime = MaxBoostTime;
			}

			OnBoostChanged.Invoke(_currentBoostTime);
		}

		private void UseBoost() {
			if (!ShipInputProvider.GetBoostInput() || _currentBoostTime <= 0f) {
				_thrustModifier.Disable();
				return;
			}

			_currentBoostTime -= Time.fixedDeltaTime;
			_currentBoostRechargeDelay = _boostRechargeDelay;
			_thrustModifier.Enable();
			OnBoostChanged.Invoke(_currentBoostTime);
		}
	}
}
