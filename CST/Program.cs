using System;
using System.Collections.Generic;
using System.Linq;

namespace CST
{
    public class Algorithms
    {
        public static List<int> CountSort(IList<int> collection)
        {
            var valueCount = new JpDict<int, int>(); //自己写的Dictionary，FCL的添加后主键并不是有序的
            foreach (var i in collection)
                if (valueCount.ContainsKey(i))
                    valueCount[i]++;
                else
                    valueCount.Add(Pair.CreatePair(i, 1));

            var valueOrder = valueCount.Select(pair => new CountStruct(pair.First, pair.Second)).ToList();
            for (var i = 0; i < valueOrder.Count - 1; ++i) //设置Order为小于等于Value的数量
                valueOrder[i + 1] = new CountStruct(valueOrder[i + 1].Value, valueOrder[i + 1].Order + valueOrder[i].Order);

            var returnList = new List<int>(collection.Count);
            for (var i = 0; i < collection.Count; ++i)//填充为0
                returnList.Add(0);

            foreach (var countStruct in valueOrder)
            {
                var count = valueCount[countStruct.Value]; //解决相同数据的情况

                for (var i = 0; i < count; ++i)                                     //根据计数插入
                    returnList[countStruct.Order - i - 1] = countStruct.Value;
            }
            return returnList;
        }

        private struct CountStruct
        {
            public int Value { get; }
            public int Order { get; }

            public CountStruct(int value, int order)
            {
                Value = value;
                Order = order;
            }
        }
    }


    public class Program
    {
        public static void Main()
        {
            var lis = new List<int> {2, 5, 7, 8, 3,3};
            var ret = Algorithms.CountSort(lis);
            foreach (var i in ret)
                Console.Write("{0} ", i);
        }
    }
}