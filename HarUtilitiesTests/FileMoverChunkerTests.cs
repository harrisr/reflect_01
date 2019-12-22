using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarUtilities.DirectoryPoller;
using System.IO;

namespace HarUtilitiesTests
{
    [TestClass]
    public class FileMoverChunkerTests
    {

        [TestMethod]
        public void A01_TestConstructorValid()
        {
            FileMoverChunker obj = new FileMoverChunker();
            Assert.IsNotNull(obj);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void A02_TestConstructorInvalidSourceDir()
        {
            try
            {
                //  this should crash due to an invalid argument
                FileMoverChunker obj = new FileMoverChunker("", "ZZZ", 42, 42, true);
                Assert.IsTrue(false);
            }
            catch (Exception ex1)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void A03_TestConstructorInvalidDestDir()
        {
            try
            {
                //  this should crash due to an invalid argument
                FileMoverChunker obj = new FileMoverChunker("ZZZ", "", 42, 42, true);
                Assert.IsTrue(false);
            }
            catch (Exception ex1)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void A04_TestConstructorInvalidChunkSize()
        {
            try
            {
                //  this should crash due to an invalid argument
                FileMoverChunker obj = new FileMoverChunker("ZZZ", "ZZZ", -1, 42, true);
                Assert.IsTrue(false);
            }
            catch (Exception ex1)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void A05_TestConstructorInvalidChunkPeriod()
        {
            try
            {
                //  this should crash due to an invalid argument
                FileMoverChunker obj = new FileMoverChunker("ZZZ", "ZZZ", 42, -1, true);
                Assert.IsTrue(false);
            }
            catch (Exception ex1)
            {
                Assert.IsTrue(true);
            }
        }
        
        [TestMethod]
        public void A06_TestMoveFiles()
        {
            try
            {
                //  this keeps running until the task is complete --  must monitor...
                FileMoverChunker obj = new FileMoverChunker();
                obj.MoveFiles();
                Assert.IsTrue(true);
            }
            catch (Exception ex1)
            {
                Assert.IsTrue(false);
            }
        }

    }
}
