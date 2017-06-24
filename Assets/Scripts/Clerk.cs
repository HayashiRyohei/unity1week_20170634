using UnityEngine;
using System.Collections;

/// <summary>
/// 店員。客との橋渡し
/// </summary>
public class Clerk : MonoBehaviour {

	[SerializeField]
	private ObjectShooter _shooter;
	[SerializeField]
	private Customer _customer;
	[SerializeField]
	private CustomerEmotion _emotion;

	private BurgerData _burger;

	private void Start() {
		if(_shooter) {
			_shooter.onShot.RemoveListener(OnShot);
			_shooter.onShot.AddListener(OnShot);
		}
		//とりあえずここで
		OnComeCustomer();
	}

	/// <summary>
	/// 来店イベント
	/// </summary>
	private void OnComeCustomer() {
		if(_emotion) {
			_emotion.ShowEmotion(0);
		}
	}

	/// <summary>
	/// 発射イベント
	/// </summary>
	/// <param name="offset">Offset.</param>
	/// <param name="food">Food.</param>
	private void OnShot(float offset, FoodType food) {
		if(_burger == null) {
			_burger = new BurgerData();
		}
		_burger.Add(food, offset);

		//客に評価させる
		if(_customer) {
			int rank = _customer.Evaluate(_burger);
			if(_emotion) {
				_emotion.ShowEmotion(rank);
			}
		}
	}


}