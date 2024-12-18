using Sandbox;
using System;
using System.Text.RegularExpressions;

namespace Solutions;

class DayThree
{
    private class StateInstruction(Match instruct) : Instruction
    {
        bool Instruction.IsStateInstruction => true;

        bool isDo { get; } = instruct.Length == 4;
    }


public static void Main()
    {

        using (StreamReader fileInput = new StreamReader("C:/Users/Brenden/Desktop/input3.txt"))
        {
            int count = 0;

            string line;
            string allLines = "";
            while ((line = fileInput.ReadLine()) != null)
            {
                allLines += line;
            }

            ReturnSumOfLineWithInstructions(allLines);

            Console.WriteLine(count);
        }
    }

    static int ReturnSumOfLine(string line)
    {
        //used for part one...
        int sum = 0;
        while (!line.Equals(""))
        {
            string regx = "mul\\(\\d{1,3},\\d{1,3}\\)";
            Match target = Regex.Match(line, regx);

            if (target.Success)
            {
                line = line.Substring(target.Index);
                string current = line.Substring(0, target.Length);
                string[] findableDigits = current.Split(",");
                string firstDigit = findableDigits[0].Substring(4);
                string secondDigit = findableDigits[1].Substring(0, findableDigits[1].Length - 1);
                sum += int.Parse(firstDigit) * int.Parse(secondDigit);

                line = line.Substring(target.Length);
            }
            else
            {
                line = "";
            }
        }

        return sum;
    }

    static int ReturnSumOfLineWithInstructions(string line)
    {
        Queue<Instruction> queue = new Queue<Instruction>();
        string multRegx = "mul\\(\\d{1,3},\\d{1,3}\\)";
        string stateRegx = "do\\(\\)|don't\\(\\)";
        while (!line.Equals(""))
        {
            Match state = Regex.Match(line, stateRegx);
            Match mult = Regex.Match(line, multRegx);

            if (state.Success && mult.Success)
            {
                if (mult.Index < state.Index)
                {
                    queue.Enqueue(new MultiplyInstruction(mult));
                    queue.Enqueue(new StateInstruction(state));

                    line = line.Substring(state.Index + state.Length);
                }
                else
                {
                    queue.Enqueue(new StateInstruction(state));
                    queue.Enqueue(new MultiplyInstruction(mult));

                    line = line.Substring(mult.Index + mult.Length);
                }
            }
            else if (mult.Success)
            {
                queue.Enqueue(new MultiplyInstruction(mult));
                line = line.Substring(mult.Index + mult.Length);
            }
            else
            {
                line = "";
            }
        }


        int sum = 0;
        bool isOperating = true;
        while(queue.Count > 0)
        {
            Instruction current = queue.Dequeue();
            if(current.IsStateInstruction)
            {
                
            }
        }
            
    }

}