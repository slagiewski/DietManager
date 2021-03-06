﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DM.Database;

namespace DM.Repositories.Interfaces
{
    public interface IFavouriteRepository: IBaseRepository<Favourite>
    {
        Task<ICollection<Favourite>> GetUserFavouritesAsync(Guid userId, int index, int takeAmount, string nameFilter = null);
        Task<bool> DeleteAsync(Guid userId, Guid mealId);
        Task<bool> ContainsAsync(Guid userId, Guid mealId);
    }
}