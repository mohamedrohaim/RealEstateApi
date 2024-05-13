using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Comment:BaseEntity<int>
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public User user { get; set; }

        
    }
}
