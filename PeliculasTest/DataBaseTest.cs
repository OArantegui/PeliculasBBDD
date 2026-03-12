using AccesoDatos;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    
namespace AplicacionConsola
{
    [TestFixture]
    public class DataBaseTest
    {
        [Test]
        public void Test_ConexionString_DataBase()
        {
            string connStringEsperada = @"Data Source=PC21WTTF\SQLEXPRESS;Initial Catalog=Cine;Integrated Security=True";

            StringAssert.AreEqualIgnoringCase(connStringEsperada, DataBase.GetSqlConnection().ConnectionString);
            //Assert.AreEqual(connStringEsperada, DataBase.GetSqlConnection().ConnectionString);

        }
    }
}