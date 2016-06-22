using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using Entity;
using BashReader.Data;

namespace Entity
{
    class PageParser
    { 
        PostsRepository repository = new PostsRepository();
        /// <summary>
        ///
        /// </summary>
        /// <param name="pageNamberStart">Page start</param>
        /// <param name="pageNaberEnd">Page end</param>
        public void ParsePage()
        {
            WebClient client = new WebClient();
            for (int index = LastPage(); index >= (LastPage() - 10); index--)
            {
                try
                {
                    GetPost(client.DownloadString(ConfigurationSettings.AppSettings["Bash"].ToString() + index));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                Console.WriteLine(index.ToString());
            }
            
            foreach (var reader in repository.GetAll())
            {
                Console.WriteLine(String.Format("Индивидуальный индифекатор записи = {0}," + '\n'
                                                + " Номер поста = {1}, Рэйтинг = {2}, Дата публикации = {5},"
                                                + '\n' + " {3}," + '\n' + " {4}" + '\n',
                                                reader.Id, reader.PostId, reader.Rating, reader.PostName, reader.PostText, reader.PublishDate));
            }
            //newConnect.ReadAllItemsFromDB();
            // newConnect.DeletAllItemsFromDB();
            Console.ReadKey();
        }

        private int LastPage()
        {
            WebClient client = new WebClient();
            string startPage = client.DownloadString(ConfigurationSettings.AppSettings["Page"].ToString());
            return (Convert.ToInt32(startPage.Remove(startPage.IndexOf("value",startPage.IndexOf("max=")) - 2).Remove(0, startPage.IndexOf("max=") + 5)));
        }

        /// <summary>
        /// Returne true if Web page is exist.
        /// </summary>
        /// <param name="url">URL web page</param>
        /// <returns></returns>
        private bool CheckURL(string url)
        {
            if (String.IsNullOrEmpty(url))
                return false;

            WebRequest request = WebRequest.Create(url);
            try
            {
                HttpWebResponse res = request.GetResponse() as HttpWebResponse;

                if (res.StatusDescription == "OK")
                    return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        private void GetPost(string page)
        {
            int trigger = 0;
            int indexAction = 0;
            int index = 0;
            for (;;)
            { 
                switch (trigger)
                {
                    case 0:
                        if (page.IndexOf("actions", index) != -1)
                        {
                            index = page.IndexOf("actions", index);
                            indexAction = page.IndexOf("rating" + '"', index) + 8;
                            trigger++;
                        }
                        else
                            return;
                        break;
                    case 1: 
                        index = page.IndexOf("</div>", index + 6);
                        trigger++;
                        break;
                    case 2:
                        index = page.IndexOf("</div>", index);
                        ParsePost(page.Remove(page.IndexOf("</div>", index + 6)).Remove(0, indexAction));
                        trigger = 0;
                        break;
                }
            }
        }

        private void ParsePost(string post)
        {
            Post postDevid = new Post();
            try
            {
                if ((Convert.ToInt32(post.Remove(post.IndexOf("<")))) >= 1000)
                {
                    string symbol = "/\\?.,':;[]{}@<>-&*%$@^!";
                    postDevid.Rating = Convert.ToInt32(post.Remove(post.IndexOf("<")));
                    postDevid.PostId = Convert.ToInt32(post.Remove(post.IndexOf("'", post.IndexOf("#"))).Remove(0, (post.IndexOf("#") + 1)));
                    postDevid.PublishDate = DateTime.Parse(post.Remove(post.IndexOf("<", post.IndexOf("date"))).Remove(0, (post.IndexOf("date") + 6)));
                    postDevid.PostText = post.Remove(0, (post.IndexOf("text") + 6));
                    if ((post.Remove(0, (post.IndexOf("text") + 6)).Remove(4) == "xxx:") || 
                        (post.Remove(0, (post.IndexOf("text") + 6)).Remove(4) == "ххх:"))
                    {
                        string name = post.Remove(0, (post.IndexOf("text") + 10));
                        postDevid.PostName = name.Remove(name.IndexOfAny(symbol.ToCharArray()));
                    }
                    else
                    {
                        string name = post.Remove(0, (post.IndexOf("text") + 6));
                        postDevid.PostName = name.Remove(name.IndexOfAny(symbol.ToCharArray()));
                    }
                    repository.Create(postDevid);
                }
            }
            catch
            {
            }
        }

    }
}
