﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PizzaHut.Core.Entities;

public partial class ItemType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}