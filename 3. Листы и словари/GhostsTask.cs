using System;
using System.Text;

namespace hashes
{
	public class GhostsTask :
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
		IMagic
	{
		private Robot robot = new Robot("Android");
		private Cat cat = new Cat("Stepa", "Domestic", new DateTime(2022, 05, 10));
		private Vector vector = new Vector(1, 0);
		private Segment segment = new Segment(new Vector(0, 1), new Vector(1, 0));
		private static byte[] documentText = new byte[] { 10, 9, 8, 7 };
		private Document document = new Document("Document", Encoding.UTF8, documentText);

		public void DoMagic()
		{
			Robot.BatteryCapacity += 10; //public static, при изменении ломается хэш 
			cat.Rename("Vas'ka"); //изменяем имя кота через Animal, хэш ломается
			vector.Add(new Vector(10, 10));//Х и Y public, при изменении ломается хэш 
			segment.Start.Add(new Vector(10, 10));//Х и Y вектора start public, при изменении ломается хэш 
			documentText[2]++; //если после создания поменять массив content, хэш ломается
		}

		// Чтобы класс одновременно реализовывал интерфейсы IFactory<A> и IFactory<B> 
		// придется воспользоваться так называемой явной реализацией интерфейса.
		// Чтобы отличать методы создания A и B у каждого метода Create нужно явно указать, к какому интерфейсу он относится.
		// На самом деле такое вы уже видели, когда реализовывали IEnumerable<T>.

		Vector IFactory<Vector>.Create()
		{
			return vector;
		}

		Segment IFactory<Segment>.Create()
		{
			return segment;
		}

		Robot IFactory<Robot>.Create()
		{
			return robot;
		}

		Cat IFactory<Cat>.Create()
		{
			return cat;
		}

		Document IFactory<Document>.Create()
		{
			return document;
		}
	}
}