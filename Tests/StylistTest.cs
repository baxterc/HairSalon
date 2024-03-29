using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_GetAll_StylistTableEmpty()
    {
      int result = Stylist.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ForSameStylistInfo()
    {
      Stylist firstStylist = new Stylist("Pat");
      Stylist secondStylist = new Stylist("Pat");
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Test_Save_SavesStylistToDatabase()
    {
      Stylist testStylist = new Stylist("Pat");
      testStylist.Save();
      List<Stylist> results = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};
      Assert.Equal(testList, results);
    }

    [Fact]
    public void Test_Save_AssignsIdToStylistObject()
    {
      Stylist testStylist = new Stylist("Pat");
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.GetId();
      int testId = testStylist.GetId();
      Assert.Equal (testId, result);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      Stylist testStylist = new Stylist("Libby");
      testStylist.Save();
      int foundId = testStylist.GetId();
      Stylist resultStylist = Stylist.Find(foundId);
      Assert.Equal (testStylist, resultStylist);
    }

    [Fact]
    public void Test_Update_UpdatesStylistInDatabase()
    {
      Stylist testStylist = new Stylist("Mike");
      testStylist.Save();
      string newStylistName = "Donna";
      testStylist.Update(newStylistName);
      string result = testStylist.GetName();
      Assert.Equal(newStylistName, result);
    }

    [Fact]
    public void Test_Delete_DeletesStylistFromDatabase()
    {
      List<Stylist> testStylists = new List<Stylist>{};

      Stylist testStylist1 = new Stylist("Nora");
      testStylist1.Save();
      testStylists.Add(testStylist1);

      Stylist testStylist2 = new Stylist("Jimmy");
      testStylist2.Save();
      testStylists.Add(testStylist2);

      testStylist1.Delete();
      testStylists.Remove(testStylist1);
      List<Stylist> resultStylists = Stylist.GetAll();

      Assert.Equal(testStylists, resultStylists);
    }

    [Fact]
    public void Test_GetClients_GetsAllClientsAssociatedWithStylist()
    {
      List<Client> testClients = new List<Client>{};

      Stylist testStylist = new Stylist("Polly");
      testStylist.Save();

      Client testClient1 = new Client("Pete", testStylist.GetId());
      testClient1.Save();
      testClients.Add(testClient1);

      Client testClient2 = new Client("Prasad", testStylist.GetId());
      testClient2.Save();
      testClients.Add(testClient2);

      List<Client> resultClients = testStylist.GetClients();

      Assert.Equal(testClients, resultClients);
    }

    [Fact]
    public void Test_GetAppointments_GetsAllAppointmentsAssociatedWithStylist()
    {
      Appointment.DeleteAll();
      List<Appointment> testAppointments = new List<Appointment>{};

      Stylist testStylist = new Stylist("Jethro");
      testStylist.Save();
      int testStylistId = testStylist.GetId();

      DateTime testDate1 = new DateTime(2017, 5, 21, 12, 0, 0);
      Appointment testAppointment1 = new Appointment(1, testStylistId, testDate1, 15);
      testAppointment1.Save();
      testAppointments.Add(testAppointment1);

      DateTime testDate2 = new DateTime(2016, 11, 21, 14, 0, 0);
      Appointment testAppointment2 = new Appointment(1, testStylistId, testDate2, 30);
      testAppointment2.Save();
      testAppointments.Add(testAppointment2);

      List<Appointment> resultAppointments = testStylist.GetAppointments();

      Assert.Equal(testAppointments, resultAppointments);
    }


    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
