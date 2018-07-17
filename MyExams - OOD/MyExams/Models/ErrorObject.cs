using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExams.Models
{
    public class ErrorObject
    {
        public string Message { get; set; }

        public string Related { get; set; }

        public string Controller { get; set; }


        public ErrorObject(string entity, string relatedEntity, string controller) 
        {
            Message = entity + " cannot be deleted because of related " + relatedEntity + ". Delete " + relatedEntity + " first.";
            Related = "Go to " + relatedEntity;
            Controller = controller;
        }
    }
}