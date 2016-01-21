using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;



namespace SQLI_CrossAR.CrossAR.DataAccess.DAO 
{
    /// <summary>
    /// this class is used to transfert all type of object from database to xml file or vice versa.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DAOTransfert<T> where T : class
    {


        //THIS IS A TEST BY FRANCOIS 

        //roland
//        public T TransfertDataXMLToDB(string filePath)
//        {
//            T myObject;
//            // Create an instance of the XmlSerializer depending on the plateform
//#if WINDOWS_PHONE || WINRT

//            using(FileStream fs = new FileStream(filePath, FileMode.Open))
//            {
//                XmlSerializer serializer = new XmlSerializer(typeof(T));
//                XmlReader reader = XmlReader.Create(fs);
//                myObject = (T)serializer.Deserialize(reader);
//            }

//#endif
//#if __IOS__ || __ANDROID__

//            var serializer = new SharpSerializer();
//            myObject = (T)serializer.Deserialize(filePath);

//#endif

//            return myObject;
//        }

//        public void TransfertDataDBToXML(T myObject, string filePath)
//        {
//            // Create an instance of the XmlSerializer depending on the plateform
//#if WINDOWS_PHONE || WINRT

//            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
//            {
//                XmlSerializer serializer = new XmlSerializer(typeof(T));
//                serializer.Serialize(fs, myObject);
//            }

//#endif
//#if __IOS__ || __ANDROID__

//            var serializer = new SharpSerializer();
//            serializer.Serialize(myObject, filePath);
//#endif
//        }


    }
}
