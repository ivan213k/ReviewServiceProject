﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReviewService.Shared.ApiModels
{
    public class ImportanceLevelApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
