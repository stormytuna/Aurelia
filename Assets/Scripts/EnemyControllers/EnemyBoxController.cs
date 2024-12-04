using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

namespace EnemyControllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class EnemyBoxController : MonoBehaviour
	{
		private Rigidbody2D _rigidbody;
		private GameObject _bulletPrefab;
		private List<Transform> _shootPoints;

		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private float _shootSpeed;
		[SerializeField] private float _maxShootDelay;

		private Vector2 _restingPosition;
		private float _shootDelay;

		void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
			_bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
			_shootPoints = gameObject.RecursivelyFindChildrenWithTag("ShootPoint").Select(child => child.transform).ToList();
		}

		void Start() {
			var restingPositionScreen = (Random.insideUnitCircle * 200f) + new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
			_restingPosition = Camera.main.ScreenToWorldPoint(restingPositionScreen);

			StartCoroutine(MoveToRestingPosition());
		}

		private IEnumerator MoveToRestingPosition() {
			start:

			yield return new WaitForFixedUpdate();

			_rigidbody.MoveToPoint(transform, _restingPosition, _moveSpeed);
			transform.LookAtPoint(_restingPosition, _rotationSpeed);

			if (transform.position.Distance(_restingPosition) < 0.1f) {
				StartCoroutine(ShootAndSpin());
				yield break;
			}

			goto start;
		}

		private IEnumerator ShootAndSpin() {
			start:

			yield return new WaitForFixedUpdate();

			transform.rotation *= Quaternion.Euler(0f, 0f, _rotationSpeed);

			HandleShoot();

			goto start;
		}

		private void HandleShoot() {
			_shootDelay -= Time.fixedDeltaTime;
			if (_shootDelay > 0f) {
				return;
			}

			_shootDelay = _maxShootDelay;
			foreach (var shootPoint in _shootPoints) {
				GameObject bullet = Instantiate(_bulletPrefab, shootPoint.position, shootPoint.rotation);
				Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
				rigidbody.AddForce(bullet.transform.up * _shootSpeed, ForceMode2D.Impulse);
				bullet.layer = LayerMask.NameToLayer("EnemyAttack");
			}
		}
	}
}
