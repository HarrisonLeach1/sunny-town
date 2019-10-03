using System.Collections;
using System.Collections.Generic;

public static class Shuffle
{
    private static System.Random random = new System.Random();  

    /*
     * A randomize method based on the Fisher-Yates shuffle.
     * Reference: https://stackoverflow.com/questions/273313/randomize-a-listt
     */
    public static void Randomize<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = random.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}