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

        /// <summary>
        /// </summary>
        /// <param name="original"></param>
        /// <param name="search"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
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

        #region Is Number

        private enum NumberState
        {
            Start,
            Sign, // +-
            Zero,
            Number, // 1-9
            ZeroNumber, // 0-9
            Decimal, // .
            Exponent, // E
            NaN
        }

        /// <summary>
        /// 1. Positive/Negative Integer,
        /// 2. Positive/Negative decimal with only one dot(.)
        /// 3. E: such as 1.3E2333343
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(string s)
        {
            NumberState state = NumberState.Start;

            foreach (char c in s)
            {
                NumberState nextState = NumberState.NaN;

                switch (state)
                {
                    case NumberState.Start:
                        //valid start values, sign, zero, number
                        if ((c == '+') || (c == '-'))
                        {
                            nextState = NumberState.Sign;
                        }
                        else if (c == '0')
                        {
                            nextState = NumberState.Zero;
                        }
                        else if ((c >= '1') && (c <= '9'))
                        {
                            nextState = NumberState.Number;
                        }
                        break;
                    case NumberState.Sign:
                        //only numbers can come now
                        if (c == '0')
                        {
                            nextState = NumberState.Zero;
                        }
                        else if ((c >= '1') && (c <= '9'))
                        {
                            nextState = NumberState.Number;
                        }
                        break;
                    case NumberState.Zero:
                        //if we had a zero, only valid one is decimal now
                        if (c == '.')
                        {
                            nextState = NumberState.Decimal;
                        }
                        break;
                    case NumberState.Decimal:
                        //if we are in decimal it has to be numbers now including zero
                        if ((c >= '0') && (c <= '9'))
                        {
                            nextState = NumberState.ZeroNumber;
                        }
                        break;
                    case NumberState.Number:
                        //valid ones are another decimal point, all numbers and exponent
                        if (c == '.')
                        {
                            nextState = NumberState.Decimal;
                        }
                        else if (c == 'E')
                        {
                            nextState = NumberState.Exponent;
                        }
                        else if ((c >= '0') && (c <= '9'))
                        {
                            nextState = NumberState.Number;
                        }
                        break;
                    case NumberState.ZeroNumber:
                        //since we are already in decimals, only valid ones are other numbers and exponent
                        if (c == 'E')
                        {
                            nextState = NumberState.Exponent;
                        }
                        else if ((c >= '0') && (c <= '9'))
                        {
                            nextState = NumberState.ZeroNumber;
                        }
                        break;
                    case NumberState.Exponent:
                        //if we are past exponent, only integers are allowed
                        if ((c == '+') || (c == '-'))
                        {
                            nextState = NumberState.Exponent;
                        }
                        else if ((c >= '0') && (c <= '9'))
                        {
                            nextState = NumberState.Exponent;
                        }
                        break;
                    default:
                        nextState = NumberState.NaN;
                        break;
                }

                state = nextState;
            }

            return state != NumberState.NaN;
        }

        #endregion

        #region Isomorphic

        /// <summary>
        /// Two words are called isomorphic if the letters in one word can be remapped to get the second word.
        /// 
        /// given "foo", "app"; returns true we can map f -> a and o->p
        /// given "bar", "foo"; returns false we can't map both 'a' and 'r' to 'o'
        /// given "ab", "ca"; returns true we can map 'a' -> 'b' and 'c' -> 'a'
        /// 
        /// seems pretty simple, retain a mapping dictionary, where key is the word to change to (o in example 2)
        /// if value is different (a in example 2) from current value (r in example 2) then cannot be isomorphic
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool IsIsomorphic(string s1, string s2)
        {
            if (s1.Length != s2.Length)
            {
                return false;
            }

            Dictionary<char, char> mapping = new Dictionary<char, char>();
            for (int index = 0; index < s1.Length; index++)
            {
                if (mapping.ContainsKey(s2[index])) {
                    if (mapping[s2[index]] != s1[index]) {
                        return false;
                    }
                }
                else {
                    mapping.Add(s2[index], s1[index]);
                }
            }

            return true;
        }

        #endregion
        
        #region Longest Palindrom

        /// <summary>
        /// abcdeeeeeeeeefghi
        /// 
        /// this should be simple, naive solution gives n^3 complexity (for each sub sequence n^2, try
        /// palindrom check n), dynamic solutions gives n^2 with n^2 space, what about iterative solution
        /// 
        /// for each character, try expanding the palindrom as far as it goes
        /// Just be careful that there are two types of palindrom
        /// 
        /// aba and abba
        /// 
        /// so you have to try both
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestPalindrom(string s)
        {
            string longest = string.Empty;

            for (int index = 0; index < s.Length; index++)
            {
                //get ones like aba
                string palindrom1 = GetLongestPalindrom(s, index, index);
                if (palindrom1.Length > longest.Length)
                {
                    longest = palindrom1;
                }

                //get one like abba, also check that there is letter to the right
                if (index < (s.Length - 1))
                {
                    string palindrom2 = GetLongestPalindrom(s, index, index + 1);
                    if (palindrom2.Length > longest.Length)
                    {
                        longest = palindrom2;
                    }
                }
            }

            return longest;
        }

        private static string GetLongestPalindrom(string s, int leftIndex, int rightIndex)
        {
            while ((leftIndex >= 0) && (rightIndex <= (s.Length - 1)) && (s[leftIndex] == s[rightIndex]))
            {
                leftIndex--;
                rightIndex++;
            }

            return s.Substring(leftIndex + 1, rightIndex - 1 - leftIndex);
        }

        #endregion

        #region Shortest Pattern

        /// <summary>
        /// Given a large document and a set of words (W1, W2, W3), find the shortest pattern
        /// which has all the patterns in any order
        /// 
        /// e.g. W2 xxx yyyy W1 jjjj kkk W3
        /// 
        /// If all words in the pattern are unique, we can use a dictionary to keep track of last
        /// found indexes and only replace the solution if max(index) - min(index) < min(length)
        /// </summary>
        /// <param name="longText"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string ShortestPattern(string longText, string[] pattern)
        {
            int minimumLength = int.MaxValue;
            int minStartingIndex = 0;
            int minEndingIndex = 0;

            bool sequenceComplete = false;
            Dictionary<string, int> positions = new Dictionary<string, int>();

            //assume space as a terminator for a while
            StringBuilder lastWord = new StringBuilder();
            for (int index = 0; index < longText.Length; index++)
            {
                char character = longText[index];
                if (character == ' ')
                {
                    //handle word found
                    if (lastWord.Length > 0)
                    {
                        string word = lastWord.ToString().ToLower();
                        lastWord.Clear();

                        if ((positions.ContainsKey(word)) || (pattern.Contains(word)))
                        {
                            if (positions.ContainsKey(word))
                            {
                                //we are updating position of existing words, we need to check if we see another minimum
                                positions[word] = index - 1;
                                if (sequenceComplete)
                                {
                                    //this has to be the current one
                                    int max = positions[word];

                                    //this is ending position of the word, we want the actual min since there could be another
                                    //one with similar ending position but shorter word
                                    int min = positions.Values.Min();
                                    string minWord = (from kv in positions where kv.Value == min select kv.Key).SingleOrDefault();
                                    min -= (minWord.Length - 1);

                                    int length = max - min + 1;
                                    if (length < minimumLength)
                                    {
                                        minimumLength = length;
                                        minStartingIndex = min;
                                        minEndingIndex = max;
                                    }
                                }
                            }
                            else
                            {
                                //we are still finding words...check if our sequence is complete
                                positions.Add(word, index - 1);
                                sequenceComplete = positions.Count == pattern.Length;
                            }
                        }
                    }
                }
                else
                {
                    lastWord.Append(character);
                }
            }

            if (minimumLength != int.MaxValue)
            {
                //we found one
                return (longText.Substring(minStartingIndex, minEndingIndex - minStartingIndex + 1));
            }

            return null;
        }

        #endregion

        #region Word Distance

        /// <summary>
        /// You have a large text file containing words. Given any two words, find the 
        /// shortest distance (in terms of number of words) between them in the file. If the 
        /// operation will be repeated many times for the same file (but different pairs of 
        /// words), can you optimize your solution?
        /// 
        /// as we encounter each words, update the position and see if it's shorter than previous
        /// </summary>
        /// <param name="words"></param>
        /// <param name="wordsToSearch"></param>
        /// <returns></returns>
        public static int WordDistanceIterate(string[] words, string s1, string s2)
        {
            Dictionary<string, int> lookup = new Dictionary<string, int>();

            int minDistance = int.MaxValue;
            for (int index = 0; index < words.Length; index++)
            {
                var word = words[index];
                if ((word == s1) || (word == s2))
                {
                    lookup[word] = index;

                    if (lookup.Count == 2)
                    {
                        int distance = System.Math.Abs(lookup.Values.ElementAt(0) - lookup.Values.ElementAt(1));
                        if (minDistance > distance)
                        {
                            minDistance = distance;
                        }
                    }
                }
            }

            return minDistance;
        }

        ///for reusable method, store index of all occurrence for each work in a dictionary. Then for 2 words, use the
        ///min different in array to find the min different index. (min(abs(x-y)) in Array.cs

        #endregion

    }
}
