using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Drawing;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ImportExportController : ControllerBase
    {
        private readonly IEmployeeBasicDetailsInterface employeeBasicDetailsInterface;
        private readonly IEmployeeAdditionalDetailsInterface employeeAdditionalDetailsInterface;

        public ImportExportController(IEmployeeBasicDetailsInterface employeeBasicDetails, IEmployeeAdditionalDetailsInterface employeeAdditionalDetails)
        {
            employeeBasicDetailsInterface = employeeBasicDetails;
            employeeAdditionalDetailsInterface = employeeAdditionalDetails;
        }



        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var employeeBasicDetailsEntity = new EmployeeBasicDetailsEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            FirstName = worksheet.Cells[row, 2].Text,
                            LastName = worksheet.Cells[row, 3].Text,
                            Email = worksheet.Cells[row, 4].Text,
                            Mobile = worksheet.Cells[row, 5].Text,
                            ReportingManagerName = worksheet.Cells[row, 6].Text,
                            DateOfBirth = DateTime.Parse(worksheet.Cells[row, 7].Text),
                            DateOfJoining = DateTime.Parse(worksheet.Cells[row, 8].Text)
                        };

                        await employeeBasicDetailsInterface.AddEmployeeBasicDetails(employeeBasicDetailsEntity);
                    }
                }
            }

            return Ok();
        }


        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var employees = await employeeBasicDetailsInterface.GetAllEmployees();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");
                worksheet.Cells[1, 1].Value = "Sr.No";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Phone No";
                worksheet.Cells[1, 6].Value = "Reporting Manager Name";
                worksheet.Cells[1, 7].Value = "Date Of Birth";
                worksheet.Cells[1, 8].Value = "Date of Joining";

                int row = 2;
                foreach (var employee in employees)
                {
                    worksheet.Cells[row, 1].Value = row - 1;
                    worksheet.Cells[row, 2].Value = employee.FirstName;
                    worksheet.Cells[row, 3].Value = employee.LastName;
                    worksheet.Cells[row, 4].Value = employee.Email;
                    worksheet.Cells[row, 5].Value = employee.Mobile;
                    worksheet.Cells[row, 6].Value = employee.ReportingManagerName;
                    worksheet.Cells[row, 7].Value = employee.DateOfBirth.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 8].Value = employee.DateOfJoining.ToString("yyyy-MM-dd");
                    row++;
                }

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "EmployeeDetails.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }

        }
    }
}