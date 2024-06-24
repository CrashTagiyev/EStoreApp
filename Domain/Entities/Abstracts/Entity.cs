using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Entities.Abstracts
{
	public class Entity
	{
		public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
		public DateTime DeletedTime { get; set; }
		public bool IsDeleted { get; set; }

	
    }
}
