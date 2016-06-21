using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashReader
{
    class Program
    {
        static void Main(string[] args)
        {
           // PageParser newParse = new PageParser();
            //newParse.ParsePage();
            Context db = new Context();
            Post[] newPost = new Post[(db.Posts.Count() - 1)];
            //newPost = db.Postsj;
            //db.Posts.Attach(newPost);
            //db.Posts.Remove(newPost);
            db.SaveChanges();

        }
    }
}
