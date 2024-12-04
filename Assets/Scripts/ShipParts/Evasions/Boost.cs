using System;
using DataStructures;
using UnityEngine;
using UnityEngine.Events;

namespace ShipParts.Evasions
{
	public class Boost : ShipEvasion
	{
		[SerializeField] private float _boostThrustMultiplier = 1f;
		[SerializeField] public float MaxBoostTime = 0f;
		[SerializeField] private float _boostRechargeDelay = 0f;
		[SerializeField] private float _boostRechargeRate = 1f;
		
		private float _currentBoostTime;
		private float _currentBoostRechargeDelay;
		
		public UnityEvent<float> OnBoostChanged = new();

		private void Start() {
			_currentBoostTime = MaxBoostTime;	
		}

		private void FixedUpdate() {
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

		public override ShipData UpdateShipData(ShipData shipData, ShipInputData inputData) {
			if (!inputData.EvasionInput || _currentBoostTime <= 0f) {
				return shipData;
			}
			
			shipData.Thrust *= _boostThrustMultiplier;

			_currentBoostTime -= Time.fixedDeltaTime;	
			_currentBoostRechargeDelay = _boostRechargeDelay;
			OnBoostChanged.Invoke(_currentBoostTime);
			
			return shipData;
		}
	}
}
