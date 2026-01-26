using System.Collections.Generic;
using BusinessObjects;

namespace Repositories.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}