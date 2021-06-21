using DaroohaNewSite.Common.Helpers.Helpers.Pagination;
using DaroohaNewSite.Data.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DaroohaNewSite.Common.Helpers.Helpers
{
    public static class Extensions
    {
        public static void AddAppError(this HttpResponse response, string message)
        {
            response.Headers.Add("App-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "App-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse response,
            int currentPage, int itemsPerPage,
            int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage,
             totalItems, totalPages);
            var camelCaseFormater = new JsonSerializerSettings();
            camelCaseFormater.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormater));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }

        public static int ToAge(this DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;
            if (dateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;
        }

        public static Expression<Func<Tbl_Product, bool>> ToProductExpression(this string Filter, string id)
        {
            if (string.IsNullOrEmpty(Filter) || string.IsNullOrWhiteSpace(Filter))
            {
                Expression<Func<Tbl_Product, bool>> exp = p => p.Tbl_Menu.ID.Equals(id);
                return exp;
            }
            else
            {
                Expression<Func<Tbl_Product, bool>> exp =
                                p => p.Tbl_Menu.ID == id.ToString() &&
                                p.ProductName.Contains(Filter) ||
                                p.ProductPrice.ToString().Contains(Filter) ||
                                p.Size.Contains(Filter) ||
                                p.Code.Contains(Filter) ||
                                p.EnclosureType.Contains(Filter) ||
                                p.ManufacturingCountry.Contains(Filter) ||
                                p.ManufacturerCompany.Contains(Filter) ||
                                p.WebAddress.Contains(Filter) ||
                                p.Features.Contains(Filter) ||
                                p.MethodUse.Contains(Filter) ||
                                p.Indications.Contains(Filter) ||
                                p.Warnings.Contains(Filter) ||
                                p.Discount.Contains(Filter) ||
                                p.Maintenance.Contains(Filter) ||
                                p.ScientificName.Contains(Filter);

                string test = exp.ToString();

                return exp;
            }

        }

        public static string ToProductOrderBy(this string sortHe, string sortDir)
        {
            if (string.IsNullOrEmpty(sortHe) || string.IsNullOrWhiteSpace(sortHe))
                return "";
            else
            {
                return sortHe.FirstCharToUpper() + "," + sortDir;
            }
        }
        public static string FirstCharToUpper(this string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
