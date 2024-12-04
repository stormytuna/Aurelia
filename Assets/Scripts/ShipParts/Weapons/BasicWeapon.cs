using System.Linq;
using DataStructures;
using Helpers;
using Meta;
using ShipController;
using UnityEngine;

namespace ShipParts
{
	public class BasicWeapon : ShipPart
	{
		[SerializeField] private float _shootSpeed = 0.1f;
		[SerializeField] private float _maxShootDelay = 2f;
		[SerializeField] private float _damage = 10f;
		[SerializeField, Layer] private int _shootLayer;
		[SerializeField] private GameObject _bulletPrefab;

		private ShipPartsManager _shipPartsManager;
		private Transform[] _shootPoints;
		private float _currentShootDelay;

		public override void Equip(ref ShipData shipData, ShipPartsManager shipPartsManager) {
			shipData.ShootSpeed.BaseValue = _shootSpeed;
			shipData.MaxShootDelay.BaseValue = _maxShootDelay;
			shipData.Damage.BaseValue = _damage;
			_shootPoints = gameObject.FindChildrenWithTag("ShootPoint").Select(child => child.transform).ToArray();
			_shipPartsManager = shipPartsManager;
		}

		private void FixedUpdate() {
			_currentShootDelay -= Time.fixedDeltaTime;

			if (!ShipInputProvider.GetShootInput() || _currentShootDelay > 0f) {
				return;
			}

			_currentShootDelay = _shipPartsManager.ShipData.MaxShootDelay.Value;

			foreach (var shootingPoint in _shootPoints) {
				GameObject bullet = Instantiate(_bulletPrefab, shootingPoint.position, shootingPoint.rotation);
				bullet.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * _shipPartsManager.ShipData.ShootSpeed.Value, ForceMode2D.Impulse);
				bullet.layer = _shootLayer;
			}
		}
	}
}
