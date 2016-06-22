using System;
using BashReader.Data;

namespace Entity
{
    class Program
    {
        static void Main(string[] args)
        {
            //PostsRepository repository = new PostsRepository();
            //Post n = new Post();
            //n.PostId = 439834;
            //repository.Create(n);
            ////repository.Delete();
            PageParser newParse = new PageParser();
            newParse.ParsePage();
        }
    }
}
