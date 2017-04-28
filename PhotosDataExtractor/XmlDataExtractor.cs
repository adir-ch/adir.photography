using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace PhotosDataExtractor
{
    /***
     The class should build a photo xml entry that looks like this:
        <photo>
            <filename>Flying-Low.jpg</filename>
            <title>Flying-Low</title>
            <metadata>
                <width>1920</width>
                <height>981</height>
            </metadata>
            <caption>A.C(C)</caption>
            <tags>
                <tag></tag>
                <tag>Portfolio-1</tag>
                <tag>Portfolio-2</tag>
                <tag>ap-website</tag>
                <tag>ap-wildlife</tag>
            </tags>
        </photo>
     */
    class XmlDataExtractor
    {
        XElement _root; 
        
        public XmlDataExtractor()
        {
            _root = new XElement("photos");
        }

        public XElement BuildPhotosXml(IEnumerable<FileInfo> photos, bool outputResult = false)
        {
            foreach(var photo in photos) 
            {
                //Console.WriteLine("{0}\\{1}", photo.Directory.ToString(), photo.Name);
                ParsePhotoMetadata(photo.Directory.ToString(), photo.Name); 
            }

            if (outputResult == true)
            {
                Console.WriteLine(_root.ToString());
            }

            return _root; 
        }

        private void ParsePhotoMetadata(string path, string fileName)
        {
            using (var stream = new System.IO.FileStream((path + "\\" + fileName), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BitmapSource bitmapStream = BitmapFrame.Create(stream);
                BitmapMetadata meta = (BitmapMetadata)bitmapStream.Metadata;
                List<string> keys = new List<string>(meta.Keywords);
                GeneratePhotoXmlEntry(fileName, meta.Title, meta.Comment, bitmapStream.PixelWidth, bitmapStream.PixelHeight, keys); 
            }
        }

        private void GeneratePhotoXmlEntry(string fileName, string title, string caption, int width, int height, List<string> tags)
        {
            //Console.WriteLine("Photo: name: {0}, title: {1}, {2}x{3}, tags: {4}", fileName, title, width, height, tags.Count());
            XElement photoElement = new XElement("photo");
            photoElement.Add(new XElement("filename", fileName));
            photoElement.Add(new XElement("title", title));
            photoElement.Add(new XElement("caption", caption));
            XElement metadata = new XElement("metadata");
            metadata.Add(new XElement("width", width));
            metadata.Add(new XElement("height", height));
            photoElement.Add(metadata); 

            XElement tagsElement = new XElement("tags"); 
            foreach(var tag in tags)
            {
                tagsElement.Add(new XElement("tag", tag)); 
            }

            photoElement.Add(tagsElement);
            _root.Add(photoElement); 
        }

        static void Main(string[] args)
        {
            if(args.Length > 1)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);
                IEnumerable<FileInfo> files = directoryInfo.EnumerateFiles("*.jpg");
                Console.WriteLine("Photos found: {0}", files.Count());
                Console.WriteLine("Generated XML Data {0}=====================", System.Environment.NewLine);
                XmlDataExtractor extractor = new XmlDataExtractor();
                

                if(args[1] != null) // change galleries xml file as well 
                {
                    var photos = extractor.BuildPhotosXml(files, false);
                    ReplacePhotosData(args[1], photos);
                }
                else
                {
                    extractor.BuildPhotosXml(files, true);
                }
            } 
            else
            {
                Console.WriteLine("Usage: PhotoDataExtractor <photos location> [galleries xml]");
            }
        }

        private static void ReplacePhotosData(string galleriesXmlFile, XElement photos)
        {
            XElement galleries = XDocument.Load(galleriesXmlFile).Element("root");
            var photosElement = galleries.Element("photos"); 
            if(photosElement != null)
            {
                photosElement.ReplaceWith(photos);
                galleries.Save(galleriesXmlFile);
            }

        }
    }
}
