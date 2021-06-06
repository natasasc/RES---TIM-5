using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public interface IXml
    {
        XmlDocument ReadXML(string path);
        List<XmlNodeList> LoadNodes(XmlDocument xmlDoc, string id, string time);
        List<string> Validate(List<XmlNodeList> lista);
        void Work(List<string> podaci);
        void WriteData(double value);
    }
}
