using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.Desktop.Tests;

[TestFixture]
[App(@"C:\Program Files\Autodesk\Revit 2021\Revit.exe", Lifecycle.RestartEveryTime)]

public class RevitTest : NUnit.DesktopTest
{

    [OneTimeSetUp]
    public void AssemblyInitialize()
    {
        App.StartWinAppDriver();
    }

    [OneTimeTearDown]
    public void AssemblyCleanUp()
    {
        var app = ServicesCollection.Current.Resolve<App>();
        app?.Dispose();
        App.StopWinAppDriver();
    }

    [Test]
    public void LaunchRevit()
    {

    }
}
