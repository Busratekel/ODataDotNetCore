using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System;
using System.Linq;


public class MyModelsController : ODataController
{
    [HttpGet]
    public IEnumerable<MyModel> Get()
    {
        return new List<MyModel>
        {
            new MyModel { Id = 1, Name = "Model1" },
            new MyModel { Id = 2, Name = "Model2" }
        };
    }
}