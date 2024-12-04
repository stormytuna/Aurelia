using DataStructures;
using Helpers;
using ShipParts;
using UnityEngine;
using UnityEngine.Events;

namespace ShipController
{
	[RequireComponent(typeof(Rigidbody2D), typeof(IInputProvider))]
	public class ShipManager : MonoBehaviour
	{
		private Rigidbody2D _rigidbody;
		private IInputProvider _inputProvider;
		private ShipFrame _shipFrame;
		private ShipThrusters _shipThrusters;
		private ShipWeapon _shipWeapon;
		private ShipEvasion _shipEvasion;
		private ShipData _shipData;
		private ShipData _shipDataBase;

		private float _currentShootDelay;

		public UnityEvent<ShipData> OnShipDataInitialised = new();

		private void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
			_inputProvider = GetComponent<IInputProvider>();
		
			_shipFrame = gameObject.transform.Find("ShipPartsContainer").FindChildWithComponent<ShipFrame>();
			_shipThrusters = gameObject.transform.Find("ShipPartsContainer").FindChildWithComponent<ShipThrusters>();
			_shipWeapon = gameObject.transform.Find("ShipPartsContainer").FindChildWithComponent<ShipWeapon>();
			_shipEvasion = gameObject.transform.Find("ShipPartsContainer").FindChildWithComponent<ShipEvasion>();
		}

		private void Start() {
			ShipData shipData = new();
		
			shipData = _shipFrame.InitialiseShipData(shipData);
			shipData = _shipThrusters.InitialiseShipData(shipData);
			shipData = _shipWeapon.InitialiseShipData(shipData);
			shipData = _shipEvasion.InitialiseShipData(shipData);
		
			_shipData = _shipDataBase = shipData;
			_rigidbody.mass = _shipData.Mass;
			OnShipDataInitialised.Invoke(_shipData);	
		}

		private void FixedUpdate() {
			UpdateShipData();
			DoLinearMovement();
			DoAngularMovement();
			DoShooting();
		}

		private void UpdateShipData() {
			ShipInputData shipInputData = new ShipInputData {
				MovementInput = _inputProvider.GetMovementInput(),
				DesiredAngle = _inputProvider.GetDesiredAngle(_rigidbody.rotation),
				EvasionInput = _inputProvider.GetBoostInput(),
				PrimaryInput = _inputProvider.GetShootInput(),
			};
		
			var shipData = _shipDataBase;
			shipData = _shipFrame.UpdateShipData(shipData, shipInputData);
			shipData = _shipThrusters.UpdateShipData(shipData, shipInputData);
			shipData = _shipWeapon.UpdateShipData(shipData, shipInputData);
			shipData = _shipEvasion.UpdateShipData(shipData, shipInputData);
		
			_shipData = shipData;
		}

		private void DoLinearMovement() {
			Vector2 movementDirection = _inputProvider.GetMovementInput().normalized;
			float thrust = _shipData.Thrust;
			_rigidbody.AddForce(movementDirection * thrust);
		}

		private void DoAngularMovement() {
			float desiredAngle = _inputProvider.GetDesiredAngle(_rigidbody.rotation);
			_rigidbody.MoveRotation(Quaternion.Slerp(Quaternion.Euler(0f, 0f, _rigidbody.rotation), Quaternion.Euler(0f, 0f, desiredAngle), _shipData.RotationSpeed));
		}

		private void DoShooting() {
			_currentShootDelay -= Time.fixedDeltaTime;

			if (!_inputProvider.GetShootInput() || _currentShootDelay > 0f) {
				return;
			}

			_currentShootDelay = _shipData.MaxShootDelay;

			// TODO: When expanding ShipWeapon class, pass responsibility onto that for spawning bullets
			//   Our ShipManager will just tell it when that happens
			foreach (var shootingPoint in _shipData.ShootPoints) {
				GameObject bullet = Instantiate(_shipData.BulletPrefab, shootingPoint.position, shootingPoint.rotation);
				bullet.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * _shipData.ShootSpeed, ForceMode2D.Impulse);
				bullet.layer = _shipData.ShootLayer;
			}
		}
	}
}
