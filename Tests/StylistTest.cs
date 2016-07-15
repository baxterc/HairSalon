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

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
