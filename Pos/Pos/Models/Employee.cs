using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Employee")]
[Index("BusinessLocationId", Name = "IX_Employee_BusinessLocationId")]
[Index("CityId", Name = "IX_Employee_CityId")]
[Index("CountryId", Name = "IX_Employee_CountryId")]
[Index("DepartmentId", Name = "IX_Employee_DepartmentId")]
[Index("PositionId", Name = "IX_Employee_PositionId")]
[Index("StateId", Name = "IX_Employee_StateId")]
public partial class Employee
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string FirstName { get; set; }

    [StringLength(100)]
    public string LastName { get; set; }

    [StringLength(255)]
    public string NameOfFather { get; set; }

    [StringLength(255)]
    public string NameOfMother { get; set; }

    [StringLength(255)]
    public string Address { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Height { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Weight { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [StringLength(255)]
    public string FamilySituation { get; set; }

    [StringLength(255)]
    public string BloodGroup { get; set; }

    [StringLength(255)]
    public string Civility { get; set; }

    [StringLength(255)]
    public string SpecialMarque { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; }

    public DateOnly? HireDate { get; set; }

    public bool? Assured { get; set; }

    [StringLength(255)]
    public string NoCard { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CardDeliveryAt { get; set; }

    [StringLength(255)]
    public string NoPass { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PassDeliveryAt { get; set; }

    public bool? BlackList { get; set; }

    [StringLength(255)]
    public string SpouseName { get; set; }

    [Column(TypeName = "text")]
    public string ShortBiography { get; set; }

    public int? ChildrenCount { get; set; }

    [StringLength(255)]
    public string EmergencyContactPhone { get; set; }

    [StringLength(255)]
    public string EmergencyContactRelation { get; set; }

    [StringLength(255)]
    public string EmergencyContactName { get; set; }

    [StringLength(255)]
    public string EducationLevel { get; set; }

    [StringLength(255)]
    public string ExperienceYears { get; set; }

    [StringLength(255)]
    public string PreviousEmployer { get; set; }

    [Column(TypeName = "image")]
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
    public string LinkedInProfile { get; set; }

    [StringLength(255)]
    public string EmploymentType { get; set; }

    [Column(TypeName = "text")]
    public string Note { get; set; }

    [StringLength(15)]
    public string Gender { get; set; }

    [StringLength(50)]
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
