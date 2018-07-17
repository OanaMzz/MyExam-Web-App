using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyExams.Repository.Mappings;
using MyExams.Mappings1;


namespace MyExams.App_Start
{
    public class Bootstrap
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg=>
            {
                cfg.AddProfile(new DataToDto());
                cfg.AddProfile(new DtoToData());
                cfg.AddProfile(new DtoToVM());
                cfg.AddProfile(new VMToDto());
            }
            );
        }
    }
}