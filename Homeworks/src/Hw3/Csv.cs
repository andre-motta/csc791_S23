using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Homeworks.src.Hw3
{
    public static class Csv
    {
        public static Tuple<List<string>,List<ArrayList>> Read (string filename)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                string[] names = csvParser.ReadFields();
                List<string> colNames = new List<string>();
                foreach(var name in names)
                {
                    colNames.Add(name);
                }
                List<ArrayList> data = new List<ArrayList>();
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    ArrayList cells = new ArrayList();
                    foreach(var field in fields)
                    {
                        cells.Add(field);
                    }
                    data.Add(cells);
                }
                return new Tuple<List<string>, List<ArrayList>>(colNames, data);
            }

        }
    }
}
