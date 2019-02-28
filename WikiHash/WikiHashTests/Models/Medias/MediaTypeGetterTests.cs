using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiHash.Models.Medias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiHash.Models.Medias.Tests
{
    [TestClass()]
    public class MediaTypeGetterTests
    {
        [TestMethod()]
        public void GetMediaTypeTest()
        {
            var fileNameImage = "file-name.fhfsd.fdsgdfg   .sdfsd.mp4.jpg";
            var fileNameImageBig = "file-name.fhfsd.fdsgdfg   .sdfsd.mp4.JPG";
            var fileNameVideo = "file-name.fhfsd.fdsgdfg   .sdfsd.jpg.mp4";
            var fileNameVideoBig = "file-name.fhfsd.fdsgdfg   .sdfsd.jpg.MP4";
            var fileNameUnsupported = "file-name.fhfsd.fdsgdfg   .sdfsd.jpg.ico";

            Assert.AreEqual("image", MediaTypeGetter.GetMediaType(fileNameImage));
            Assert.AreEqual("image", MediaTypeGetter.GetMediaType(fileNameImageBig));
            Assert.AreEqual("video", MediaTypeGetter.GetMediaType(fileNameVideo));
            Assert.AreEqual("video", MediaTypeGetter.GetMediaType(fileNameVideoBig));
            Assert.AreEqual("unsupported", MediaTypeGetter.GetMediaType(fileNameUnsupported));
        }
    }
}