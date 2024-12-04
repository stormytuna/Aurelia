using System;
using ShipController;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ShipPartsManager))]
public class Health : MonoBehaviour
{
	[SerializeField] private float _maxHealth;
	[SerializeField] private float _health;

	public UnityEvent<float, float> OnDamaged = new();

	void Awake() {
		ShipPartsManager shipPartsManager = GetComponent<ShipPartsManager>();
		shipPartsManager.OnShipDataInitialised.AddListener((shipData) => {
			_health = _maxHealth = shipData.Health.Value;
		});
	}

	public void DealDamage(int damage) {
		_health -= damage;
		_health = Math.Max(_health, 0);

		OnDamaged.Invoke(_health, damage);

		// TODO: Maybe want to move this somewhere else??
		if (_health <= 0) {
			Destroy(gameObject);
		}
	}

	public void Heal(int health) {
		_health += health;
		_health = Math.Max(_health, _maxHealth);
	}
}
