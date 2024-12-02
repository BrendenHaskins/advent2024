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
        Console.WriteLine(SimilarityScores(one,two));
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

    static int SimilarityScores(List<int> one, List<int> two) {
        Dictionary<int, int> freq = new Dictionary<int, int>();
        int sum = 0;

        for(int i = 0; i < two.Count; i++) {
            int curr = two[i];
            if(freq.ContainsKey(curr)) {
                freq[curr] = freq[curr] + 1;
            } else {
                freq[curr] = 1;
            }
        }

        for(int i = 0; i < one.Count; i++) {
            int curr = one[i];
            if(freq.ContainsKey(curr)) {
                sum += curr * freq[curr];
            }
        }

        return sum;
    }
}