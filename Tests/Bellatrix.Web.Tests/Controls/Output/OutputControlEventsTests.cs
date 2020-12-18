﻿// <copyright file="OutputControlEventsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Bellatrix.Web.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [Browser(BrowserType.Edge, BrowserBehavior.ReuseIfStarted)]
    [AllureSuite("Output Control")]
    [AllureFeature("ControlEvents")]
    public class OutputControlEventsTests : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().OutputLocalPage);

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void HoveringCalled_BeforeActuallyHover()
        {
            Output.Hovering += AssertStyleAttributeEmpty;

            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput");

            outputElement.Hover();

            Assert.AreEqual("color: red;", outputElement.GetStyle());

            Output.Hovering -= AssertStyleAttributeEmpty;

            void AssertStyleAttributeEmpty(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void HoveredCalled_AfterHover()
        {
            Output.Hovered += AssertStyleAttributeContainsNewValue;

            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput");

            outputElement.Hover();

            Output.Hovered -= AssertStyleAttributeContainsNewValue;

            void AssertStyleAttributeContainsNewValue(object sender, ElementActionEventArgs args)
            {
                App.ElementCreateService.CreateById<Output>("myOutput").ValidateStyleIs("color: red;");
            }
        }
    }
}