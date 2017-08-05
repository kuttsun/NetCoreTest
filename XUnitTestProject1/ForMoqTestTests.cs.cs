using System;
using Xunit;
using Moq;

namespace NetCoreConsoleApp.Test
{
    // Calculation のテストクラス
    public class CalculationTests
    {
        // AddDouble のテストメソッド
        [Fact]
        public void AddDoubleTest()
        {
            // AddDouble をテストするためには ICalculation の Add が実装されている必要がある
            // なので、ICalculation のモックを作成する
            var mock = new Mock<ICalculation>();

            // Add メソッドの動作を定義する
            // ここでは引数として 2 と 3 を与えた時に 5 を返すように定義している
            mock.Setup(m => m.Add(2, 3)).Returns(5);

            // モックの準備が完了したので、テストコードを書く

            // モックを引数で渡してテスト対象のクラスをインスタンス化する
            var target = new Calculation(mock.Object);

            //　実行と結果の判定
            Assert.Equal(10, target.AddDouble(2, 3));
        }
    }
}
