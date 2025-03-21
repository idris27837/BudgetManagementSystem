using BudgetManagementSystem.Infrastructure.Abstractions;
using Microsoft.Extensions.Hosting;

namespace BudgetManagementSystem.Infrastructure.Concrete;

public static class ExcelValidation
{
    public static (bool status, string row, string column) ValidateExcel(int noOfRow, ExcelWorksheet workSheet, int noOfRequired)
    {
        int colum = 0;
        for (int row = 2; row <= noOfRow; row++)
        {
            try
            {
                for (int i = 1; i <= noOfRequired; i++)
                {
                    new ArrayList().Add(workSheet.Cells[row, i].Value.ToString().Trim());
                    colum = i + 1;
                }
            }
            catch (Exception e)
            {
                return (false, row.ToString(), colum.ToString() + " " + e.Message);
            }
        }
        return (true, "", "");
    }

    public static bool ConvertToBool(string excelWord)
    {
        if (excelWord == "YES" || excelWord == "TRUE")
        {
            return true;
        }
        return false;
    }
}


public class FileStorage : IFileStorage
{
    private readonly IHostingEnvironment _hostingEnvironment;

    public FileStorage(IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }


    /// Save file implementation for local file storage.
    /// It returns a SaveUpdateStorageVm model as result
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<StorageResult> SaveFile(IFormFile file)
    {
        var result = new StorageResult();
        if (CheckFile(file))
        {
            try
            {

                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var fileName = Guid.NewGuid().ToString() + extension;

                var path = Path.Combine(_hostingEnvironment.ContentRootPath, $"RelevantDocuments/{fileName}");
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                result.Status = true;
                result.Message = "Successful";
                result.FullPath = path;
                result.FileName = fileName;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = false;
            }
        }
        return result;
    }
    public async Task<StorageResult> SaveRelevantDocFile(IFormFile file)
    {
        var result = new StorageResult();
        if (CheckFile(file))
        {
            try
            {

                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var fileName = Guid.NewGuid().ToString() + extension;

                var path = Path.Combine(_hostingEnvironment.ContentRootPath, $"wwwroot\\RelevantDocuments\\{fileName}");
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                result.Status = true;
                result.Message = "Successful";
                result.FullPath = path;
                result.FileName = fileName;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = false;
            }
        }
        return result;
    }


    /// <summary>
    /// Check if a file is not null by returning true or false.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public bool CheckFile(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Delete file implementation for both local file storage.
    /// It returns true if operation is successful
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public void DeleteFile(string fileName)
    {
        fileName = Path.Combine(_hostingEnvironment.ContentRootPath, $"wwwroot\\RelevantDocuments\\{fileName}");
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

    /// <summary>
    /// Update file implementation for both local file storage.
    /// It returns a SaveUpdateStorageVm model as result
    /// </summary>
    /// <param name="file"></param>
    /// <param name="fullPath"></param>
    /// <returns></returns>
    public async Task<StorageResult> UpdateFile(IFormFile file, string fullPath)
    {
        DeleteFile(fullPath);
        var result = await SaveFile(file);
        return result;

    }


}
