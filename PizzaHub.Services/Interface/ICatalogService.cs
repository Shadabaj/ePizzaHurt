﻿using PizzaHub.Services.Interface;
using PizzaHut.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHurt.Services.Interface
{
   public interface ICatalogService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<ItemType> GetItemTypes();
        IEnumerable<Item> GetItems();
        Item GetItem(int id);
        void AddItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int id);
    }
}
