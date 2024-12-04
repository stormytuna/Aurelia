using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Helpers
{
	public static class Helpers
	{
		public static float AngleTo(this Vector3 from, Vector3 to) {
			Vector3 heading = to - from;
			return Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg - 90f;
		}

		public static float AngleTo(this Transform from, Vector3 to) {
			Vector3 heading = to - from.position;
			return Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg - 90f;
		}

		public static Vector3 DirectionTo(this Vector3 from, Vector3 to) {
			return (to - from).normalized;
		}

		public static Vector3 DirectionTo(this Transform from, Vector3 to) {
			return (to - from.position).normalized;
		}

		public static float Distance(this Vector3 from, Vector3 to) {
			return Vector3.Distance(from, to);
		}

		public static void MoveToPoint(this Rigidbody2D rigidbody, Transform transform, Vector3 point, float speed) {
			Vector3 toPoint = transform.DirectionTo(point);
			rigidbody.AddForce(toPoint * speed);
		}

		public static void LookAtPoint(this Transform transform, Vector3 point, float rotationSpeed) {
			float targetAngle = transform.AngleTo(point);
			Quaternion targetQuaternion = Quaternion.AngleAxis(targetAngle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, rotationSpeed);
		}

		public static List<GameObject> FindChildrenWithTag(this GameObject parent, string tag) {
			List<GameObject> children = new List<GameObject>();

			foreach (Transform child in parent.transform) {
				if (child.CompareTag(tag)) {
					children.Add(child.gameObject);
				}
			}

			return children;
		}

		public static List<GameObject> RecursivelyFindChildrenWithTag(this GameObject parent, string tag) {
			List<GameObject> children = new List<GameObject>();

			foreach (Transform child in parent.transform) {
				if (child.CompareTag(tag)) {
					children.Add(child.gameObject);
				}

				if (child.childCount > 0) {
					children.AddRange(child.gameObject.RecursivelyFindChildrenWithTag(tag));
				}
			}

			return children;
		}

		public static List<T> RecursivelyFindChildrenWithComponent<T>(this GameObject parent) {
			List<T> children = new();

			foreach (Transform child in parent.transform) {
				if (child.gameObject.TryGetComponent(out T component)) {
					children.Add(component);
				}

				if (child.childCount > 0) {
					children.AddRange(child.gameObject.RecursivelyFindChildrenWithComponent<T>());
				}
			}

			return children;
		}
	
		public static T FindChildWithComponent<T>(this GameObject parent) where T : Component {
			return FindChildWithComponent<T>(parent.transform);
		}

		public static T FindChildWithComponent<T>(this Transform parent) where T : Component {
			foreach (Transform child in parent) {
				if (child.gameObject.TryGetComponent(out T component)) {
					return component;
				}
			}

			return null;
		}

		public static T RecursivelyFindChildWithComponent<T>(this GameObject parent) where T : Component {
			return RecursivelyFindChildWithComponent<T>(parent.transform);
		}
	
		public static T RecursivelyFindChildWithComponent<T>(this Transform parent) where T : Component {
			foreach (Transform child in parent) {
				if (child.gameObject.TryGetComponent(out T component)) {
					return component;
				}

				if (child.childCount <= 0) {
					continue;
				}

				var childComponent = child.gameObject.RecursivelyFindChildWithComponent<T>();
				if (childComponent != null) {
					return childComponent;
				}
			}

			return null;
		}
		public static Vector2 RandomPointOnScreen() {
			var screenPosition = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
			return Camera.main.ScreenToWorldPoint(screenPosition);
		}
	}
}
