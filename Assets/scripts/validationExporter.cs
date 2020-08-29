using System.IO;
using UnityEngine;

public class validationExporter
{
    private static string reportDirectoryName = "Validation_Report";
    private static string reportFileName = "valreport.csv";
    private static string reportSeperator = ",";
    private static string[] reportHeaders = new string[]
    {
        "Sensor Name", //0
        "Starting Time - Game Clock", // 1
        "Ending Time - Game Clock", //2
        "Starting Time - Real Clock", //3
        "Ending Time - Real Clock"//4
    };

    public static void AppendToReport(string[] strings)
    {
        VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = new StreamWriter(GetFilePath(), true))
        {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeperator;
                }
                finalString += strings[i];
            }
            sw.WriteLine(finalString);
        }
    }

    public static void CreateReport()
    {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < reportHeaders.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeperator;
                }
                finalString += reportHeaders[i];
            }
            sw.WriteLine(finalString);
        }
    }

    static void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);

        }
    }

    static void VerifyFile()
    {

        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateReport();
        }
    }

    static string GetDirectoryPath()
    {
        return Application.dataPath + '/' + reportDirectoryName;
    }

    static string GetFilePath()
    {
        return GetDirectoryPath() + '/' + reportFileName;
    }
}
