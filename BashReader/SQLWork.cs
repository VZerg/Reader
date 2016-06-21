using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BashReader.Data;

namespace Entity
{
    class SQLWork
    {

        public void AddItemToDB(Post newModel)
        {
            
            SqlConnection conectToDB = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connect"].ToString());
            try
            {
                SqlCommand insertToDB = new SqlCommand();
                insertToDB.Connection = conectToDB;
                insertToDB.CommandType = System.Data.CommandType.Text;
                insertToDB.CommandText = "INSERT into BashPost(PostID, Rating, PostName, PostText, PublishDate) VALUES(@PostID, @Rating, @PostName, @PostText, @PublishDate)";
                insertToDB.Parameters.AddWithValue("@PostID", newModel.PostId);
                insertToDB.Parameters.AddWithValue("@Rating", newModel.Rating);
                insertToDB.Parameters.AddWithValue("@PostName", newModel.PostName);
                insertToDB.Parameters.AddWithValue("@PostText", newModel.PostText);
                insertToDB.Parameters.AddWithValue("@PublishDate", newModel.PublishDate);
                try
                {
                    conectToDB.Open();
                    insertToDB.ExecuteNonQuery();
                }
                catch(SqlException e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    conectToDB.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            } 

        }

        public void ReadAllItemsFromDB()
        {
            SqlConnection conectToDB = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connect"].ToString());
            conectToDB.Open();
            SqlCommand command = new SqlCommand("select*from BashPost;", conectToDB);
            // int result = command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("Индивидуальный индифекатор записи = {0},"+'\n'
                                                +" Номер поста = {1}, Рэйтинг = {2}, Дата публикации = {5},"
                                                +'\n'+" {3},"+'\n'+" {4}" + '\n', 
                                                reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
            }
            conectToDB.Close();
        }

        public void DeletAllItemsFromDB()
        {
            SqlConnection conectToDB = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connect"].ToString());
            conectToDB.Open();
            string sql = @"DELETE FROM BashPost;";
            SqlCommand command = new SqlCommand(sql, conectToDB);
            command.ExecuteNonQuery();
            conectToDB.Close();
        }

        public bool UppDateAllItemsFromDB(Post newModel)
        {
            SqlConnection conectToDB = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connect"].ToString());
            conectToDB.Open();
            string rating;
            SqlCommand command = new SqlCommand("select Rating from BashPost where PostID="+ newModel.PostId + ";", conectToDB);
            try
            {
                rating = command.ExecuteScalar().ToString();
            }
            catch
            {
                conectToDB.Close();
                return true;
            }
            if (Convert.ToInt32(rating) != newModel.Rating)


            {
                command = new SqlCommand("UPDATE BashPost SET Rating = " + newModel.Rating + " WHERE PostID = " + newModel.PostId + "; ", conectToDB);
                conectToDB.Close();
                return false;
            }
            conectToDB.Close();
            
            return true;
        }

    }
}
