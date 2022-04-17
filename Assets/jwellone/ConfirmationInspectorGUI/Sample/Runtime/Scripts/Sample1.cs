using System;
using UnityEngine;

#nullable enable

namespace jwellone.Sample
{
	public class Sample1 : MonoBehaviour
	{
		enum Step
		{
			One,
			Two,
			Three
		}

		[SerializeField] private Step _step = Step.One;
		[SerializeField] private string _name = null!;
		[SerializeField] private int _hp = 10;

		public string Name => _name;
		public int hp => _hp;
		private bool _isValid;
		long _longValue = 123L;
		uint _uintValue = 111;
		double _doubleValue = 0.000123d;
		DateTime dateTime;
	}
}
