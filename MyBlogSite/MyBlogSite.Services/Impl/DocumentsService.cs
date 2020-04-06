using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlogSite.Core;
using MyBlogSite.Models.Documents;
using MyBlogSite.Repo;

namespace MyBlogSite.Services.Impl
{
    public class DocumentsService : IDocumentsService, IDependency
    {
        private readonly IDocumentsRepo _documentsRepo;

        public DocumentsService(IDocumentsRepo documentsRepo)
        {
            _documentsRepo = documentsRepo;
        }

        public async Task<List<DocumentsModel>> GetDocumentsList()
        {
            return await _documentsRepo.GetDocumentsList();
        }

        public async Task<DocumentsModel> GetDocumentsById(int id)
        {
            return await _documentsRepo.GetDocumentsById(id);
        }

        public async Task<DocumentsModel> UpdateDocumentByModelId(DocumentsModel model)
        {
            return await _documentsRepo.UpdateDocumentByModelId(model);
        }
    }
}