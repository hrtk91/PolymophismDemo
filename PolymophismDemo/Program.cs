using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolymophismDemo
{
	// ポリモーフィズム（多態性）のデモ
	// スイッチケースで分岐していた処理をポリモーフィズムで置き換える
	class Program
	{
		static void Main(string[] args)
		{
			/// スイッチを使う場合の処理

			// まずどのクラスのRunを実行するか前もって決めておく。
			// 今回は全部
			var isHuman = true;
			var isDog = true;
			var isCar = true;

			// 人間だったら0で実行
			if (isHuman)
			{
				doRunSwitch(0);
			}
			// 犬だったら1で実行
			if (isDog)
			{
				doRunSwitch(1);
			}
			// 車だったら2で実行
			if (isCar)
			{
				doRunSwitch(2);
			}

			/// ポリモーフィズムを使った処理
			Runable runnable = new RunableHuman() as Runable;
			doRun(runnable);

			runnable = new RunableDog() as Runable;
			doRun(runnable);

			runnable = new RunableCar() as Runable;
			doRun(runnable);

			Console.ReadKey();
		}

		static void doRunSwitch(int mode)
		{
			switch (mode)
			{
				// 人の場合
				case 0:
					var human = new Human();
					human.Run();
					break;
				// 犬の場合
				case 1:
					var dog = new Dog();
					dog.Run();
					break;
				// 車の場合
				case 2:
					var car = new Car();
					car.Run();
					break;
				// ポリモーフィズムを使わないと、Runメソッドを
				// 実行する必要のあるクラスはすべて列挙しないといけなくなる
				// case 3:
				//		var cat = new Cat();
				//		cat.Run();
				//		break;
				// case 4:
				//		var horse = new Horse();
				//		horse.Run();
				//		break;
				// ...
				// switch の判定条件mode変数も値が増えていく -> 管理コストが増大
				default:
					break;
			}
		}

		static void doRun(Runable runnnable)
		{
			// ポリモーフィズムを使えばswitch文のように
			// 列挙しなくてもいい
			// mode変数なんか用意しなくてもいい -> 管理コストの低減
			// 大きなプロジェクトになると管理コストの増大で工数も増える…
			runnnable.Run();
		}
	}

	public class Human
	{
		public void Run()
		{
			Console.WriteLine("Human run");
		}
	}
	public class Dog
	{
		public void Run()
		{
			Console.WriteLine("Dog run");
		}
	}
	public class Car
	{
		public void Run()
		{
			Console.WriteLine("Car run");
		}
	}

	/// <summary>
	/// メソッドRunを提供するインターフェース。
	/// </summary>
	public interface Runable
	{
		void Run();
	}

	public class RunableHuman : Runable
	{
		public void Run()
		{
			Console.WriteLine("RunableHuman Run");
		}
	}
	public class RunableDog : Runable
	{
		public void Run()
		{
			Console.WriteLine("RunableDog Run");
		}
	}
	public class RunableCar : Runable
	{
		public void Run()
		{
			Console.WriteLine("RunableCar Run");
		}
	}
}
