using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreConsoleApp
{
    public interface ICalculation
    {
        // 加算を行うメソッド
        int Add(int x, int y);
    }

    // このクラスは ICalculation に依存しているため、
    // ICalculation が実装されていないとテストができない
    // そこで Moq を使ってテストする
    public class Calculation
    {
        ICalculation hoge;

        public Calculation(ICalculation hoge)
        {
            this.hoge = hoge;
        }

        // 加算結果を２倍にして返すメソッド
        public int AddDouble(int x, int y)
        {
            return hoge.Add(x, y) * 2;
        }
    }
}
