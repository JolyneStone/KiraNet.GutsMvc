using KiraNet.GutsMVC.Implement;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using Xunit;

namespace KiraNet.GutsMVC.Test
{
    public class NameValueCollectionValueProviderTest
    {
        //[Fact]
        //public void PrefixTest()
        //{
            //var collection = new NameValueCollection
            //{
            //    { "Person.Name", "zzq" },
            //    { "Person.Address.Port", "6123" },
            //    { "Preson.Address.Local", "China" }
            //};

            //var valueProvider1 = new NameValueCollectionValueProvider(collection);

        //}

        //[Fact]
        //public void PrefixContainer()
        //{
        //    var prefixContainer = new PrefixContainer(new string[] { "Foo.Gay", "Foo.Bars", "Foo.Bar[0]" });

        //    Assert.True(prefixContainer.ContainsPrefix("Foo.Bar"));
        //}

        [Fact]
        public void NameValueCollection()
        {
            var collection = new NameValueCollection
            {
                { "Person.Name", "zzq" },
                { "Person.Address.Port", "6123" },
                { "Preson.Address.Local", "China" }
            };

            collection.Add("Person.Name", "123");

            var vaule1 = collection["Preson.Name"];
            var value2 = collection["Person.Address.Port"];
            Assert.True(collection.GetValues("Person.Name").Length == 2);
            var value3 = collection.GetValues("Person.Name");
        }
    }
}
