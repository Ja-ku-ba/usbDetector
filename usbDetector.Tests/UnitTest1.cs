using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using usbDetector;

namespace UsbDetector.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetLeterFromMask_ReturnsChar()
        {
            // Arange
            var form = new Form1();

            // Act
            var mask8 = form.GetLetter(8);
            var mask16 = form.GetLetter(16);
            
            //Assert
            Assert.AreEqual(mask8, 'D');
            Assert.AreEqual(mask16, 'E');
        }
    }
}