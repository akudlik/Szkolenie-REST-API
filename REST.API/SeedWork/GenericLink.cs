using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace REST.API.SeedWork
{
    public class PaginationLink
    {
        /// <summary>
        /// URL to first page
        /// </summary>
        public string FirstPage { get; set; }

        /// <summary>
        /// URL to next page
        /// </summary>
        public string NextPage { get; set; }

        /// <summary>
        /// URL to previous page
        /// </summary>
        public string PreviousPage { get; set; }

        /// <summary>
        /// URL to last page
        /// </summary>
        public string LastPage { get; set; }

        public PaginationLink(IUrlHelper urlHelper,string actionName, int pageNumber, int pageSize, long itemCount)
        {
            var pageCount = itemCount > 0
                ? (int) Math.Ceiling(itemCount / (double) pageSize)
                : 0;

            pageCount--;

            FirstPage = urlHelper.Link(actionName, new {PageNumber = 0, PageSize = pageSize});

            if (pageNumber > 0)
                PreviousPage = urlHelper.Link(actionName, new {PageNumber = pageNumber - 1, PageSize = pageSize});

            if (pageCount > pageNumber)
                NextPage = urlHelper.Link(actionName, new {PageNumber = pageNumber + 1, PageSize = pageSize});
            LastPage = urlHelper.Link(actionName, new {PageNumber = pageCount, PageSize = pageSize});
        }
    }
}