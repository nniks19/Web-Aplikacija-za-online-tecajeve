﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOT.Models.ViewModels
{
    public class StatistikaKorisnikaVM
    {
        public int? brPrijava { get; set; }
        public int? brOcjenaTecaja { get; set; }
        public int? brTecaja { get; set; }
    }
}
