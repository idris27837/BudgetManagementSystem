using BudgetManagementSystem.Models.PerformanceMgt;
using PMS.Models;
using PMS.Models.PerformanceMgt.CoreModels;
namespace BudgetManagementSystem.ViewModels.PMSVms.SetupVm

{

    public class ProjectVm : BaseWorkFlow

    {

        public string ProjectId { get; set; }

        public string ProjectManager { get; set; }

        public virtual List<ProjectMember> ProjectMembers { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Deliverables { get; set; }

        public Status RecordStatus { get; set; }

        public string ReviewPeriodId { get; set; }

        public PerformanceReviewPeriod ReviewPeriod { get; set; }

        public List<WorkProduct> WorkProducts { get; }

    }



}

