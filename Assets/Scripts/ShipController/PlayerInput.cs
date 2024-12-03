using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IInputProvider
{
	private InputAction _moveAction;
	private InputAction _boostAction;
	private InputAction _shootAction;

	private void Awake() {
		_moveAction = InputSystem.actions.FindAction("Move");
		_boostAction = InputSystem.actions.FindAction("Boost");
		_shootAction = InputSystem.actions.FindAction("Shoot");
	}

	public Vector2 GetMovementInput() {
		return _moveAction.ReadValue<Vector2>();
	}

	public float GetDesiredAngle(float currentAngle) {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return transform.AngleTo(mousePosition);
	}

	public bool GetBoostInput() {
		return _boostAction.IsPressed();
	}

	public bool GetShootInput() {
		return _shootAction.IsPressed();
	}


}
