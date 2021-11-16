using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            for (int t = 0; t < n; t++)
            {
                var consoleInput = Console.ReadLine();

                var cardNumber = Convert.ToInt64(consoleInput);

                var reversedCardNumber = ReverseCardNumber(cardNumber);

                var reversedCardNumberParts = reversedCardNumber.ToString().ToCharArray();

                var intOfReversedCardNumberParts = covertCharArrayToInt(reversedCardNumberParts);

                var A = AddOddPoistionValues(intOfReversedCardNumberParts);

                var evenDigits = ExtractAndSumEven(intOfReversedCardNumberParts);

                var B = CalculateB(evenDigits);

                Validate(A, B);
            }

            Console.ReadLine();
        }

        static long ReverseCardNumber(long cardNumber)
        {
            long reversedCardNumber = 0;
            while (cardNumber > 0)
            {
                var remainder = cardNumber % 10;
                reversedCardNumber = (reversedCardNumber * 10) + remainder;
                cardNumber = cardNumber / 10;
            }
            return reversedCardNumber;
        }

        static int[] covertCharArrayToInt(char[] input)
        {
            int[] convertedArray = Array.ConvertAll(input, c => (int)Char.GetNumericValue(c));

            return convertedArray;
        }

        static int AddOddPoistionValues(int[] input)
        {
            int runningSum = 0;

            for (var i = 0; i < input.Length; i += 2)
            {
                runningSum += input[i];
            }

            return runningSum;
        }

        static int CalculateB(int[] input)
        {
            int sum = 0;
            for(var i = 0; i < input.Length; i++)
            {
                sum += input[i];
            }

            return sum;
        }

        static int[] ExtractAndSumEven(int[] input)
        {
            int[] EvenPositionDigits = new int[input.Length / 2];
            int loopcount = 0;

            for (var i = 1; i < input.Length; i += 2)
            {
                EvenPositionDigits[loopcount] = input[i];
                loopcount++;
            }

            for(var i = 0; i < EvenPositionDigits.Length; i++)
            {
                EvenPositionDigits[i] = EvenPositionDigits[i] * 2;

                if (EvenPositionDigits[i] > 9)
                {
                    var charValues = EvenPositionDigits[i].ToString().ToCharArray();
                    int[] intCharValues = new int[charValues.Length];
                    int runsum = 0;

                    for(var x = 0; x < charValues.Length; x++)
                    {
                        intCharValues[x] = Convert.ToInt32(charValues[x]);
                    }

                    for (var y = 0; y < intCharValues.Length; y++)
                    {
                        runsum += intCharValues[y];
                    }

                    EvenPositionDigits[i] = runsum;
                }
            }

            return EvenPositionDigits;
        }

        static void Validate(int A, int B)
        {
            var sum = A + B;

            if(sum % 10 == 0)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
                
        }
    }
}
