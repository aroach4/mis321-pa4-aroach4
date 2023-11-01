namespace api.Models
{
    public class Activity
    {
        public int id {get; set;}
        public string activityType {get; set;}
        public string distance {get; set;}
        public string date {get; set;}
        public  bool pinned {get; set;}
        public  bool deleted {get; set;}

    
    }
}