using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class String
    {
        public static string ReverseWord(string original)
        {
            //first reverse everything
            //abc def -> fed cba
            var reversedString = ReverseString(original);

            //foreach word, reverse again
            var buffer = new StringBuilder();
            var startPosition = 0;
            var count = 0;
            foreach(var ch in reversedString)
            {
                if (ch == ' ')
                {
                    buffer.Append(ReverseString(reversedString.Substring(startPosition, count - startPosition)));
                    buffer.Append(' ');

                    startPosition = count + 1;
                }
                count++;
            }
            
            //handle the last piece
            buffer.Append(ReverseString(reversedString.Substring(startPosition, count - startPosition)));

            return buffer.ToString();
        }

        private static string ReverseString(string original)
        {
            var charArray = original.ToCharArray();
            for (var count = 0; count < System.Math.Floor(original.Length / 2d); count++)
            {
                var temp = charArray[count];
                charArray[count] = charArray[original.Length - count - 1];
                charArray[original.Length - count - 1] = temp;
            }
            return new string(charArray);
        }

        public static IList<string> FindAllPermutations(string original)
        {
            var result = new List<string>();

            if (original.Length > 0)
            {
                result.AddRange(GetAllPermutations(original));
            }

            return result;
        }

        private static IList<string> GetAllPermutations(string original)
        {
            var result = new List<string>();

            if (original.Length > 1)
            {
                var permutations = GetAllPermutations(original.Substring(1));
                foreach(var word in permutations)
                {
                    for(var count = 0; count <= word.Length; count++)
                    {
                        result.Add(word.Insert(count, original[0].ToString()));
                    }
                }
            }
            else
            {
                result.Add(original);
            }

            return result;
        }

        public static IList<string> FindAllPhoneNumberPermutations(string phoneNumber)
        {
            var result = new List<string>();

            var phoneMap = new Dictionary<int, char[]>
                               {
                                   {0, new char[] {}},
                                   {1, new char[] {}},
                                   {2, new[] {'A', 'B', 'C'}},
                                   {3, new[] {'D', 'E', 'F'}},
                                   {4, new[] {'G', 'H', 'I'}},
                                   {5, new[] {'J', 'K', 'L'}},
                                   {6, new[] {'M', 'N', 'O'}},
                                   {7, new[] {'P', 'Q', 'R', 'S'}},
                                   {8, new[] {'T', 'U', 'V'}},
                                   {9, new[] {'W', 'X', 'Y', 'Z'}},
                               };

            if (phoneNumber.Length > 0)
            {
                result.AddRange(GetAllPhoneNumberPermutations(phoneMap, phoneNumber));
            }

            return result;
        }

        private static IList<string> GetAllPhoneNumberPermutations(Dictionary<int, char[]> phoneMap, string phoneNumber)
        {
            var result = new List<string>();

            if (phoneNumber.Length > 1)
            {
                var permutations = GetAllPhoneNumberPermutations(phoneMap, phoneNumber.Substring(1));
                foreach (var word in permutations)
                {
                    var chars = phoneMap[int.Parse(phoneNumber[0].ToString())];
                    if (chars.Length > 0)
                    {
                        foreach (var ch in chars)
                        {
                            result.Add(ch + word);
                        }
                    }
                    else
                    {
                        result.Add(word);
                    }
                }
            }
            else
            {
                foreach (var ch in phoneMap[int.Parse(phoneNumber[0].ToString())])
                {
                    result.Add(ch.ToString());
                }
            }

            return result;
        }

        public static bool ValidShuffle(string word1, string word2, string shuffle)
        {
            if (word1.Length + word2.Length != shuffle.Length)
            {
                return false;
            }

            return CheckWordFragment(word1, word2, shuffle);
        }

        private static bool CheckWordFragment(string word1, string word2, string shuffle)
        {
            if ((word1.Length > 0) && (word1[0] == shuffle[0]))
            {
                return CheckWordFragment(word1.Substring(1), word2, shuffle.Substring(1));
            }
            else if ((word2.Length > 0) && (word2[0] == shuffle[0]))
            {
                return CheckWordFragment(word1, word2.Substring(1), shuffle.Substring(1));
            }
            else if ((word1.Length == 0) && (word2.Length == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
