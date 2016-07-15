using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Xunit;
using System;

namespace HairSalon
{
  public class AppointmentTest :IDisposable
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

    [Fact]
    public void Test_Save_SavesAppointmentToDatabase()
    {
      DateTime testDate = new DateTime(2016, 7, 6, 12, 30, 0);
      Appointment testAppointment = new Appointment(3, 1, testDate, 60);
      testAppointment.Save();
      List<Appointment> results = Appointment.GetAll();
      List<Appointment> testList = new List<Appointment>{testAppointment};
      Assert.Equal(testList, results);
    }

    [Fact]
    public void Test_Save_AssignsIdToAppointmentObject()
    {
      DateTime testDate = new DateTime(2016, 8, 10, 15, 00, 0);
      Appointment testAppointment = new Appointment(3, 2, testDate, 20);
      testAppointment.Save();
      Appointment savedAppointment = Appointment.GetAll()[0];
      int result = savedAppointment.GetId();
      int testId = testAppointment.GetId();
      Assert.Equal (testId, result);
    }

    [Fact]
    public void Test_Find_FindsAppointmentInDatabase()
    {
      DateTime testDate = new DateTime(2016, 9, 22, 9, 0, 0);
      Appointment testAppointment = new Appointment(5, 2, testDate, 11);
      testAppointment.Save();
      int foundId = testAppointment.GetId();
      Appointment resultAppointment = Appointment.Find(foundId);
      Assert.Equal (testAppointment, resultAppointment);
    }

    [Fact]
    public void Test_Update_UpdatesAppointmentInDatabase()
    {
      DateTime testDate = new DateTime(2016, 10, 20, 18, 0, 0);
      Appointment testAppointment = new Appointment(3, 3, testDate, 45);
      testAppointment.Save();
      DateTime newDate = new DateTime(2016, 10, 22, 9, 0, 0);
      int newStylistId = 7;
      testAppointment.Update(3, newStylistId, newDate, 45);

      DateTime resultDate = testAppointment.GetDate();
      int resultStylistId = testAppointment.GetStylistId();
      Assert.Equal(newDate, resultDate);
      Assert.Equal(newStylistId, resultStylistId);
    }

    [Fact]
    public void Test_Delete_DeletesAppointmentFromDatabase()
    {
      List<Appointment> testAppointments = new List<Appointment>{};

      DateTime testDate1 = new DateTime(2017, 5, 21, 12, 0, 0);
      Appointment testAppointment1 = new Appointment(1, 3, testDate1, 15);
      testAppointment1.Save();
      testAppointments.Add(testAppointment1);

      DateTime testDate2 = new DateTime(2016, 11, 21, 14, 0, 0);
      Appointment testAppointment2 = new Appointment(2, 3, testDate2, 30);
      testAppointment2.Save();
      testAppointments.Add(testAppointment2);

      testAppointment1.Delete();
      testAppointments.Remove(testAppointment1);
      List<Appointment> resultAppointments = Appointment.GetAll();

      Assert.Equal(testAppointments, resultAppointments);
    }

    public void Dispose()
    {
      Appointment.DeleteAll();
    }
  }
}
