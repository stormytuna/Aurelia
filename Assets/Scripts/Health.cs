using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	[SerializeField] private int _maxHealth;
	[SerializeField] private int _health;

	public UnityEvent<int, int> OnDamaged = new();

	void Awake() {
		_health = _maxHealth;
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
