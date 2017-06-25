using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System;

/// <summary>
/// 店員。客との橋渡し
/// </summary>
public class Clerk : MonoBehaviour {

	[SerializeField]
	private ObjectShooter _shooter;
	[SerializeField]
	private CustomerEmotion _emotion;
	[SerializeField]
	private ImageBoard _imageBoard;

	[Header("Customer Parameter")]
	[SerializeField]
	private CustomerQueue _customerQueue;
	[SerializeField]
	private Transform _customerParent;

	[Header("Food")]
	[SerializeField]
	private FoodList _foodList;
	[SerializeField]
	private Vector3 _initBunsPosition;
	[SerializeField]
	private Vector3 _topBunsPosition;

	private Customer _customer;         //現在の客
	private BurgerData _burger;         //現在作成中のバーガー
	private int _foodCnt;               //現在作成中のバーガーの具材の数(バンズを除いたもの)
	private List<Food> _burgerFoods;    //現在作成中のバーガーの具材の実態 

	private void Awake() {
		_burgerFoods = new List<Food>();
	}

	private void Start() {
		if (_shooter) {
			_shooter.onShot.RemoveListener(OnShot);
			_shooter.onShot.AddListener(OnShot);
		}
		//テスト
		GameStart();
	}

	/// <summary>
	/// ゲームの開始
	/// </summary>
	public void GameStart() {
		NextCustomer();
	}

	/// <summary>
	/// 次の客
	/// </summary>
	private void NextCustomer() {
		if (_customerQueue) {
			if (_customerQueue.HasNext()) {
				//次の客をさばく
				if (_customer) {
					LeaveCustomer();
				}
				ComeCustomer(_customerQueue.GetNext());
			} else {
				//客がいないので結果発表へ
				Debug.Log("お客がいないので結果へ飛びます");
			}
		} else {
			Debug.LogError("Customer Queueへの参照が設定されていません！");
			return;
		}
	}

	/// <summary>
	/// 客の帰宅
	/// </summary>
	private void LeaveCustomer() {
		//客を削除
		if (_customer) {
			Destroy(_customer.gameObject);
		}
		for (int i = 0; i < _burgerFoods.Count; ++i) {
			Destroy(_burgerFoods[i].gameObject);
		}
	}

	/// <summary>
	/// 客の来店
	/// </summary>
	/// <param name="c">Customer.</param>
	private void ComeCustomer(Customer c) {
		_customer = Instantiate(c);
		_customer.transform.SetParent(_customerParent, false);
		_imageBoard.SetBurger(_customer.order.contents);
		if (_emotion) {
			_emotion.ShowEmotion(0);
		}
		_foodCnt = 0;
		_burgerFoods.Clear();
		// 最初のバンズを配置する
		_burgerFoods.Add(InstantiateFood(FoodType.BunsBottom, _initBunsPosition));
		_burger = new BurgerData();
		_burger.Add(FoodType.BunsBottom, _initBunsPosition.x);
		//シューターを操作できるように
		if (_shooter) {
			_shooter.controllable = true;
		}
	}

	/// <summary>
	/// 発射イベント
	/// </summary>
	/// <param name="offset">Offset.</param>
	/// <param name="food">Food.</param>
	private void OnShot(float offset, Food food) {
		_foodCnt++;
		_burger.Add(food.type, offset);
		_burgerFoods.Add(food);

		//客に評価させる
		if (_customer) {
			int rank = _customer.Evaluate(_burger);
			if (_emotion) {
				_emotion.ShowEmotion(rank);
			}
		}
		//最後の具材か判定
		if (_customer.orderFoodCnt <= _foodCnt) {
			//シューターを操作できないように
			if (_shooter) {
				_shooter.controllable = false;
			}
			StartCoroutine(Wait(1f, () => {
				//上のバンズを落として提供する
				_burgerFoods.Add(InstantiateFood(FoodType.BunsTop, _topBunsPosition));
				_burger.Add(FoodType.BunsTop, _topBunsPosition.x);
				int rank = _customer.Evaluate(_burger);
				if (_emotion) {
					_emotion.ShowEmotion(rank);
				}
				StartCoroutine(Wait(1f, () => {
					NextCustomer();
				}));
			}));
		}
	}

	/// <summary>
	/// 待機したのちにイベント実行
	/// </summary>
	/// <returns>The wait.</returns>
	private IEnumerator Wait(float wait, Action callback) {
		yield return new WaitForSeconds(wait);
		if (callback != null) {
			callback.Invoke();
		}
	}

	/// <summary>
	/// 食材オブジェクトを生成する
	/// </summary>
	/// <returns>The food.</returns>
	/// <param name="food">Food.</param>
	/// <param name="position">Position.</param>
	/// <param name="scale">Scale.</param>
	private Food InstantiateFood(FoodType food, Vector3 position) {
		var foodObj = Instantiate(_foodList.GetFood(food), position, Quaternion.identity);
		foodObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		return foodObj;
	}
}