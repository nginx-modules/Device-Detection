/* *********************************************************************
 * This Source Code Form is copyright of 51Degrees Mobile Experts Limited.
 * Copyright 2017 51Degrees Mobile Experts Limited, 5 Charlotte Close,
 * Caversham, Reading, Berkshire, United Kingdom RG4 7BY
 *
 * This Source Code Form is the subject of the following patents and patent
 * applications, owned by 51Degrees Mobile Experts Limited of 5 Charlotte
 * Close, Caversham, Reading, Berkshire, United Kingdom RG4 7BY:
 * European Patent No. 2871816;
 * European Patent Application No. 17184134.9;
 * United States Patent Nos. 9,332,086 and 9,350,823; and
 * United States Patent Application No. 15/686,066.
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0.
 *
 * If a copy of the MPL was not distributed with this file, You can obtain
 * one at http://mozilla.org/MPL/2.0/.
 *
 * This Source Code Form is "Incompatible With Secondary Licenses", as
 * defined by the Mozilla Public License, v. 2.0.
 ********************************************************************** */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Collections.Specialized;

namespace FiftyOne.UnitTests.API.Enterprise
{
    [TestClass]
    public class Trie : TrieBase
    {
        [TestInitialize]
        public void Initialise()
        {
            if (_wrapper == null) { _wrapper = CreateWrapper(); }
        }

        [TestCleanup]
        public void CleanUp()
        {
            Dispose();
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_NullUserAgent()
        {
            using (var result = _wrapper.Match((string)null))
            {
                Console.WriteLine(result.ToString());
            }
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_EmptyUserAgent()
        {
            using (var result = _wrapper.Match(String.Empty))
            {
                Console.WriteLine(result.ToString());
            }
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_LongUserAgent()
        {
            var userAgent = String.Join(" ", UserAgentGenerator.GetEnumerable(10, 10));
            using (var result = _wrapper.Match(userAgent))
            {
                Console.WriteLine(result.ToString());
            }
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_HttpHeaders()
        {
            var headers = new NameValueCollection();
            foreach(var header in _wrapper.HttpHeaders)
            {
                headers.Add(header, UserAgentGenerator.GetRandomUserAgent(0));
            }
            using (var result = _wrapper.Match(headers))
            {
                foreach(var property in _wrapper.AvailableProperties)
                {
                    Console.WriteLine("{0}: {1}", property, result[property]);
                }
            }
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_ProviderMemory()
        {
            string properties = "IsMobile,BrowserName,PlatformName";
            var provider = CreateWrapper(properties, true);
            provider.Dispose();
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_ProviderMemoryEmptyProperties()
        {
            string properties = "";
            var provider = CreateWrapper(properties, true);
            provider.Dispose();
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_DriftFeature()
        {
            using (var provider = CreateWrapper())
            {
                TestDrift(provider);
            }
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_InitEmptyPropertiesString()
        {
            InitEmptyPropertiesStringTest();
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_InitPropertiesString()
        {
            InitPropertiesStringTest();
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_InitEmptyPropertiesArray()
        {
            InitEmptyPropertiesArrayTest();
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_InitPropertiesArray()
        {
            InitPropertiesArrayTest();
        }

        [TestMethod]
        [TestCategory("API"), TestCategory("Trie"), TestCategory("Enterprise")]
        public void EnterpriseTrieAPI_DifferenceFeature()
        {
            using (var provider = CreateWrapper())
            {
                TestDifference(provider);
            }
        }
        
        protected override string DataFile
        {
            get { return Constants.ENTERPRISE_TRIE_V34; }
        }
    }
}
