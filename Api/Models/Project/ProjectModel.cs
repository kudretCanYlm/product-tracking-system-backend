using DAS.Model.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.Project
{
    public class ProjectPostModel
    {
        public string Name { get; set; }
        public string ContentText { get; set; }
        public decimal Price { get; set; }
        public MoneyTypesEnum MoneyType { get; set; } = MoneyTypesEnum.TL;
    } 
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContentText { get; set; }
        public decimal Price { get; set; }
        public string MoneyType { get; set; }
    }
}