using BudgetManagementSystem.DataAccessLayer.Context;
using System.Text;
#nullable disable
namespace BudgetManagementSystem.BusinessLogic.Concrete {
    public class AppKey
    {
        private CompetencyCoreDbContext Db { get; }
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }
        public AppKey(CompetencyCoreDbContext db)
        {

            Db = db;
            var ivModel = Db.Settings.FirstOrDefault(x => x.Name.ToUpper().Equals("IV_KEY"));
            if (ivModel != null) 
            IV = Encoding.UTF8.GetBytes(ivModel.Value);

            var keyModel = Db.Settings.FirstOrDefault(x => x.Name.ToUpper().Equals("AES_KEY"));
            if(keyModel != null)
            Key = Encoding.UTF8.GetBytes(keyModel.Value);

        }

    }
}
