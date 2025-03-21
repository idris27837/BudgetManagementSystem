

using System.ComponentModel.DataAnnotations;

namespace BudgetManagementSystem.Models.BudgetMgt
{

    public enum Status
    { 
        Draft = 1,
        PendingApproval,
        ApprovedAndActive,
        Returned,
        Rejected,
        AwaitingEvaluation,
        Completed,
        Paused,
        Cancelled,
        Breached,
        Deactivated,
        All,
        Closed,
        PendingAcceptance,
        Active,
        PendingResolution,
        ResolvedAwaitingFeedback,
        Escalated,
        AwaitingRespondentComment,
    }

    public enum ResolutionLevl
    {
        SBU = 1,
        Department,
        HRD
    }


    public enum ResolutionRemark
    {
        Pending = 1,
        Accepted,
        Escalated
    }

    public enum ReviewPeriodRange
    {
        Quarterly = 1,
        BiAnually,
        Annually
    }
  
    public enum QuaterType
    {
        FirstQuarter = 1,
        SecondQuarter,
        ThirdQuarter,
        FourthQuarter
    };
    public enum BiAnuallType
    {
        FirstBiAnuall = 1,
        SecondBiAnuall
    }

    public enum PerformanceGrade
    {
        Developing = 1, // < 50
        Progressive,    // > 49.99 & < 66
        Competent,      // > 65.99 & < 80
        Accomplished,   // > 79.99 & < 90
        Exemplary       // > 89.99 & < 100
    }
    public enum FeedBackRequestTypes
    {
        WorkProductEvaluation = 1,
        ObjectivePlanning,
        ProjectPlanning,
        CommitteePlanning,
        WorkProductFeedback,
        _360ReviewFeedback,
        WorkProductPlanning,
        CompetencyReview,
        ReviewPeriod,
        ReviewPeriodExtension,
        ProjectMemberAssignment,
        CommitteeMemberAssignment,
        PeriodObjectiveOutcomeEvaluation,
        DepartmentObjectiveOutcomeEvaluation,
        ReviewPeriod360Review,
        ProjectWorkProductDefinition,
        CommitteeWorkProductDefinition,
    }

    public enum ConCat
    {
        Before,
        After
    }


    public enum SequenceNumberTypes
    {
        CategoryDefinitions = 1,
        CommitteeMembers,
        Committees,
        CompetencyReviewerRatings,
        CompetencyReviewers,
        CompetencyReviewFeedbacks,
        DepartmentObjectives,
        DivisionObjectives,
        EnterpriseObjectives,
        EvaluationOptions,
        FeedbackQuestionaireOptions,
        FeedbackQuestionaires,
        FeedbackRequestLogs,
        GrievanceResolutions,
        Grievances,
        ObjectiveCategory,
        OfficeObjectives,
        PerformanceReviewPeriods,
        PeriodObjectives,
        Periods,
        PmsCompetencies,
        PmsConfigurations,
        ProjectMembers,
        Projects,
        Settings,
        Strategies,
        WorkProductEvaluations,
        WorkProducts,
        WorkProductTasks,
        ReviewPeriodExtensions,
        CommitteeObjectives,
        ProjectObjectives,
        PeriodObjectiveDepartmentEvaluations,
        PeriodObjectiveEvaluations,
        ProjectWorkProducts,
        CommitteeWorkProducts,
        OperationalObjectiveWorkProducts,
        ReviewPeriodIndividualPlannedObjectives,
        PeriodScores,
        DepartmentObjectiveWorkProducts,
        DivisionObjectiveWorkProducts,
        OfficeObjectiveWorkProducts,
        ProjectAssignedWorkProducts,
        CommitteeAssignedWorkProducts,
        CompetencyGapClosures,
        WorkProductDefinition,
        ReviewPeriod360Reviews,
		StrategicThemes,
		ProjectAssignedWorkProductDefinition,
		CommitteeAssignedWorkProductDefinition
	}

    public enum GrievanceType
    {   None =0,
        WorkProductEvaluation = 1,
        WorkProductAssignment,
        WorkProductPlanning,
        ObjectivePlanning
    }

    public enum WorkProductType
    {
        Operational = 1,
        Project,
        Committee
    }
    public enum EvaluationType
    {
        Timeliness = 1,
        Quality,
        Output
    }
    public enum ObjectiveLevel
    {
        Department=1,
        Division,
        Office,
        Enterprise
    }

    public enum OperationTypes
    {
        Add,
        Update,
        Delete,
        Draft,
        CommitDraft,
        Approve,
        Reject,
        Cancel, 
        Complete,
        Pause,
        Close,
        ReSubmit,
        Return,
        Accept,
        ReEvaluate,
        EnableObjectivePlanning,
        DisableObjectivePlanning,
        AddWithoutApproval,
        ReInstate, 
        Drop,
        Resume
    }
    public enum Approval
    {
        Approved = 1,
        Rejected,
    }
    public enum ReviewPeriodExtensionTargetType
    {
        Bankwide = 1,
        Department,
        Division,
        Office,
        Staff
    }
    public enum ReviewPeriod360TargetType
    {
        Bankwide = 1,
        Department,
        Division,
        Office,
        Staff
    }

    public enum ObjectiveType
    {
        Enterprise = 1,
        Operational,
    }

    public enum JobGradeGroupType
    {
        Junior = 1,
        Officer,
        Manager,
        Executive
    }

    public enum LineManagerPerformnanceCategory
    {
        ObjectivePlanning = 1,
        WorkProductPlanning,
        WorkProductEvaluation,
        ProjectPlanning,
        ProjectWorkProductPlanning,
        ProjectWorkProductEvaluation,
        CommitteePlanning,
        CommitteeWorkProductPlanning,
        CommitteeWorkProductEvaluation,
    }

    public enum OrganogramLevel
    {
        Bankwide = 1,
        Department,
        Division,
        Office,
        Directorate,
    }
    public enum AdhocAssignmentType
    {
        Committee = 1,
        Project,
    }

    public static class EmployeeLocation
    {
        public const int HQ = 147;
    }
}