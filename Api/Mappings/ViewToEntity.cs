using Api.Models.Chat;
using AutoMapper;
using DAS.Model.Model.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Mappings
{
    public class ViewToEntity:Profile
    {
        public override string ProfileName => "ViewToEntityMap";

        protected override void Configure()
        {
            
        }
    }
}