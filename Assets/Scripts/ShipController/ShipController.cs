using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(IInputProvider))]
public class ShipController : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	private IInputProvider _inputProvider;

	[Header("Movement")]
	[SerializeField] private float _thrust = 10f;
	[SerializeField] private float _rotationSpeed = 0.1f;

	[Header("Boost")]
	[SerializeField] private float _boostThrustMultiplier = 1f;
	[SerializeField] public float MaxBoostTime = 0f;
	[SerializeField] private float _boostRechargeDelay = 0f;
	[SerializeField] private float _boostRechargeRate = 1f;

	[Header("Shooting")]
	[SerializeField] private float _shootSpeed = 10f;
	[SerializeField] private float _maxShootDelay;
	[SerializeField, Layer] private int _shootLayer;
	[SerializeField] private GameObject _bulletPrefab;

	private float _currentBoostTime;
	private float _currentBoostRechargeDelay;
	private float _currentShootDelay;
	private List<Transform> _shootingPoints;

	public UnityEvent<float> OnBoostChanged = new();

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody2D>();
		_inputProvider = GetComponent<IInputProvider>();
		_shootingPoints = gameObject.RecursivelyFindChildrenWithTag("ShootPoint").Select(child => child.transform).ToList();
	}

	private void FixedUpdate() {
		RegainBoost();
		DoLinearMovement();
		DoAngularMovement();
		DoShooting();
	}

	private void RegainBoost() {
		_currentBoostRechargeDelay -= Time.fixedDeltaTime;

		if (MaxBoostTime <= 0f || _currentBoostTime >= MaxBoostTime || _currentBoostRechargeDelay > 0f) {
			return;
		}

		_currentBoostTime += Time.fixedDeltaTime * _boostRechargeRate;
		if (_currentBoostTime >= MaxBoostTime) {
			_currentBoostTime = MaxBoostTime;
		}

		OnBoostChanged.Invoke(_currentBoostTime);
	}

	private void DoLinearMovement() {
		Vector2 movementDirection = _inputProvider.GetMovementInput().normalized;
		float thrust = _thrust;

		if (_inputProvider.GetBoostInput() && _currentBoostTime > 0f) {
			thrust *= _boostThrustMultiplier;
			_currentBoostTime -= Time.fixedDeltaTime;
			_currentBoostRechargeDelay = _boostRechargeDelay;
			OnBoostChanged.Invoke(_currentBoostTime);
		}

		_rigidbody.AddForce(movementDirection * thrust);
	}

	private void DoAngularMovement() {
		float desiredAngle = _inputProvider.GetDesiredAngle(_rigidbody.rotation);
		_rigidbody.MoveRotation(Quaternion.Slerp(Quaternion.Euler(0f, 0f, _rigidbody.rotation), Quaternion.Euler(0f, 0f, desiredAngle), _rotationSpeed));
	}

	private void DoShooting() {
		_currentShootDelay -= Time.fixedDeltaTime;

		if (!_inputProvider.GetShootInput() || _currentShootDelay > 0f) {
			return;
		}

		_currentShootDelay = _maxShootDelay;

		foreach (var shootingPoint in _shootingPoints) {
			GameObject bullet = Instantiate(_bulletPrefab, shootingPoint.position, shootingPoint.rotation);
			bullet.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * _shootSpeed, ForceMode2D.Impulse);
			bullet.layer = _shootLayer;
		}

	}
}
