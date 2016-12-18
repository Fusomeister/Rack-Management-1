using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rack_Management_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Program.Scrabble("ladilmy", "daily"));
            Console.WriteLine(Program.Scrabble("eerriin", "eerie"));
            Console.WriteLine(Program.Scrabble("orrpgma", "program"));
            Console.WriteLine(Program.Scrabble("orppgma", "program"));

            Console.WriteLine("");

            Console.WriteLine(Program.ScrabbleWild("pizza??", "pizzazz"));
            Console.WriteLine(Program.ScrabbleWild("piizza?", "pizzazz"));
            Console.WriteLine(Program.ScrabbleWild("a??????", "program"));
            Console.WriteLine(Program.ScrabbleWild("b??????", "program"));

            Console.WriteLine("");

            Program.LongestWord("dcthoyueorza");
            Program.LongestWord("uruqrnytrois");
            Program.LongestWord("rryqeiaegicgeo??");
            Program.LongestWord("udosjanyuiuebr??");
            Program.LongestWord("vaakojeaietg????????");

            Console.WriteLine("");

            Program.HighestPoints("dcthoyueorza");
            Program.HighestPoints("uruqrnytrois");
            Program.HighestPoints("rryqeiaegicgeo??");
            Program.HighestPoints("udosjanyuiuebr??");
            Program.HighestPoints("vaakojeaietg????????");

        }

        public static bool Scrabble(string tiles, string word)
        {
            string temp = string.Empty;
            char[] tileArr = tiles.ToCharArray();

            for (int i = 0; i < word.Length; i++)
            {
                if (tileArr.Contains(word[i]))
                {
                    temp += word[i];

                    List<char> tmp = new List<char>(tileArr);
                    tmp.Remove(word[i]);
                    tileArr = tmp.ToArray();
                }
            }

            if (temp == word)
            {
                return true;
            }

            return false;
        }

        public static bool ScrabbleWild(string tiles, string word)
        {
            string tmpWord = string.Empty;
            char[] tilesArr = tiles.ToCharArray();

            for (int i = 0; i < word.Length; i++)
            {
                if (tilesArr.Contains(word[i]))
                {
                    tmpWord += word[i];

                    List<char> tmp = new List<char>(tilesArr);
                    tmp.Remove(word[i]);
                    tilesArr = tmp.ToArray();
                }

                else if (!tilesArr.Contains(word[i]) && tilesArr.Contains('?'))
                {
                    tmpWord += word[i];

                    List<char> tmp = new List<char>(tilesArr);
                    tmp.Remove('?');
                    tilesArr = tmp.ToArray();
                }
            }

            if (tmpWord == word)
            {
                return true;
            }

            return false;
        }

        public static void LongestWord(string tiles)
        {
            string wordOnList = string.Empty;
            //int counter = 0;
            List<string> words = new List<string>();
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"D:\Visual Studio Projekter\Rack Management 1\Rack Management 1\bin\Debug\enable1.txt");

            char[] tilesArr = tiles.ToCharArray();
            string tempWord = string.Empty;

            while ((wordOnList = file.ReadLine()) != null)
            {
                for (int i = 0; i < wordOnList.Length; i++)
                {
                    if (tilesArr.Contains(wordOnList[i]))
                    {
                        tempWord += wordOnList[i];

                        List<char> tmp = new List<char>(tilesArr);
                        tmp.Remove(wordOnList[i]);
                        tilesArr = tmp.ToArray();
                    }

                    else if (!tilesArr.Contains(wordOnList[i]) && tilesArr.Contains('?'))
                    {
                        tempWord += wordOnList[i];

                        List<char> tmp = new List<char>(tilesArr);
                        tmp.Remove('?');
                        tilesArr = tmp.ToArray();
                    }
                }

                if (wordOnList == tempWord)
                {
                    words.Add(wordOnList);

                    tilesArr = tiles.ToCharArray();
                    tempWord = string.Empty;
                }
                else
                {
                    tilesArr = tiles.ToCharArray();
                    tempWord = string.Empty;
                }

                //counter++;
            }
            //file close, done looking
            file.Close();

            //find the big word
            int longestWord = 0;
            string theWord = string.Empty;

            foreach (var word in words)
            {
                if (longestWord < word.Length)
                {
                    longestWord = word.Length;
                    theWord = word;
                }
            }

            Console.WriteLine(theWord);
        }

        public static void HighestPoints(string tiles)
        {
            string wordOnList = string.Empty;
            //int counter = 0;
            List<string> words = new List<string>();
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"D:\Visual Studio Projekter\Rack Management 1\Rack Management 1\bin\Debug\enable1.txt");

            char[] tilesArr = tiles.ToCharArray();
            string tempWord = string.Empty;

            //point scoring
            int mostPoints = 0;
            string theWord = string.Empty;

            while ((wordOnList = file.ReadLine()) != null)
            {
                int tmpPoints = 0;

                for (int i = 0; i < wordOnList.Length; i++)
                {

                    if (tilesArr.Contains(wordOnList[i]))
                    {
                        //call TileValue method, start counting
                        tmpPoints += Program.TileValue(wordOnList[i]);
                        tempWord += wordOnList[i];

                        List<char> tmp = new List<char>(tilesArr);
                        tmp.Remove(wordOnList[i]);
                        tilesArr = tmp.ToArray();
                    }

                    else if (!tilesArr.Contains(wordOnList[i]) && tilesArr.Contains('?'))
                    {
                        tempWord += wordOnList[i];

                        List<char> tmp = new List<char>(tilesArr);
                        tmp.Remove('?');
                        tilesArr = tmp.ToArray();
                    }
                }

                if (wordOnList == tempWord)
                {
                    words.Add(wordOnList);

                    //is this real word bigger?
                    if (mostPoints < tmpPoints)
                    {
                        mostPoints = tmpPoints;
                        theWord = wordOnList;
                    }
                }

                //reset
                tilesArr = tiles.ToCharArray();
                tempWord = string.Empty;

                //counter++;
            }

            //file close, done looking
            file.Close();

            Console.WriteLine(theWord);
        }

        public static int TileValue(char letter)
        {
            switch (letter)
            {
                case '?':
                    return 0;

                case 'e':
                case 'a':
                case 'i':
                case 'o':
                case 'n':
                case 'r':
                case 't':
                case 'l':
                case 's':
                case 'u':
                    return 1;

                case 'd':
                case 'g':
                    return 2;

                case 'b':
                case 'c':
                case 'm':
                case 'p':
                    return 3;

                case 'f':
                case 'h':
                case 'v':
                case 'w':
                case 'y':
                    return 4;

                case 'k':
                    return 5;

                case 'j':
                case 'x':
                    return 8;

                case 'q':
                case 'z':
                    return 10;

                default:
                    return 0;
            }
        }
    }
}
