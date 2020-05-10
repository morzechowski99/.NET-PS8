using Microsoft.Extensions.Configuration;
using PS8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;


namespace PS8.DAL
{
    public class ProductXmlDB : IProductDB
    {
        XmlDocument db = new XmlDocument();
        string xmlDB_path;
        public ProductXmlDB(IConfiguration _configuration)
        {
            xmlDB_path = _configuration.GetValue<string>("AppSettings:XmlDB_path");
            db.Load(xmlDB_path);
        }
        public List<Product> List()
        {
            List<Product> productList = new List<Product>();
            XmlNodeList productXmlNodeList = db.SelectNodes("/store/product");

            foreach (XmlNode productXmlNode in productXmlNodeList)
            {
                productList.Add(XmlNodeProduct2Product(productXmlNode));
            }
            return productList;
        }
        public Product Get(int _id)
        {
            
            XmlNode node = XmlNodeProductGet(_id);
            return XmlNodeProduct2Product(node);
        }
        public void Update(Product _product)
        {
            XmlNode node = XmlNodeProductGet(_product.id);
            node["name"].InnerText = _product.name;
            node["price"].InnerText = _product.price.ToString();
            SaveXmlBase();
        }
        public void Delete(int _id)
        {
            XmlNode toDel = db.SelectSingleNode("/store/product[@id=" + _id.ToString() + "]");
            if(toDel != null)
            {
                toDel.ParentNode.RemoveChild(toDel);
                SaveXmlBase();
            }
        }
        public void Add(Product _product)
        {
            _product.id = GetNextID();
            XmlNode node = db.DocumentElement;
            XmlElement element = db.CreateElement("product");
            XmlElement name = db.CreateElement("name");
            XmlElement price = db.CreateElement("price");
            name.InnerText = _product.name;
            price.InnerText = _product.price.ToString();
            XmlAttribute id = db.CreateAttribute("id");
            id.Value = _product.id.ToString();
            element.Attributes.Append(id);
            element.AppendChild(name);
            element.AppendChild(price);
            node.InsertAfter(element, node.LastChild);
            SaveXmlBase();
        }
        private Product XmlNodeProduct2Product(XmlNode node)
        {
            Product p = new Product();
            p.id = int.Parse(node.Attributes["id"].Value);
            p.name = node["name"].InnerText;
            p.price = decimal.Parse(node["price"].InnerText);
            return p;
        }
        private void SaveXmlBase()
        {
            db.Save(xmlDB_path);
        }
        private XmlNode XmlNodeProductGet(int _id)
        {
            XmlNode node = null;
            XmlNodeList list = db.SelectNodes("/store/product[@id=" + _id.ToString() +"]");
            node = list[0];
            return node;
        }
        private int GetNextID()
        {
            List<Product> products = List();
            if (products.Count == 0) return 0;
            return products[products.Count - 1].id + 1;
        }
    }
}
