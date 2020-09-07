﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Repositories.Questionary
{
    public interface IObjectPropertiesRepository
    {
        public Task<ObjectProperty> GetAsync(int id);
        public Task<List<ObjectProperty>> GetAllAsync();
        public Task<ObjectProperty> CreateAsync(ObjectProperty obj);
        public Task<ObjectProperty> UpdateAsync(ObjectProperty obj);
    }
}