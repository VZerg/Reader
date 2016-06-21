using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashReader
{
   public abstract class BaseRepository<T> where T: class
    {
        Context db = new Context();

        public T Create(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public T DeletePost(T entity)
        {
            
            db.SaveChanges();
            return entity;
        }

        //public virtual void DeletPosts(Post[] SomePost)
        //{
        //    for (int i = 0; i < SomePost.Length; i++)
        //        DeletePost(SomePost[i]);
        //}

        //public virtual Post GetPost(int IdPost)
        //{
        //    Context db = new Context();
        //    db.Posts.;
        //}

    }
}
