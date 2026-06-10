using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pos.Function;

namespace Pos.Models;

[Table("Employee")]
[Index("BusinessLocationId", Name = "IX_Employee_BusinessLocationId")]
[Index("CityId", Name = "IX_Employee_CityId")]
[Index("CountryId", Name = "IX_Employee_CountryId")]
[Index("DepartmentId", Name = "IX_Employee_DepartmentId")]
[Index("PositionId", Name = "IX_Employee_PositionId")]
[Index("StateId", Name = "IX_Employee_StateId")]
[SensitiveEntity(SensitivityLevel.Confidential, "Employee personal and professional data")]
public partial class Employee
{
    [Key]
    [NoEncryption("Primary key - technical field")]
    public int Id { get; set; }

    [StringLength(250)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Employee first name",
        LegalBasis = "Contract")]
    public string FirstName { get; set; }

    [StringLength(100)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Employee last name",
        LegalBasis = "Contract")]
    public string LastName { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Father's name")]
    public string NameOfFather { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Mother's name")]
    public string NameOfMother { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Home address",
        LegalBasis = "Contract")]
    public string Address { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Height { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Weight { get; set; }

    [Column(TypeName = "datetime")]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Date of birth",
        LegalBasis = "Contract")]
    public DateTime? DateOfBirth { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Family situation (married, single, etc.)")]
    public string FamilySituation { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Restricted, DataCategory.Biometric,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        Description = "Blood group - Article 9 GDPR special category",
        LegalBasis = "Explicit Consent")]
    public string BloodGroup { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.PII,
        Description = "Civility (Mr, Mrs, etc.)")]
    public string Civility { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Restricted, DataCategory.Biometric,
        RequiresEncryption = true,
        Description = "Physical distinctive marks",
        LegalBasis = "Explicit Consent")]
    public string SpecialMarque { get; set; }

    [StringLength(100)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Email address",
        LegalBasis = "Contract")]
    public string Email { get; set; }

    [StringLength(15)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Phone number",
        LegalBasis = "Contract")]
    public string PhoneNumber { get; set; }

    public DateOnly? HireDate { get; set; }

    public bool? Assured { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Classified, DataCategory.Identity,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        Description = "Identity card number",
        LegalBasis = "Legal Obligation")]
    public string NoCard { get; set; }

    [Column(TypeName = "datetime")]
    [NoEncryption("Delivery date - not PII itself")]
    public DateTime? CardDeliveryAt { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Classified, DataCategory.Identity,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        Description = "Passport number",
        LegalBasis = "Legal Obligation")]
    public string NoPass { get; set; }

    [Column(TypeName = "datetime")]
    [NoEncryption("Delivery date - not PII itself")]
    public DateTime? PassDeliveryAt { get; set; }

    public bool? BlackList { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Spouse name")]
    public string SpouseName { get; set; }

    [Column(TypeName = "text")]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.PII,
        Description = "Short biography")]
    public string ShortBiography { get; set; }

    [NoEncryption("Count only - not identifying")]
    public int? ChildrenCount { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Emergency contact phone - third party PII",
        LegalBasis = "Legitimate Interest")]
    public string EmergencyContactPhone { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Emergency contact relationship",
        LegalBasis = "Legitimate Interest")]
    public string EmergencyContactRelation { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Emergency contact name - third party PII",
        LegalBasis = "Legitimate Interest")]
    public string EmergencyContactName { get; set; }

    [StringLength(255)]
    public string EducationLevel { get; set; }

    [StringLength(255)]
    public string ExperienceYears { get; set; }

    [StringLength(255)]
    public string PreviousEmployer { get; set; }

    [Column(TypeName = "image")]
    [SensitiveData(SensitivityLevel.Restricted, DataCategory.Biometric,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        Description = "Employee photo - Article 9 GDPR biometric data",
        LegalBasis = "Explicit Consent",
        SubjectToErasure = true)]
    public byte[] Image { get; set; }

    [Column(TypeName = "text")]
    public string Certifications { get; set; }

    [StringLength(255)]
    public string LanguagesSpoken { get; set; }

    [Column(TypeName = "text")]
    public string Skills { get; set; }

    public int? WorkHoursPerWeek { get; set; }

    public int? VacationDays { get; set; }

    public int? SickDays { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastPromotionDate { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.Contact,
        Description = "LinkedIn profile URL")]
    public string LinkedInProfile { get; set; }

    [StringLength(255)]
    [NoEncryption("Employment type - not PII")]
    public string EmploymentType { get; set; }

    [Column(TypeName = "text")]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.PII,
        Description = "Internal notes")]
    public string Note { get; set; }

    [StringLength(15)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Gender")]
    public string Gender { get; set; }

    [StringLength(50)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Postal code",
        LegalBasis = "Contract")]
    public string CodePostal { get; set; }

    [StringLength(250)]
    public string Code { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? DepartmentId { get; set; }

    public int? PositionId { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public int? UserId { get; set; }

    [StringLength(50)]
    public string Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [InverseProperty("Employee")]
    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("Employees")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Employees")]
    public virtual City City { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("Employees")]
    public virtual Country Country { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Employees")]
    public virtual Department Department { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeDependent> EmployeeDependents { get; set; } = new List<EmployeeDependent>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeExpense> EmployeeExpenses { get; set; } = new List<EmployeeExpense>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeFeedback> EmployeeFeedbackEmployees { get; set; } = new List<EmployeeFeedback>();

    [InverseProperty("FeedbackByNavigation")]
    public virtual ICollection<EmployeeFeedback> EmployeeFeedbackFeedbackByNavigations { get; set; } = new List<EmployeeFeedback>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeFoodService> EmployeeFoodServices { get; set; } = new List<EmployeeFoodService>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeGoal> EmployeeGoals { get; set; } = new List<EmployeeGoal>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeReview> EmployeeReviews { get; set; } = new List<EmployeeReview>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeReward> EmployeeRewards { get; set; } = new List<EmployeeReward>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeShiftSchedule> EmployeeShiftSchedules { get; set; } = new List<EmployeeShiftSchedule>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeTransport> EmployeeTransports { get; set; } = new List<EmployeeTransport>();

    [InverseProperty("Employee")]
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    [InverseProperty("Employee")]
    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

    [ForeignKey("PositionId")]
    [InverseProperty("Employees")]
    public virtual Position Position { get; set; }

    [InverseProperty("AssignedToNavigation")]
    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    [InverseProperty("Employee")]
    public virtual ICollection<ProjectTeamMember> ProjectTeamMembers { get; set; } = new List<ProjectTeamMember>();

    [InverseProperty("Manager")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [ForeignKey("StateId")]
    [InverseProperty("Employees")]
    public virtual State State { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Waste> Wastes { get; set; } = new List<Waste>();
}
