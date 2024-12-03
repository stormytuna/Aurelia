using UnityEngine;

public class StateBasedInputManager : MonoBehaviour, IInputProvider
{
	[SerializeField] private StateBasedInputProvider _currentState;

	private void FixedUpdate() {
		if (_currentState.TryGetNextState(out StateBasedInputProvider nextState)) {
			_currentState = nextState;
		}
	}

	public Vector2 GetMovementInput() {
		return _currentState.GetMovementInput();
	}

	public float GetDesiredAngle(float currentAngle) {
		return _currentState.GetDesiredAngle(currentAngle);
	}

	public bool GetBoostInput() {
		return _currentState.GetBoostInput();
	}

	public bool GetShootInput() {
		return _currentState.GetShootInput();
	}
}
