using EmployeeManagementSystem.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagementSystem.ServiceFilters
{
    public class BuildBasicFilter :IAsyncActionFilter
    {
        //Execute acode before action executes
       public async Task OnActionExecutionAsync(ActionExecutingContext context , ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeBasicFilterCriteria);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }
            EmployeeBasicFilterCriteria filterCriteria= (EmployeeBasicFilterCriteria)param.Value;
            var statusFilter = filterCriteria.Filters.Find(a => a.FieldName == "status");
            if(statusFilter == null)
            {
                statusFilter = new FilterCriteria();
                statusFilter.FieldName = "status";
                statusFilter.FieldValue = "Active";
                filterCriteria.Filters.Add(statusFilter);
            }

            filterCriteria.Filters.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));
            var result = await next();
        }

    }
}
