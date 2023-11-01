using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Database;
using api;
namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        // GET: api/Activity
        [HttpGet]
       public List<Activity> Get()
        {
            ActivityUtility utility = new ActivityUtility();
            return utility.GetAllActivities();
        }
        // GET: api/Activity/5
        [HttpGet("{id}", Name = "Get")]
        public Activity Get(int id)
        {
            ActivityUtility utility = new ActivityUtility();
            List<Activity> myActivities = utility.GetAllActivities();
            foreach(Activity activity in myActivities){
                if(activity.id == id){
                    return activity;
                }
            } 
            return new Activity();
        }

        // POST: api/Activity
        [HttpPost]
        public void Post([FromBody] Activity myActivity)
        {
            
            ActivityUtility utility = new ActivityUtility();
            utility.AddActivity(myActivity);
            System.Console.WriteLine(myActivity);
        }

        // PUT: api/Activity/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] bool pinned)
        {
            ActivityUtility utility = new ActivityUtility();
            utility.PinActivity(id, pinned);
        }

        // DELETE: api/Activity/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ActivityUtility utility = new ActivityUtility();
            utility.DeleteActivity(id);
        }
    }
}
