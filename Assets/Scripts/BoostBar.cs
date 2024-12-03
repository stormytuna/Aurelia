using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BoostBar : MonoBehaviour
{
	private Slider _slider;

	void Awake() {
		_slider = GetComponent<Slider>();

		ShipController playerShipController = Singleton<GameManager>.instance.Player.GetComponent<ShipController>();
		_slider.maxValue = playerShipController.MaxBoostTime;

		playerShipController.OnBoostChanged.AddListener((newBoostTime) => {
			_slider.value = newBoostTime;
		});
	}
}
