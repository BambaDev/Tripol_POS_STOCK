using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("User")]
[Index("BusinessLocationId", Name = "IX_User_BusinessLocationId")]
[Index("RoleId", Name = "IX_User_RoleId")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string FirstName { get; set; }

    [StringLength(250)]
    public string LastName { get; set; }

    [StringLength(250)]
    public string FullName { get; set; }

    [StringLength(250)]
    public string UserLogin { get; set; }

    [StringLength(250)]
    public string Email { get; set; }

    [StringLength(250)]
    public string Phone { get; set; }

    [StringLength(250)]
    public string Password { get; set; }

    [StringLength(150)]
    public string PinOne { get; set; }

    [StringLength(150)]
    public string PinTwo { get; set; }

    [StringLength(150)]
    public string PinThree { get; set; }

    [StringLength(150)]
    public string PinFour { get; set; }

    [Column(TypeName = "image")]
    public byte[] Image { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [StringLength(250)]
    public string Gender { get; set; }

    [StringLength(250)]
    public string IsAdmin { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? RoleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    [InverseProperty("User")]
    public virtual ICollection<Adjustment> Adjustments { get; set; } = new List<Adjustment>();

    [InverseProperty("User")]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [InverseProperty("User")]
    public virtual ICollection<AuditTrail> AuditTrails { get; set; } = new List<AuditTrail>();

    [InverseProperty("User")]
    public virtual ICollection<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();

    [InverseProperty("User")]
    public virtual ICollection<BankTransfer> BankTransfers { get; set; } = new List<BankTransfer>();

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("Users")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("User")]
    public virtual ICollection<EmployeeExpense> EmployeeExpenses { get; set; } = new List<EmployeeExpense>();

    [InverseProperty("User")]
    public virtual ICollection<EmployeeFeedback> EmployeeFeedbacks { get; set; } = new List<EmployeeFeedback>();

    [InverseProperty("User")]
    public virtual ICollection<EmployeeHealth> EmployeeHealths { get; set; } = new List<EmployeeHealth>();

    [InverseProperty("Interviewer")]
    public virtual ICollection<EmployeeReview> EmployeeReviewInterviewers { get; set; } = new List<EmployeeReview>();

    [InverseProperty("User")]
    public virtual ICollection<EmployeeReview> EmployeeReviewUsers { get; set; } = new List<EmployeeReview>();

    [InverseProperty("User")]
    public virtual ICollection<EmployeeReward> EmployeeRewards { get; set; } = new List<EmployeeReward>();

    [InverseProperty("User")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [InverseProperty("User")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    [InverseProperty("User")]
    public virtual ICollection<PointsTransaction> PointsTransactions { get; set; } = new List<PointsTransaction>();

    [InverseProperty("User")]
    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

    [InverseProperty("User")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("User")]
    public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; } = new List<ProjectDocument>();

    [InverseProperty("User")]
    public virtual ICollection<ProjectMilestone> ProjectMilestones { get; set; } = new List<ProjectMilestone>();

    [InverseProperty("User")]
    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    [InverseProperty("User")]
    public virtual ICollection<ProjectTeamMember> ProjectTeamMembers { get; set; } = new List<ProjectTeamMember>();

    [InverseProperty("User")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("User")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    [InverseProperty("User")]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    [InverseProperty("ClosedBy")]
    public virtual ICollection<RegisterRecord> RegisterRecordClosedBies { get; set; } = new List<RegisterRecord>();

    [InverseProperty("TransferredTo")]
    public virtual ICollection<RegisterRecord> RegisterRecordTransferredTos { get; set; } = new List<RegisterRecord>();

    [InverseProperty("User")]
    public virtual ICollection<RegisterRecord> RegisterRecordUsers { get; set; } = new List<RegisterRecord>();

    [InverseProperty("User")]
    public virtual ICollection<RepairInvoicePayment> RepairInvoicePayments { get; set; } = new List<RepairInvoicePayment>();

    [InverseProperty("User")]
    public virtual ICollection<RepairInvoice> RepairInvoices { get; set; } = new List<RepairInvoice>();

    [InverseProperty("User")]
    public virtual ICollection<RepairPayment> RepairPayments { get; set; } = new List<RepairPayment>();

    [InverseProperty("User")]
    public virtual ICollection<ReturnPurchase> ReturnPurchases { get; set; } = new List<ReturnPurchase>();

    [InverseProperty("User")]
    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<SaleHold> SaleHolds { get; set; } = new List<SaleHold>();

    [InverseProperty("User")]
    public virtual ICollection<SalePayment> SalePayments { get; set; } = new List<SalePayment>();

    [InverseProperty("User")]
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    [InverseProperty("User")]
    public virtual ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();

    [InverseProperty("User")]
    public virtual ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();

    [InverseProperty("User")]
    public virtual ICollection<UserHasPermission> UserHasPermissions { get; set; } = new List<UserHasPermission>();

    [InverseProperty("User")]
    public virtual ICollection<UserHasRole> UserHasRoles { get; set; } = new List<UserHasRole>();

    [InverseProperty("User")]
    public virtual ICollection<Waste> Wastes { get; set; } = new List<Waste>();

    [InverseProperty("User")]
    public virtual ICollection<CashDiscrepancy> CashDiscrepanciesReported { get; set; } = new List<CashDiscrepancy>();

    [InverseProperty("ResolvedBy")]
    public virtual ICollection<CashDiscrepancy> CashDiscrepanciesResolved { get; set; } = new List<CashDiscrepancy>();

    [InverseProperty("ArchivedByUser")]
    public virtual ICollection<Product> ArchivedProducts { get; set; } = new List<Product>();
}
