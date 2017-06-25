using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// お客さんのキュー
/// </summary>
public class CustomerQueue : MonoBehaviour {

	/// <summary>
	/// お客のグループ
	/// </summary>
	[Serializable]
	private class CustomerGroup {
		[SerializeField, Range(1, 5)]
		private int _selectCnt = 2;
		[SerializeField]
		private Customer[] _customers;

		/// <summary>
		/// ランダムにお客を選ぶ
		/// </summary>
		/// <returns>The random customer.</returns>
		public Customer[] GetRandomCustomer() {
			Customer[] selects = new Customer[_selectCnt];
			List<Customer> temps = new List<Customer>(_customers);
			for (int i = 0; i < _selectCnt; ++i) {
				int index = UnityEngine.Random.Range(0, temps.Count);
				selects[i] = temps[index];
				temps.RemoveAt(index);
			}
			return selects;
		}
	}

	[SerializeField]
	private CustomerGroup[] _groups;

	private Queue<Customer> _selectCustomers;

	private void Awake() {
		_selectCustomers = SelectCustomers();
	}

	/// <summary>
	/// 登場させる客の選択
	/// </summary>
	/// <returns>The customers.</returns>
	private Queue<Customer> SelectCustomers() {
		Queue<Customer> customers = new Queue<Customer>();
		for (int i = 0; i < _groups.Length; ++i) {
			foreach (var c in _groups[i].GetRandomCustomer()) {
				customers.Enqueue(c);
			}
		}
		return customers;
	}

	/// <summary>
	/// 次の確認
	/// </summary>
	/// <returns><c>true</c>, if next was hased, <c>false</c> otherwise.</returns>
	public bool HasNext() {
		return _selectCustomers.Count > 0;
	}

	/// <summary>
	/// 次の客を取得する
	/// </summary>
	/// <returns>The next.</returns>
	public Customer GetNext() {
		return _selectCustomers.Dequeue();
	}
}