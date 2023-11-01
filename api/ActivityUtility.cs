using api.Database;
using api.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace api
{
    public class ActivityUtility
    {
        public List<Activity> GetAllActivities(){
            ConnectionString db = new ConnectionString();
            using var con = new MySqlConnection(db.cs);
            List<Activity> myActivities = new List<Activity>();
            con.Open();
            string stm = "select * from activities;";
            using var cmd = new MySqlCommand(stm, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                // Activity newActivity = new Activity();
                System.Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)}");
                // newActivity.id = rdr.GetInt32(0);
                // newActivity.activityType = rdr.GetString(1);
                myActivities.Add(new Activity(){id = rdr.GetInt32(0), activityType = rdr.GetString(1), distance = rdr.GetString(2), date = rdr.GetString(3), pinned = rdr.GetBoolean(4), deleted = rdr.GetBoolean(5)});
            }
            con.Close();
            return myActivities;
        }

        public void AddActivity(Activity myActivity){
            System.Console.WriteLine(myActivity.activityType);
            ConnectionString db = new ConnectionString();
            using var con = new MySqlConnection(db.cs);
            con.Open();
            string stm = "INSERT INTO activities (activityType, distance, date, pinned, deleted) VALUES (@activityType, @distance, @date, @pinned, @deleted)";
            using var cmd = new MySqlCommand(stm, con);

            //cmd.Parameters.AddWithValue("@id", myActivity.id);
            cmd.Parameters.AddWithValue("@activityType", myActivity.activityType);
            cmd.Parameters.AddWithValue("@distance", myActivity.distance);
            cmd.Parameters.AddWithValue("@date", myActivity.date);
            cmd.Parameters.AddWithValue("@pinned", myActivity.pinned);
            cmd.Parameters.AddWithValue("@deleted", myActivity.deleted);


            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteActivity(int id){
                int activityId = id; // Replace with the ID of the book to update.
                
                bool delete = true;
                // Create the UPDATE SQL statement.
                ConnectionString db = new ConnectionString();
                using var con = new MySqlConnection(db.cs);
                con.Open();
                string stm = "UPDATE activities SET deleted = @delete WHERE id = @id";
                using var cmd = new MySqlCommand(stm, con);

                // Set parameters for the SQL statement.
                cmd.Parameters.AddWithValue("@delete", delete);
                cmd.Parameters.AddWithValue("@id", activityId);

                // Execute the SQL statement to update the book.
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
        }

        public void PinActivity(int id, bool pinned){
                int activityId = id; // Replace with the ID of the book to update.
                
                pinned = !(pinned);
                // Create the UPDATE SQL statement.
                ConnectionString db = new ConnectionString();
                using var con = new MySqlConnection(db.cs);
                con.Open();
                string stm = "UPDATE activities SET pinned = @pin WHERE id = @id";
                using var cmd = new MySqlCommand(stm, con);

                // Set parameters for the SQL statement.
                cmd.Parameters.AddWithValue("@pin", pinned);
                cmd.Parameters.AddWithValue("@id", activityId);

                // Execute the SQL statement to update the book.
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
        }
    }
}