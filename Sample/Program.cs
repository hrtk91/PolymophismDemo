using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 安富: 俺俺ポリモフィズム
/// Visual Studio 2015 (C# 6.0)
/// </summary>
namespace Sample
{
    /// <summary>
    /// 実行可能を表現するインターフェース
    /// </summary>
    public interface IRunnable
    {
        void Run();
    }

    /// <summary>
    /// 実行可能な人間 を表現したインターフェース
    /// </summary>
    public interface IHuman : IRunnable
    {

    }

    /// <summary>
    /// 実行可能な人間クラス
    /// </summary>
    public class Human : IHuman
    {
        public void Run() => Console.WriteLine("Human Run");
    }

    /// <summary>
    /// 実行可能な車クラス
    /// </summary>
    public class Car : IRunnable
    {
        public void Run() => Console.WriteLine("Car Run");
    }

    /// <summary>
    /// 実行可能な犬クラス
    /// </summary>
    public class Dog : IRunnable
    {
        public void Run() => Console.WriteLine("Dog Run");
    }

    /// <summary>
    /// 実行種別
    /// </summary>
    enum RunClass
    {
        Human, Car, Dog,
    }

    class Program
    {
        /// <summary>
        /// 抽象的に実行できるオブジェクトを実行する
        /// </summary>
        /// <param name="runnable"></param>
        static void Runner(IRunnable runnable) => runnable.Run();

        static void Main(string[] args)
        {
            Console.WriteLine("多態性を用いない実行、すべてのパターンを直接記述");
            NotExecutableObjectRunTest(RunClass.Car);
            NotExecutableObjectRunTest(RunClass.Dog);
            NotExecutableObjectRunTest(RunClass.Human);

            Console.WriteLine();

            Console.WriteLine("多態性を用いた実行、一つの記述(実行専用の処理)で様々なオブジェクトを実行する");
            IRunnable car = new Car();
            IRunnable dog = new Dog();
            IRunnable human = new Human();

            ExecutableObjectRunTest(car);
            ExecutableObjectRunTest(dog);
            ExecutableObjectRunTest(human);

            Console.WriteLine();

            Console.WriteLine("多態性 +α を用いた実行、一部のオブジェクトに対するふるまいを変える");
            ExecutableObjectRunTest2(car);
            ExecutableObjectRunTest2(dog);
            ExecutableObjectRunTest2(human);


            Console.ReadLine();
        }

        /// <summary>
        /// 多態性を用いないクラスの実行メソッド
        /// </summary>
        static void NotExecutableObjectRunTest(RunClass select)
        {
            // 多態性を用いないと,複数のパターンに対応したメソッドを作る場合
            // 引数に渡されるパターン分の処理を記述しておかなければいけない。
            switch (select)
            {
                case RunClass.Human:
                    new Human().Run();
                    break;

                case RunClass.Car:
                    new Car().Run();
                    break;
                case RunClass.Dog:
                    new Dog().Run();
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 多態性を用いたクラスの実行メソッド
        /// </summary>
        /// <param name="runnable">実行可能(IRunnable)なオブジェクト</param>
        // 多態性を用いればざっくりとしたオブジェクトを渡すことができるので、
        // そのオブジェクトに対しての処理を記述するだけで様々なオブジェクトに対する
        // 処理をまとめることができる。
        static void ExecutableObjectRunTest(IRunnable runnable) => runnable.Run();

        /// <summary>
        /// 多態性を用いて処理を行うが、オブジェクトによって少し挙動を変えるテスト
        /// </summary>
        /// <param name="runnable">実行可能オブジェクト</param>
        static void ExecutableObjectRunTest2(IRunnable runnable)
        {
            // もう少し細分化して処理を決めたい場合
            // ただし、一部のオブジェクトに関して処理を分岐する。
            // (大体のオブジェクトには同じ処理を適応する)

            // 人間を処理する場合にはひと手間加える
            if (runnable is IHuman)
            {
                Console.Write("人間が処理を行います: ");
            }

            runnable.Run();
        }
    }
}
