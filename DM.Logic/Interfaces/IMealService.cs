﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DM.Models.ViewModels;
using DM.Models.Wrappers;

namespace DM.Logic.Interfaces
{
    public interface IMealService
    {
        Task<Guid> AddMealAsync(MealCreationVM mealVM, Guid userId);
        Task<MealVM> GetMealByIdAsync(Guid userId, Guid mealId);
        Task<IndexedResult<IEnumerable<MealPreviewVM>>> GetUsersMealsPreviewsAsync(Guid userId, IndexedResult<MealPreviewVM> lastReturned, int takeAmount = Constants.DEFAULT_DB_TAKE_VALUE);
    }
}