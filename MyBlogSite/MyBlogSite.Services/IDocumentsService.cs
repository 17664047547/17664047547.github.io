using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlogSite.Models.Documents;

namespace MyBlogSite.Services
{
    public interface IDocumentsService
    {
        /// <summary>
        /// 获取最新的文档列表
        /// </summary>
        /// <returns></returns>
        Task<List<DocumentsModel>> GetDocumentsList();

        /// <summary>
        /// 根据Id获取文档信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DocumentsModel> GetDocumentsById(int id);

        /// <summary>
        /// 根据Model中的Id修改文档信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DocumentsModel> UpdateDocumentByModelId(DocumentsModel model);
    }
}