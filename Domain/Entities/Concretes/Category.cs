using Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Concretes
{
	public class Category : Entity
	{
		public string CategoryName { get; set; }
		public virtual ICollection<Product> Products { get; set; }
	}
}
