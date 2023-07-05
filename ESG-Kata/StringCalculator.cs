using System.Text;
using System.Text.RegularExpressions;

namespace ESG_Kata
{
    public static class StringCalculator
    {
        public static int Add(string input)
        {
            List<string> delimiters = GetDelimiter(input);

            return CalculateResult(GetNumberStrings(input, delimiters));
        }

        private static List<string> GetDelimiter(string input)
        {
            List<string> delimiters = new List<string>();

            MatchCollection matches = Regex.Matches(input, @"\[(.*?)\]");

            foreach (Match match in matches)
            {
                delimiters.Add(match.Groups[1].Value);
            }

            if (matches.Count < 1)
            {
                if (input.StartsWith("//"))
                {
                    delimiters.Add(input.Substring(2, 1));
                }
                else
                {
                    delimiters.Add(",");
                }
            }

            return delimiters;
        }

        private static int CalculateResult(List<string> numberStrings)
        {
            int result = 0;
            List<int> negativeNumbers = new List<int>();

            numberStrings.ForEach(s =>
            {
                if (int.TryParse(s, out int number) && number < 1000)
                {
                    if (number < 0)
                    {
                        negativeNumbers.Add(number);
                    }
                    result += number;
                }
            });

            CheckNegativeNumebers(negativeNumbers);

            return result;
        }

        private static void CheckNegativeNumebers(List<int> negativeNumbers)
        {
            if (negativeNumbers.Count > 0)
            {
                throw new ArgumentException("Negatives not allowed: " + BuildExceptionString(negativeNumbers));
            }
        }

        private static string BuildExceptionString(List<int> negativeNumbers)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < negativeNumbers.Count; i++)
            {
                builder.Append(negativeNumbers[i]);
                if (i < negativeNumbers.Count - 1)
                {
                    builder.Append(",");
                }
            }

            return builder.ToString();
        }

        private static List<string> GetNumberStrings(string input, List<string> delimiters)
        {
            string[] lines = input.Split('\n');
            List<string> numberStrings = new List<string>();
            lines.ToList().ForEach(s =>
            {
                numberStrings.AddRange(s.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList());
            });

            return numberStrings;
        }
    }
}
