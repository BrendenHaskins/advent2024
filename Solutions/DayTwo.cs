using System;

namespace Solutions;

class DayTwo {
    public static void Run() {
        int safeCount = 0;

        using (StreamReader fileInput = new StreamReader("input2.txt")) {
            string line;
            while(  (line = fileInput.ReadLine()) != null) {
                var list = new List<int>();
                string[] split = line.Split(" ");
                foreach(string num in split) {
                    list.Add(int.Parse(num));
                }
                if(LevelIsSafe(list)) {
                    safeCount++;
                }
            }
        }

        Console.WriteLine(safeCount);

    }

    static bool LevelIsSafe(List<int> level) {
        int isIncreasing;
        if(level[0] < level[1]) {
            isIncreasing = 3;
        } else if(level[0] > level[1]) {
            isIncreasing = -3;
        } else {
            return false;
        }

        for(int i = 0; i < level.Count-1; i++) {
            int diff = level[i+1] - level[i];

            //if the difference is greater than three, it increased/decreased too much
            if(Math.Abs(diff) > 3) {
                return false;
            }
            
            //if the difference did not bring isIncreasing further away from zero, it failed to increase/decrease
            if(!(Math.Abs(diff+isIncreasing) > Math.Abs(isIncreasing))) {
                return false;
            }
        }

        return true;
    }
}