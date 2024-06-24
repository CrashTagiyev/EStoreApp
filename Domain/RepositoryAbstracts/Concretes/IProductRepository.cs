﻿using Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryAbstracts.Concretes
{
	public interface IProductRepository:IRepository<Product>
	{
		Task<ICollection<Product>> GetAllProductsEagerLoadingAsync();
	}
}
