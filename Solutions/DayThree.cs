using System;
using System.Text.RegularExpressions;

namespace Solutions;

class DayThree
{
    private interface Instruction
    {
        bool IsStateInstruction { get; }

    }

    private class StateInstruction(Match instruct) : Instruction
    {
        bool Instruction.IsStateInstruction => true;

        public bool IsDo { get; } = instruct.Length == 4;

        public override string ToString() {
            if(IsDo) {return "DO";} else {return "DONT";}
        }
    }
    
    private class MultiplyInstruction(Match instruct) : Instruction
    {
        bool Instruction.IsStateInstruction => false;

        public int result { get; set; } = GetResult(instruct);


        public override string ToString() {
            return "Mul: " + result;
        }

        private static int GetResult(Match instruct)
        {
            string parsableWithParenthesis = instruct.ToString().Substring(4);
            string parsable = parsableWithParenthesis.Substring(0,parsableWithParenthesis.Length-1);
            string[] parseArr = parsable.Split(",");
            return int.Parse(parseArr[0]) * int.Parse(parseArr[1]);
        }


    }


public static void Run()
    {

        using (StreamReader fileInput = new StreamReader("input3.txt"))
        {
            string line;
            string allLines = "";
            while ((line = fileInput.ReadLine()) != null)
            {
                allLines += line;
            }

            Console.WriteLine(ReturnSumOfLineWithInstructions(allLines));
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

                    line = line.Substring(mult.Index + mult.Length);
                }
                else
                {
                    queue.Enqueue(new StateInstruction(state));

                    line = line.Substring(state.Index + state.Length);
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
                StateInstruction currentState = (StateInstruction) current;
                isOperating = currentState.IsDo;
                
            } else {
                MultiplyInstruction currentMultiplyer = (MultiplyInstruction) current;
                if(isOperating) {
                    sum += currentMultiplyer.result;
                }
            }
        }

        return sum;
            
    }

}