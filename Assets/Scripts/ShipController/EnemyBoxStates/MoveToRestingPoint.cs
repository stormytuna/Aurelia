using UnityEngine;
using Random = UnityEngine.Random;

public class MoveToRestingPoint : StateBasedInputProvider
{
	[SerializeField] private StateBasedInputProvider _spinAndShootState;
	[SerializeField] private float _restingPointRadius;

	private Vector2 _restingPoint;

	private void Awake() {
		var restingPointOffset = Random.insideUnitCircle * _restingPointRadius;
		var restingPointScreen = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f) + restingPointOffset;
		_restingPoint = Camera.main.ScreenToWorldPoint(restingPointScreen);
	}


	public override bool TryGetNextState(out StateBasedInputProvider nextState) {
		if (transform.position.Distance(_restingPoint) < 0.1f) {
			nextState = _spinAndShootState;
			return true;
		}

		nextState = null;
		return false;
	}

	public override Vector2 GetMovementInput() {
		return transform.DirectionTo(_restingPoint);
	}

	public override float GetDesiredAngle(float currentAngle) {
		return currentAngle + 179;
	}

	public override bool GetBoostInput() {
		return false;
	}

	public override bool GetShootInput() {
		return true;
	}
}
