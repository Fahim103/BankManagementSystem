using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankMgmtSys
{
    public class AccountNumberGenerator
    {
        private static int counter = 1000000;
        public static string GetNewAccountNumber()
        {
            List<String> fileNameWithPath = new List<string>();
            string folderPath = @"";
            string folderName = @"\accounts";
            fileNameWithPath = Directory.GetFiles(folderPath + folderName).OrderBy(Path.GetFileName).ToList();
            List<String> fileName = new List<string>();
            foreach (string file in fileNameWithPath)
            {
                fileName.Add(Path.GetFileName(file));
            }
            if (fileName.Count != 0)
            {
                string lastFileName = fileName.Last();
                string[] fileNameArray = lastFileName.Split('.');
                int.TryParse(fileNameArray[0], out counter);
            }
            counter++;
            return counter.ToString();
        }
    }
}
