using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MDArrayExtention
{
    public static void Print2DArray(this string[,] matrix)
    {
        string output = "";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                output+=matrix[i, j] + "\t";
            }
            output += "\n";
        }
        Debug.Log(output);
    }
}
