using BoBo.Formatting.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace BoBo.XML.Tests
{
    public class XMLFootprintTest
    {
        [Fact]
        public void XmlFootprint_ShouldReturnOutput()
        {
            var footprint = new RecursiveDump(
                "Exception",
                new BasicDump("Footprint")
            );
            XmlNode xmlNode = footprint.MakeDump(new Exception("not empty"), new XmlDocument());
            Assert.NotNull(xmlNode.ToString());
        }
        
        [Fact]
        public void XmlFootprint_ShouldReturnOutput_InnerException()
        {
            var footprint = new RecursiveDump(
                "Exception",
                new BasicDump("Footprint")
            );
            XmlNode xmlNode = footprint.MakeDump(
                new Exception("not empty", new ArgumentException("argument wrong")), new XmlDocument());
            Assert.NotNull(xmlNode.ToString());
        }
        
        [Fact]
        public void XmlFootprint_ShouldReturnOutput_InnerException_TryCatch()
        {
            try
            {
                throw new Exception("not empty", new ArgumentException("argument wrong"));
            }
            catch (Exception exception)
            {
                var footprint = new RecursiveDump(
                    "Exception",
                    new XmlDump()
                );
                XmlNode xmlNode = footprint.MakeDump(exception, new XmlDocument());
                Assert.NotNull(xmlNode.ToString());
            }
        }
    }
}
