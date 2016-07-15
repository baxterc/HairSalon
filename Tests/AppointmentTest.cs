using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Xunit;
using System;


namespace HairSalon
{
  public class AppointmentTest
  {
    public AppointmentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_GetAll_ClientTableEmpty()
    {
      int result = Appointment.GetAll().Count;
      Assert.Equal(0, result);
    }

  }
}
