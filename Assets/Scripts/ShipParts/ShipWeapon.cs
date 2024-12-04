using System.Linq;
using DataStructures;
using Helpers;
using Meta;
using Unity.VisualScripting;
using UnityEngine;

namespace ShipParts
{
	public class ShipWeapon : MonoBehaviour, IShipPart
	{
		[SerializeField] private float _shootSpeed = 0.1f;
		[SerializeField] private float _maxShootDelay = 2f;
		[SerializeField, Layer] private int _shootLayer;
		[SerializeField] private GameObject _bulletPrefab;
		
		public ShipData InitialiseShipData(ShipData shipData) {
			shipData.ShootSpeed = _shootSpeed;
			shipData.MaxShootDelay = _maxShootDelay;
			shipData.ShootLayer = _shootLayer;
			shipData.BulletPrefab = _bulletPrefab;
			shipData.ShootPoints = gameObject.FindChildrenWithTag("ShootPoint").Select(child => child.transform).ToArray();
			return shipData;
		}
		
		public ShipData UpdateShipData(ShipData shipData, ShipInputData inputData) => shipData;
	}
}
