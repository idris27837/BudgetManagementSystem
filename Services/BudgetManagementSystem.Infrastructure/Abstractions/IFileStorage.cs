
using BudgetManagementSystem.Infrastructure.CustomConfig;
using Microsoft.AspNetCore.Http;

namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IFileStorage
{
    bool CheckFile(IFormFile file);
    void DeleteFile(string fileName);
    Task<StorageResult> SaveFile(IFormFile file);
    Task<StorageResult> SaveRelevantDocFile(IFormFile file);
    Task<StorageResult> UpdateFile(IFormFile file, string fullPath);
}