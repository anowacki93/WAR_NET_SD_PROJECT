using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Models
{
    public class LogsModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Date{ get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
