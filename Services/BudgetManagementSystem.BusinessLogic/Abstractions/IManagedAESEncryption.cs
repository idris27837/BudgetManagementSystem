namespace BudgetManagementSystem.BusinessLogic.Abstractions {
    public interface IManagedAESEncryption
    {
        Task<string> Decrypt(string encryptedText);
        Task<string> Encrypt(string plainText);
    }
}