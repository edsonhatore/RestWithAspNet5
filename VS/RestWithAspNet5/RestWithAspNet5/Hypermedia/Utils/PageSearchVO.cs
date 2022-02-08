using RestWithAspNet5.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Hypermedia.Utils
{
    public class PageSearchVO<T> where T : ISupportsHyperMedia
    {

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public int SortFields { get; set; }
        public string SortDirections { get; set; }
        public Dictionary<string, object> Filters { get; set; }

        public List<T> List { get; set; }

        public PageSearchVO()
        {
        }

        public PageSearchVO(int currentPage, int pageSize, int sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PageSearchVO(int currentPage, int pageSize, int sortFields, string sortDirections, Dictionary<string, object> filters)

        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PageSearchVO(int currentPage, int sortFields, string  sortDirections)
            : this(currentPage,10, sortFields, sortDirections)
        {
            
        }

        public int GetCurrentPage()
        {

            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetpagePage()
        {

            return PageSize == 0 ? 2 : PageSize;
        }
    }
}
