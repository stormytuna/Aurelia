using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultNamespace
{
	public class ShipStat
	{
		private float _baseValue;
		public float BaseValue {
			get => _baseValue;
			set {
				if (value != _baseValue) {
					_isDirty = true;
				}
				_baseValue = value;
			}
		}

		private float _value;
		public float Value {
			get {
				if (_isDirty) {
					_value = RecalculateValue();
					_isDirty = false;
				}
				return _value;
			}
		}

		private readonly List<StatModifier> _statModifiers = new();
		private bool _isDirty = true;

		public ShipStat(float baseValue) {
			BaseValue = baseValue;
		}

		public ShipStat() : this(0f) { }

		public void AddModifier(StatModifier modifier) {
			_statModifiers.Add(modifier);
			_statModifiers.Sort((a, b) => a.Order.CompareTo(b.Order));
			modifier.OnEnabledStateChanged.AddListener(_ => _isDirty = true);
			_isDirty = true;
		}

		private float RecalculateValue() {
			float value = BaseValue;
			float totalFlat = 0f;
			float totalAdditive = 1f;
			float totalMultiplicative = 1f;

			foreach (var modifier in _statModifiers.Where(modifier => modifier.IsEnabled)) {
				switch (modifier.Type) {
					case StatModifierType.Flat:
						totalFlat += modifier.Value;
						break;
					case StatModifierType.Additive:
						totalAdditive += modifier.Value;
						break;
					case StatModifierType.Multiplicative:
						totalMultiplicative *= modifier.Value;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			return (value + totalFlat) * totalAdditive * totalMultiplicative;
		}
	}
}
