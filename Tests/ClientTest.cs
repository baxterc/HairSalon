using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_GetAll_ClientTableEmpty()
    {
      int result = Client.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ForSameClientInfo()
    {
      Client firstClient = new Client("Joe", 1);
      Client secondClient = new Client("Joe", 1);
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_Save_SavesClientToDatabase()
    {
      Client testClient = new Client("Lana", 2);
      testClient.Save();
      List<Client> results = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};
      Assert.Equal(testList, results);
    }

    [Fact]
    public void Test_Save_AssignsIdToClientObject()
    {
      Client testClient = new Client("Betty", 1);
      testClient.Save();
      Client savedClient = Client.GetAll()[0];
      int result = savedClient.GetId();
      int testId = testClient.GetId();
      Assert.Equal (testId, result);
    }

    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      Client testClient = new Client("Pete", 3);
      testClient.Save();
      int foundId = testClient.GetId();
      Client resultClient = Client.Find(foundId);
      Assert.Equal (testClient, resultClient);
    }

    [Fact]
    public void Test_Update_UpdatesMultipleClientPropertiesInDatabase()
    {
      Client testClient = new Client("Heather", 2);
      testClient.Save();
      string newClientName = "Tricina";
      int newStylistId = 3;
      testClient.Update(newClientName, newStylistId);

      string resultName = testClient.GetName();
      int resultStylistId = testClient.GetStylistId();
      Assert.Equal(newClientName, resultName);
      Assert.Equal(newStylistId, resultStylistId);
    }

    [Fact]
    public void Test_Delete_DeletesClientFromDatabase()
    {
      List<Client> testClients = new List<Client>{};

      Client testClient1 = new Client("Borkvard", 3);
      testClient1.Save();
      testClients.Add(testClient1);

      Client testClient2 = new Client("Ida", 2);
      testClient2.Save();
      testClients.Add(testClient2);

      testClient1.Delete();
      testClients.Remove(testClient1);
      List<Client> resultClients = Client.GetAll();

      Assert.Equal(testClients, resultClients);
    }

    [Fact]
    public void Test_GetAppointments_GetsAllAppointmentsAssociatedWithClient()
    {
      Appointment.DeleteAll();
      List<Appointment> testAppointments = new List<Appointment>{};

      Client testClient = new Client("Polly", 3);
      testClient.Save();
      int testClientId = testClient.GetId();
      int testClientStylistId = testClient.GetStylistId();


      DateTime testDate1 = new DateTime(2017, 5, 21, 12, 0, 0);
      Appointment testAppointment1 = new Appointment(testClientId, testClientStylistId, testDate1, 15);
      testAppointment1.Save();
      testAppointments.Add(testAppointment1);

      DateTime testDate2 = new DateTime(2016, 11, 21, 14, 0, 0);
      Appointment testAppointment2 = new Appointment(testClientId, testClientStylistId, testDate2, 30);
      testAppointment2.Save();
      testAppointments.Add(testAppointment2);

      List<Appointment> resultAppointments = testClient.GetAppointments();

      Assert.Equal(testAppointments, resultAppointments);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
