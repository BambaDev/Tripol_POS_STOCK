using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class PagedDataSource<T>
    {
        private readonly int pageSize;
        private int currentPage;
        private readonly Func<int, int, List<T>> fetchData;
        private readonly Func<int> getTotalCount;

        public PagedDataSource(int pageSize, Func<int, int, List<T>> fetchData, Func<int> getTotalCount)
        {
            this.pageSize = pageSize;
            this.fetchData = fetchData;
            this.getTotalCount = getTotalCount;
            this.currentPage = 0;
        }

        public List<T> GetDataPage()
        {
            return fetchData(currentPage * pageSize, pageSize);
        }

        public void NextPage()
        {
            if (HasMorePages())
            {
                currentPage++;
            }
        }

        public void PreviousPage()
        {
            if (currentPage > 0)
                currentPage--;
        }

        public bool HasMorePages()
        {
            return (currentPage + 1) * pageSize < getTotalCount();
        }

        public int GetCurrentPage()
        {
            return currentPage + 1; // Pages are 1-based for user display
        }
    }

}
