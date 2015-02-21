using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class String
    {

        #region Common Character In Multiple Strings

        public static string GetCommonCharacters(string[] words)
        {
            StringBuilder commonCharacters = new StringBuilder();
            Dictionary<char, int> counter = new Dictionary<char, int>();
            Dictionary<char, char> uniqueCharacters = new Dictionary<char, char>();

            foreach (string word in words)
            {
                uniqueCharacters.Clear();
                foreach (char character in word)
                {
                    if (!uniqueCharacters.ContainsKey(character))
                    {
                        //we haven't seen it before let's add it and increment counter
                        uniqueCharacters.Add(character, character);
                        if (!counter.ContainsKey(character))
                        {
                            counter.Add(character, 1);
                        }
                        else
                        {
                            counter[character]++;
                        }
                    }
                }
            }

            foreach (char character in counter.Keys)
            {
                if (counter[character] == words.Length)
                {
                    commonCharacters.Append(character);
                }
            }

            return commonCharacters.ToString();
        }

        #endregion

        #region Justification

        private class TextWrapper
        {
            public string Text { get; set; }
            public int Space { get; set; }
        }

        public static void Justify(string[] text, int length)
        {
            List<List<TextWrapper>> storage = new List<List<TextWrapper>>();

            int lineLength = 0;
            List<TextWrapper> line = new List<TextWrapper>();
            foreach (string word in text)
            {
                int wordLength = word.Length;
                int diff = length - (lineLength + wordLength);
                if (diff < 0)
                {
                    lineLength = CleanupLastWord(lineLength, line);

                    //readjust spaces and create new line
                    int leftOverEqual = (int)System.Math.Floor((length - lineLength) / (1d * (line.Count - 1)));
                    int leftOverMore = (length - lineLength) % (line.Count - 1);
                    int index = 0;
                    foreach (TextWrapper textWord in line)
                    {
                        textWord.Space += leftOverEqual + (index < leftOverMore ? 1 : 0);
                    }
                    storage.Add(line);
                    lineLength = 0;
                    line = new List<TextWrapper>();
                }

                TextWrapper textWrapper = new TextWrapper { Text = word, Space = (diff > 0) ? 1 : 0 };
                line.Add(textWrapper);
                lineLength += wordLength + textWrapper.Space;
            }

            //print rest
            foreach (List<TextWrapper> printLine in storage)
            {
                foreach (TextWrapper word in printLine)
                {
                    Console.Write(word.Text);
                    PrintSpace(word.Space);
                }
                Console.Write("\r\n");
            }

            //process last line;
            lineLength = CleanupLastWord(lineLength, line);
            line[line.Count - 1].Space = length - lineLength + line[line.Count - 1].Space;
            foreach (TextWrapper word in line)
            {
                Console.Write(word.Text);
                PrintSpace(word.Space);
            }
        }

        private static int CleanupLastWord(int lineLength, List<TextWrapper> line)
        {
            //remove space from last word, since it has to be aligned to the end
            TextWrapper lastWord = line[line.Count - 1];

            if (lastWord.Space > 0)
            {
                lineLength -= lastWord.Space;
                lastWord.Space = 0;
            }

            return lineLength;
        }

        private static void PrintSpace(int space)
        {
            for (int count = 0; count < space; count++)
            {
                Console.Write(" ");
            }
        }

        #endregion

        #region String Replace Boyer Moore

        public static string Replace(string original, string search, string replace)
        {
            int index = search.Length - 1;
            List<int> validIndexes = new List<int>();

            Dictionary<char, int> searchIndexes = new Dictionary<char, int>();
            for (int charIndex = search.Length - 1; charIndex >= 0; charIndex--)
            {
                if (!searchIndexes.ContainsKey(search[charIndex]))
                {
                    searchIndexes.Add(search[charIndex], search.Length - charIndex - 1);
                }
            }

            while (index < original.Length)
            {
                //match from the back...if it is not a match, try to skip to the next possible match
                //if the letter exists in the search string, then skip to the last index and try to align,
                //if not, skip the whole length of search string
                char character = original[index];
                if (character == search[search.Length - 1])
                {
                    bool valid = true;
                    for (int charIndex = search.Length - 2; charIndex >= 0; charIndex--)
                    {
                        if (search[charIndex] != original[index - (search.Length - charIndex - 1)])
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid)
                    {
                        validIndexes.Add(index - (search.Length - 1));
                        index += search.Length;
                    }
                    else
                    {
                        index++;
                    }
                }
                else
                {
                    if (searchIndexes.ContainsKey(character))
                    {
                        index += searchIndexes[character];
                    }
                    else
                    {
                        index += search.Length;
                    }
                }
            }

            if (validIndexes.Count > 0)
            {
                List<char> originalChar = new List<char>(original.ToArray());
                foreach (int validIndex in validIndexes)
                {
                    for (int charIndex = 0; charIndex < search.Length; charIndex++)
                    {
                        int replaceIndex = validIndex + charIndex;
                        if (charIndex < replace.Length)
                        {
                            //if replace is within search then replace
                            originalChar[replaceIndex] = replace[charIndex];
                        }
                        else
                        {
                            //remove extra search strings
                            originalChar.RemoveAt(validIndex + replace.Length);
                        }
                    }
                    if (search.Length < replace.Length)
                    {
                        //if replace is longer
                        for (int charIndex = search.Length; charIndex < replace.Length; charIndex++)
                        {
                            int injectIndex = validIndex + search.Length;
                            originalChar.Insert(injectIndex, replace[charIndex]);
                        }
                    }
                }
                return new string(originalChar.ToArray());
            }

            return original;
        }

        #endregion

        #region Reverse Word

        public static string ReverseWord(string original)
        {
            //first reverse everything
            //abc def -> fed cba
            var reversedString = ReverseString(original);

            //foreach word, reverse again
            var buffer = new StringBuilder();
            var startPosition = 0;
            var count = 0;
            foreach (var ch in reversedString)
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

        #endregion

        #region Find All Permutations

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
                foreach (var word in permutations)
                {
                    for (var count = 0; count <= word.Length; count++)
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

        #endregion

        #region Find All Phone Number Permutations

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

        #endregion

        #region Valid Shuffle

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

        #endregion

    }
}
