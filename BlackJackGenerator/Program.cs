using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            int[] cards = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
            int numberOfSets = 100;

            List<int> allCards = new List<int>(cards.Length * 4);

            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    allCards.Add(cards[i]);
                }
            }

            List<List<int>> sets = new List<List<int>>(numberOfSets);

            for (int i = 0; i < numberOfSets; i++)
            {
                List<int> set = new List<int>();
                List<int> remainingCards = new List<int>(allCards);
                int cardsSum = 0;

                while (cardsSum <= 21)
                {
                    int index = StaticRandom.Rand.Next(remainingCards.Count);
                    int selected = remainingCards[index];
                    remainingCards.RemoveAt(index);

                    cardsSum += selected;
                    set.Add(selected);
                }

                sets.Add(set);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("int[][] prerandomizedSets = new int[][]");
            sb.AppendLine("{");

            for (int i = 0; i < sets.Count; i++)
            {
                sb.Append("    new int[] { ");
                sb.Append(sets[i].Aggregate("", (acc, x) => acc += x + ", "));
                sb.AppendLine("},");
            }

            sb.AppendLine("};");

            //int[][] prerandomizedSets = new int[][] 
            //{ 
            //    new int[] { 1, 2 },
            //    new int[] { 2, 3 },
            //    new int[] { 4, 5 },
            //};

            string text = sb.ToString();
            Console.WriteLine(text);
            Console.WriteLine();

            System.Windows.Clipboard.SetText(text);

            Console.WriteLine("Code has been copied to clipboard.");
            Console.ReadKey();
        }
    }
}
