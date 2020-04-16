using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlogSite.Core;
using MyBlogSite.Models.Documents;

namespace MyBlogSite.Repo.Impl
{
    public class DocumentsRepo : IDocumentsRepo, IDependency
    {
        public async Task<List<DocumentsModel>> GetDocumentsList()
        {
            throw new System.NotImplementedException();
        }

        public async Task<DocumentsModel> GetDocumentsById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DocumentsModel> UpdateDocumentByModelId(DocumentsModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}