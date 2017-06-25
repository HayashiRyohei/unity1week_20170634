using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// 女子高生。JK
/// 欲しいものと反対のものが欲しいツンデレ
/// </summary>
public class Joshikosei : Customer {

	protected override float ContentsEvaluate(List<FoodType> foods) {
		throw new NotImplementedException();
	}

	protected override float VisualEvaluate(List<float> offsets) {
		throw new NotImplementedException();
	}

	protected override float SpecialEvaluate(BurgerData burger) {
		throw new NotImplementedException();
	}
}