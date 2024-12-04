using System.Collections;
using Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyControllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class EnemyKamikazeController : MonoBehaviour
	{
		private Rigidbody2D _rigidbody;

		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _rotationSpeed;

		void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		void Start() {
			StartCoroutine(KamikazePlayer());
		}

		private IEnumerator KamikazePlayer() {
		start:

			yield return new WaitForFixedUpdate();

			Vector3 playerPosition = Singleton<GameManager>.instance.Player.transform.position;
			_rigidbody.MoveToPoint(transform, playerPosition, _moveSpeed);
			transform.LookAtPoint(playerPosition, _rotationSpeed);

			goto start;
		}

		// TODO: Need a more robust state machine thingy that can listen to events and switch state depending on current state
		private void OnCollisionEnter2D(Collision2D other) {
			/*
		if (!other.gameObject.TryGetComponent(out PlayerController player)) {
			return;
		}

		player.GetComponent<Health>().DealDamage(1);
		Destroy(gameObject);
		*/
		}
	}
}
