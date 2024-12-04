using UnityEngine;

public class ContactDamage : MonoBehaviour
{
	[SerializeField] private int _damage;
	[SerializeField] private bool _killSelfOnContact;

	void OnTriggerEnter2D(Collider2D other) {
		if (!other.gameObject.TryGetComponent(out Health health)) {
			return;
		}

		// TODO: Make ShipStat have a "current value", then modify that
		health.DealDamage(_damage);
		if (_killSelfOnContact) {
			Destroy(gameObject);
		}
	}
}
