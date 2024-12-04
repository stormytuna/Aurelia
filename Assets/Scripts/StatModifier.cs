using UnityEngine.Events;

namespace DefaultNamespace
{
	public class StatModifier
	{
		public readonly float Value;
		public readonly StatModifierType Type;
		public readonly int Order;
		public readonly object Source;
		public bool IsEnabled = true;

		public readonly UnityEvent<bool> OnEnabledStateChanged = new();

		public StatModifier(float value, StatModifierType type, int order, object source) {
			Value = value;
			Type = type;
			Order = order;
			Source = source;
		}

		public StatModifier(float value, StatModifierType type) : this(value, type, 0, null) { }
		public StatModifier(float value, StatModifierType type, int order) : this(value, type, order, null) { }
		public StatModifier(float value, StatModifierType type, object source) : this(value, type, 0, source) { }

		public void Enable() {
			if (!IsEnabled) {
				OnEnabledStateChanged.Invoke(true);
			}

			IsEnabled = true;
		}

		public void Disable() {
			if (IsEnabled) {
				OnEnabledStateChanged.Invoke(false);
			}

			IsEnabled = false;
		}
	}

	public enum StatModifierType
	{
		Flat = 100,
		Additive = 200,
		Multiplicative = 300,
	}
}
