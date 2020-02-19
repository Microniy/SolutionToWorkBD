using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigSolutionTest
{
    [TestClass]
    public class UnitTestLanguages
    {
        [TestMethod]
        public void TestMethod1()
        {
            System.Collections.Generic.List<string> actual = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<string> expected = new System.Collections.Generic.List<string>();
            
                for (int i = 0; i < ConfigSolution.App.Languages.Count; i++)
                {
                    string Name = ConfigSolution.App.Languages[i].Name;
                    actual.Add(Name);
                    expected.Add(ConfigSolution.LanguageSwith.FindDictionary(Name));
                }
           
          /* foreach (string culture in ConfigSolution.App.Languages.Name)
            {
                actual.Add(culture.Name);
                expected.Add(ConfigSolution.LanguageSwith.FindDictionary(culture.Name));
            }*/
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
