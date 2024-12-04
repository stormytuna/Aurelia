using Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipController
{
	public class PlayerInput : MonoBehaviour, IInputProvider
	{
		private InputAction _moveAction;
		private InputAction _evasionAction;
		private InputAction _primaryShootAction;

		private void Awake() {
			_moveAction = InputSystem.actions.FindAction("Move");
			_evasionAction = InputSystem.actions.FindAction("Evasion");
			_primaryShootAction = InputSystem.actions.FindAction("Primary");
		}

		public Vector2 GetMovementInput() {
			return _moveAction.ReadValue<Vector2>();
		}

		public float GetDesiredAngle(float currentAngle) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return transform.AngleTo(mousePosition);
		}

		public bool GetBoostInput() {
			return _evasionAction.IsPressed();
		}

		public bool GetShootInput() {
			return _primaryShootAction.IsPressed();
		}


	}
}
