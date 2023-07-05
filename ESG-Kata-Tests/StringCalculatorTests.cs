using ESG_Kata;
using FluentAssertions;

namespace ESG_Kata_Tests
{
    [TestClass]
    public class StringCalculatorTests
    {
        [TestMethod]
        public void StringCalculator_DifferentValidInput_CorrectResults()
        {
            string empty = "";
            string singleNumber = "1";
            string multiNumber = "1,2";
            string tenNumbers = "1,2,1,3,4,5,1,2,3,11";

            int emptyResult = StringCalculator.Add(empty);
            int singleResult = StringCalculator.Add(singleNumber);
            int multiResult = StringCalculator.Add(multiNumber);
            int tenResult = StringCalculator.Add(tenNumbers);

            emptyResult.Should().Be(0);
            singleResult.Should().Be(1);
            multiResult.Should().Be(3);
            tenResult.Should().Be(33);
        }

        [TestMethod]
        public void StringCalculator_NewLine_CorrectResults()
        {
            string newlineNumber = "1\n2,3";

            int newlineResult = StringCalculator.Add(newlineNumber);

            newlineResult.Should().Be(6);
        }

        [TestMethod]
        public void StringCalculator_CustomDelimiter_CorrectResults()
        {
            string customDelimiterNumber = "//;\n2;3";

            int customDelimiterResult = StringCalculator.Add(customDelimiterNumber);

            customDelimiterResult.Should().Be(5);
        }

        [TestMethod]
        public void StringCalculator_negativeNumber_throwsException()
        {
            string negativeNumber = "-1";
            string multipleNegativeNumbers = "1,-2,3,-5";

            Action act = () =>
            {
                StringCalculator.Add(negativeNumber);
            };

            Action act2 = () =>
            {
                StringCalculator.Add(multipleNegativeNumbers);
            };

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Negatives not allowed: -1");

            act2.Should().Throw<ArgumentException>()
                .WithMessage($"Negatives not allowed: -2,-5");
        }

        [TestMethod]
        public void StringCalculator_IgonoreOver1000_CorrectResults()
        {
            string customDelimiterNumbers = "1001,2";

            int customDelimiterResult = StringCalculator.Add(customDelimiterNumbers);

            customDelimiterResult.Should().Be(2);
        }

        [TestMethod]
        public void StringCalculator_AnyLengthDelimiter_CorrectResult()
        {
            string longDelimiterNumbers = "//[|||]\n1|||2|||3";

            int longDelimiterResult = StringCalculator.Add(longDelimiterNumbers);

            longDelimiterResult.Should().Be(6);
        }

        [TestMethod]
        public void StringCalculator_AnyLengthMultipleDelimiters_CorrectResult()
        {
            string longDelimiterNumbers = "//[|][%]\n1|2%3";

            int longDelimiterResult = StringCalculator.Add(longDelimiterNumbers);

            longDelimiterResult.Should().Be(6);
        }

        [TestMethod]
        public void StringCalculator_AnyLengthTenDelimiters_CorrectResult()
        {
            string longDelimiterNumbers = "//[|][%][a][b][c][d][;][:][.][z]\n1|1%1a1b1c1d1;1:1.1z1";

            int longDelimiterResult = StringCalculator.Add(longDelimiterNumbers);

            longDelimiterResult.Should().Be(11);
        }
    }
}