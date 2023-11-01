namespace api.Database
{
    public class ConnectionString
    {
        public string cs {get; set;}

        public ConnectionString(){
            string server = "w3epjhex7h2ccjxx.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "l80i9ehbgpyn1bwq";
            string port = "3306";
            string userName = "bfip32q6i6wnt40i";
            string password = "o3aawtf4jo6n5bup";

            cs = $@"server = {server}; user ={userName}; database = {database}; port = {port}; password = {password};";
        }
    }
}