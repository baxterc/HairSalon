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
    public void Test_Update_UpdatesClientNameInDatabase()
    {
      Client testClient = new Client("Rachel", 2);
      testClient.Save();
      string newClientName = "Robin";
      testClient.Update(newClientName);
      string result = testClient.GetName();
      Assert.Equal(newClientName, result);
    }

    [Fact]
    public void Test_Update_UpdatesMultipleItemsInDatabase()
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

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
