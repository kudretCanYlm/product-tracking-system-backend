using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Api.Mappings;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using DAS.Core.Infrastructure;
using DAS.Core.Repository.Location;
using DAS.Model.Model.Authentication;
using DAS.Service.Services.Location;
using FluentValidation;

namespace Api.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutoMapperConfiguration();
            SetAutofacContainer();
        }
        private static void AutoMapperConfiguration()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToView>();
                x.AddProfile<ViewToEntity>();
            });
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerApiRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerApiRequest();

            builder.RegisterAssemblyTypes(typeof(CountryRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerApiRequest();

            builder.RegisterAssemblyTypes(typeof(CountryService).Assembly)
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces().InstancePerApiRequest();

            builder.RegisterAssemblyTypes(typeof(LoginValidation).Assembly)
            .Where(t => t.Name.EndsWith("Validation"))
            .AsImplementedInterfaces().InstancePerApiRequest();

            
            //will add after owin
            /*builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
         .Where(t => t.Name.EndsWith("Authentication"))
         .AsImplementedInterfaces().InstancePerHttpRequest();

            builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>( new SocialGoalEntities())))
                .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();*/

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            IContainer container = builder.Build();
            
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //https://c-sharpcorner.com/article/using-autofac-with-web-api/ düzelt
        }
    }
}