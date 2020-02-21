using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigSolutionTest2

{


    [TestClass]
    public class UnitTestLanguages
    {
        [TestMethod]
        public void TestLanguageCountDictionary()
        {
            System.Collections.Generic.List<string> actual1 = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<string> expected1 = new System.Collections.Generic.List<string>();

            foreach (System.Globalization.CultureInfo culture in ConfigSolution.App.Languages)
            {
                expected1.Add(culture.Name);
                actual1.Add(ConfigSolution.LanguageSwith.FindDictionary(culture.Name));
            }
            CollectionAssert.AreEqual(expected1, actual1, "LanguageSwith.TemplateMethod not have dictionary for new language");
        }
        
    }

    [TestClass]
    public class TestKeepers
    {
        [TestMethod]
        public void TestKepersList()
        {
           
           
        }
    }
}
