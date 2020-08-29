using System.IO;
using UnityEngine;

public static class CSVExporter
{
    private static string reportDirectoryName = "Report";
    private static string reportFileName = "report.csv";
    private static string reportSeperator = ",";
    private static string[] reportHeaders = new string[]
    {
        "Sensor Name",
        "Action",
        "Time Stamp - Game clock"
    };
    private static string timeStampHeader = "Time Stamp - Real clock";

    public static void AppendToReport(string[] strings)
    {
        VerifyDirectory();
        VerifyFile();
        using(StreamWriter sw = new StreamWriter(GetFilePath(),true))
        {
            string finalString = "";
            for(int i = 0; i < strings.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeperator;
                }
                finalString += strings[i];
            }
            finalString += reportSeperator + GetTimeStamp();
            sw.WriteLine(finalString);
        }
    }

    public static void CreateReport()
    {
        VerifyDirectory();
        using(StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";
            for(int i = 0; i < reportHeaders.Length; i++)
            {
                if(finalString!="")
                {
                    finalString += reportSeperator;
                }
                finalString += reportHeaders[i];
            }
            finalString += reportSeperator + timeStampHeader;
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

    static string GetTimeStamp()
    {
        return System.DateTime.Now.ToString();
    }
}
