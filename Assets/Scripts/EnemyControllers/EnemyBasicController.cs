using System.Collections;
using Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyControllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class EnemyBasic : MonoBehaviour
	{
		private Rigidbody2D _rigidbody;
		private GameObject _bulletPrefab;

		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private float _shootSpeed;
		[SerializeField] private float _maxShootDelay;

		private float _shootDelay;

		void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
			_bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
		}

		void Start() {
			StartCoroutine(Behaviour());
		}

		private IEnumerator Behaviour() {
		start:

			yield return new WaitForFixedUpdate();

			Vector3 playerPosition = Singleton<GameManager>.instance.Player.transform.position;
			_rigidbody.MoveToPoint(transform, playerPosition, _moveSpeed);
			transform.LookAtPoint(playerPosition, _rotationSpeed);

			HandleShoot();

			goto start;
		}

		private void HandleShoot() {
			_shootDelay -= Time.fixedDeltaTime;
			if (_shootDelay > 0f) {
				return;
			}

			_shootDelay = _maxShootDelay;

			GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
			Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
			Vector2 toPlayer = Helpers.Helpers.DirectionTo(transform.position, Singleton<GameManager>.instance.Player.transform.position);
			rigidbody.AddForce(toPlayer * _shootSpeed, ForceMode2D.Impulse);
			bullet.layer = LayerMask.NameToLayer("EnemyAttack");
		}
	}
}
