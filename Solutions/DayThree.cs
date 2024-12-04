using System;
using System.Text.RegularExpressions;

namespace Solutions;

class DayThree {
    public static void Run() {

        using (StreamReader fileInput = new StreamReader("input3.txt")) {
            int count = 0;
            
            string line;
            while(  (line = fileInput.ReadLine()) != null) {
                count += ReturnSumOfLineWithInstructions(line);
            }

            Console.WriteLine(count);
        }
    }

    static int ReturnSumOfLine(string line) {
        //used for part one...
        int sum = 0;
        while(!line.Equals("")){
            string regx = "mul\\(\\d{1,3},\\d{1,3}\\)";
            Match target = Regex.Match(line, regx);

            if(target.Success) {
                line = line.Substring(target.Index);
                string current = line.Substring(0,target.Length);
                string[] findableDigits = current.Split(",");
                string firstDigit = findableDigits[0].Substring(4);
                string secondDigit = findableDigits[1].Substring(0,findableDigits[1].Length-1);
                sum += int.Parse(firstDigit) * int.Parse(secondDigit);

                line = line.Substring(target.Length);
            } else {
                line = "";
            }
        }

        return sum;
    }

    static int ReturnSumOfLineWithInstructions(string line) {
        bool doing = true;
        int sum = 0;
        while(!line.Equals("")){
            string regx = "mul\\(\\d{1,3},\\d{1,3}\\)";
            string instRegx = "do(nt)?\\(\\)";
            Match instruction = Regex.Match(line, instRegx);
            Match target = Regex.Match(line, regx);

            if((instruction.Length == 6 /**dont()*/ && instruction.Index < target.Index) || doing == false) {
                //disregard that instruction
                doing = false;
                line = line.Substring(target.Index+target.Length);
                Console.WriteLine("Disregarding instruction, found dont()");
            }else if(instruction.Length == 4 /**do()*/ && instruction.Index < target.Index) {
                doing = true;
                line = line.Substring(target.Index);
            }
             else if(target.Success) {
                line = line.Substring(target.Index);
                string current = line.Substring(0,target.Length);
                string[] findableDigits = current.Split(",");
                string firstDigit = findableDigits[0].Substring(4);
                string secondDigit = findableDigits[1].Substring(0,findableDigits[1].Length-1);
                sum += int.Parse(firstDigit) * int.Parse(secondDigit);

                line = line.Substring(target.Length);
            } else {
                line = "";
            }
        }

        return sum;
    }
}