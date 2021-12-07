using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LineFileHelper
{
    public static IEnumerable<string> Load(string path)
    {
        return File.ReadLines(path);
    }

    public static void Save(string path, IEnumerable<string> lines)
    {
        string dir = Path.GetDirectoryName(path);
        Directory.CreateDirectory(dir);
        File.WriteAllLines(path, lines);
    }
}
