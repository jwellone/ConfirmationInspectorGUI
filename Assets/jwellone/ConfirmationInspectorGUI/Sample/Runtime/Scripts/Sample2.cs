using System.Collections.Generic;
using UnityEngine;

#nullable enable

namespace jwellone.Sample
{
    public class Sample2 : MonoBehaviour
    {
        public class Data
        {
            public readonly string name;
            public readonly int hp;
            public readonly float mp;
            public readonly float attack;

            public Data(string name, int hp, float mp, float attack)
            {
                this.name = name;
                this.hp = hp;
                this.mp = mp;
                this.attack = attack;
            }
        }

        private readonly Data[] _data = new[]
        {
            new Data("EnemyA",10,5f,0.1f), 
            new Data("EnemyB",100,10.5f,10.5f), 
            new Data("EnemyC",300,200f,50f)
        };

        private readonly List<Vector2> _vecList = new List<Vector2>();
        private readonly List<int> _intList = new List<int>();
        private readonly List<Data> _dataList = new List<Data>();

		private void Awake()
		{
            _vecList.Add(Vector2.zero);
            _vecList.Add(Vector2.one);
            _vecList.Add(Vector2.up);

            _intList.Add(1);
            _intList.Add(2);
            _intList.Add(3);

            _dataList.Add(new Data("EnemyA", 10, 5f, 0.1f));
            _dataList.Add(new Data("EnemyB", 100, 10.5f, 10.5f));
            _dataList.Add(new Data("EnemyC", 300, 200f, 50f));
        }
	}
}