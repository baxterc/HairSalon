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
    public void Test_GetAll_AppointmentTableEmpty()
    {
      int result = Appointment.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ForSameAppointmentInfo()
    {
      DateTime testDate = new DateTime(2016, 7, 6, 12, 30, 0);
      Appointment firstAppointment = new Appointment(1, 2, testDate, 30);
      Appointment secondAppointment = new Appointment(1, 2, testDate, 30);
      Assert.Equal(firstAppointment, secondAppointment);
    }
  }
}
