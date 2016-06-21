using System;
using BashReader.Data;

namespace Entity
{
    class Program
    {
        static void Main(string[] args)
        {
            // PageParser newParse = new PageParser();
            //newParse.ParsePage();
            PostsRepository db = new PostsRepository();
            Post N = new Post();
            N.PostId = 439834;
            Console.Write(db.Get(N.PostId));
            Console.ReadLine();
            

        }
    }
}
