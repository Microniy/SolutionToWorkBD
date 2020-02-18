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
            foreach (System.Globalization.CultureInfo culture in ConfigSolution.App.Languages)
            {
                actual.Add(culture.Name);
                //expected.Add()
            }
        }
    }
}
