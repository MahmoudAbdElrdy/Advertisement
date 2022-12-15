using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Common.Extensions;
using System.Collections.Generic;
using Common.Infrastructures;
using Common_Test.TestHelpers;
using Newtonsoft.Json;
using Common.Interfaces;
using System.Linq;

namespace Common_Test {
  [TestClass]
  public class TestExtentionsMethods {

    [TestMethod]
    public void Test_Get_Attribute() {
      List<string> desc = new List<string>();
      foreach (TestAttributes item in Enum.GetValues(typeof(TestAttributes))) {
        desc.Add(item.GetAttribute<DescriptionEnumTestAttribute>()?.Desc ?? "");
      }
      Assert.AreEqual("explain it,2", string.Join(',', desc));
    }

    [TestMethod]
    public void Test_Localized_Data() {
      var localized = new LocalizedData();
      Assert.AreEqual(JsonConvert.SerializeObject(new { en = "",ar="" }), localized.ToString());
      

      LanguagesModel.AddLanguages(new[] { "fr" });
      localized = new LocalizedData("", "", "");
      Assert.AreEqual(JsonConvert.SerializeObject(new { en = "", ar = "", fr = "" }), localized.ToString());

      localized = new LocalizedData("{'ar':'Mohamed','en':'Moha'}");
      Assert.AreEqual(JsonConvert.SerializeObject(new { en = "Moha", ar = "Mohamed", fr = "" }), localized.ToString());
    }
    [TestMethod]
    public void Test_Delete_Protect() {
      var testList = new List<TestDelete>() {
      new TestDelete(){IsDeleted=true},
      new TestDelete(){IsDeleted=false},
    };
      var result = testList.WhereProtected(a => a.DeletedBy == "");
    }


    public class TestDelete  {
      public bool IsDeleted { get; set; }
      public string DeletedBy { get ; set ; }
      public DateTime? DeletedDate { get ; set ; }
    }
  }
}
