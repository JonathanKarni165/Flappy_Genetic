using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// generate a report with every generation achivements over time
/// the report is a comma seperated file that can be opend with excel
/// </summary>
public class ExelFormat
{
    public static bool isPathSet = false;
    private static string fileName;
    //private static string fileName = @"C:\Users\user\Desktop\Code\Nets\FlappyData.csv";

    public static void SetPath(string path)
    {
        fileName = path + @"\FlappyData.csv";
    }

    /// <summary>
    /// open new empty csv file
    /// </summary>
    public static void OpenCSVFile()
    {
        TextWriter tw = new StreamWriter(fileName, false);
        tw.WriteLine("Gen, Score, time");
        tw.Close();
    }

    /// <summary>
    /// add new line to existing csv file
    /// </summary>
    /// <param name="genNum"> generation number </param>
    /// <param name="score"> this generation max score </param>
    public static void AddLine(int genNum, int score)
    {
        TextWriter tw = new StreamWriter(fileName, true);
        tw.WriteLine(genNum + "," + score + "," + System.DateTime.Now.ToString());

        tw.Close();
    }

    /// <summary>
    /// add new line to existing csv file
    /// </summary>
    /// <param name="genNum"> generation number </param>
    /// <param name="score"> this generation max score </param>
    /// <param name="speed"> the pipe current speed </param>
    public static void AddLine(int genNum, int score, float speed)
    {
        TextWriter tw = new StreamWriter(fileName, true);
        tw.WriteLine(genNum + "," + score + "," + speed + "," + System.DateTime.Now.ToString());

        tw.Close();
    }
}
