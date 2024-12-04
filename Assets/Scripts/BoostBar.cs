using Helpers;
using ShipParts.Evasions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BoostBar : MonoBehaviour
{
	private Slider _slider;

	void Awake() {
		_slider = GetComponent<Slider>();

		Boost boost = Singleton<GameManager>.instance.Player.RecursivelyFindChildWithComponent<Boost>();
		if (boost == null) {
			gameObject.SetActive(false);
			return;
		}
		
		_slider.maxValue = boost.MaxBoostTime;

		boost.OnBoostChanged.AddListener((newBoostTime) => {
			_slider.value = newBoostTime;
		});
	}
}
