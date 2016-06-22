using System;
using BashReader.Data;

namespace Entity
{
    class Program
    {
        static void Main(string[] args)
        {
             PageParser newParse = new PageParser();
             newParse.ParsePage();
             Console.WriteLine();
            

        }
    }
}
