using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Models;

namespace Timesheet.DataContext.MongoDb
{
    public class Class1
    {
        public void Test()
        {

            var client = new MongoClient("");
            var db = client.GetDatabase("");

             var ss = db.GetCollection<CountryHolidayModel>("");

            //ss
        }
    }
}
