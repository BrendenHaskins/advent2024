using System;

namespace Solutions;

class DayOne {
    static void Main() {
        var one = new List<int>();
        var two = new List<int>();

        using (StreamReader fileInput = new StreamReader("input1.txt")) {
            string line;
            while(  (line = fileInput.ReadLine()) != null) {
                string[] split = line.Split("   ");
                one.Add(int.Parse(split[0]));
                two.Add(int.Parse(split[1]));
            }
        }
        
        Console.WriteLine(DiffOfTwoInputArrs(one,two));
    }
    
    static int DiffOfTwoInputArrs(List<int> one, List<int> two) {
        one.Sort();
        two.Sort();

        int sum = 0;
        for(int i = 0; i < one.Count; i++) {
            int val =  Math.Abs(one[i] - two[i]);
            sum += val;
        }
        return sum;
    }
}