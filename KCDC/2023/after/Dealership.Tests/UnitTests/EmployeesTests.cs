using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.APIs;
using Dealership.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dealership.Tests.UnitTests;

public class EmployeesTests
{
  [Fact]
  public void CanGetEmployees()
  {
    var result = EmployeesApi.GetEmployees(null, new Data.Repo());

    Assert.IsAssignableFrom<Ok<List<Employee>>>(result);

    var value = ((Ok<List<Employee>>)result).Value;

    Assert.NotNull(value);
  }
}
