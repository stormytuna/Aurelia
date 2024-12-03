using UnityEngine;

public class ContactDamage : MonoBehaviour
{
	[SerializeField] private int _damage;
	[SerializeField] private bool _killSelfOnContact;

	void OnTriggerEnter2D(Collider2D other) {
		if (!other.gameObject.TryGetComponent(out Health health)) {
			return;
		}

		health.DealDamage(_damage);
		if (_killSelfOnContact) {
			Destroy(gameObject);
		}
	}
}
