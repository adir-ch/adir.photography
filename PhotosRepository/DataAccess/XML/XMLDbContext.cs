using log4net;
using PhotosRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PhotosRepository.DataAccess.XML
{
    public class XMLDbContext : IDbContext
    {
        private XElement _dbContext;
        private string _serverPath; 
        private static readonly ILog _log = LogManager.GetLogger("PhotoReprository"); //System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public XMLDbContext(XElement root, string serverPath)
        {
            _dbContext = root;
            _serverPath = serverPath; 
        }

        public bool Save(Object entity)
        {
            bool status = false;
            try
            {
                _dbContext.Save(_serverPath + "/galleries.xml");
                _log.DebugFormat("DB Updated with: Name: {0}, Description: {1}", (entity as IDBEntity).EntityName, (entity as IDBEntity).EntityDescription);
                status = true;
            }
            catch (Exception e)
            {
                _log.ErrorFormat("Unable to update photos DB file: {0}", e.Message);
            }

            return status;
        }

        public IEnumerable<Object> Fetch(string query, string queryParam = null)
        {
            List<Object> output = new List<Object>(); 
            if (query == "all-photos")
            {
                GetAllPhotos(ref output, queryParam);
            }

            if (query == "all-galleries")
            {
                GetAllGalleries(ref output, queryParam);
            }

            if (query == "gallery")
            {
                GetGallery(ref output, queryParam);
            }

            return output; 
        }

        private void GetGallery(ref List<Object> output, string queryParam = null)
        {
            var entities = _dbContext.Element("galleries").Descendants("name").Where(g =>
                        String.Equals(g.Value, queryParam, StringComparison.CurrentCultureIgnoreCase));
            foreach (var entity in entities)
            {
                output.Add(entity);
            }
        }

        private void GetAllGalleries(ref List<Object> output, string queryParam = null)
        {
            var entities = _dbContext.Element("galleries").Descendants().Where(g => g.Name == "name").ToList(); 
            foreach (var entity in entities)
            {
                output.Add(entity);
            }
        }

        private void GetAllPhotos(ref List<Object> output, string queryParam = null)
        {
            var entities = _dbContext.Element("photos").Descendants().Where(tag => tag.Name == "photo").ToList();
            foreach (var entity in entities)
            {
                output.Add(new XMLPhotoDBEntity(entity));
            }
        }

        public bool Update(Object entity)
        {
            throw new NotImplementedException();
        }
    }
}
