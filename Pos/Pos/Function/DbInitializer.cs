using Microsoft.EntityFrameworkCore;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class DbInitializer
    {
        static byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the bitmap to the memory stream
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                // Return the byte array
                return memoryStream.ToArray();
            }
        }

        public static void Seed(AppDbContext context)
        {
        //    var settings = new Setting[]
        //    {
        //    new Setting
        //    {
        //        WhatsAppStatus = "Active",
        //        AccountSid = "YOUR_TWILIO_ACCOUNT_SID_HERE",
        //        AuthToken = "YOUR_TWILIO_AUTH_TOKEN_HERE",
        //        FromPhoneNumber = "55238886",
        //        WhatsAppMaintStatus = "InActive",
        //        WhatsAppMaintInvoiceStatus = "InActive",
        //        WhatsAppPhoneCode = "+213",
        //        MailMailer = "from@example.com",
        //        MailStatus = "Active",
        //        MailAllowHtml = "true",
        //        MailHost = "smtp.example.com",
        //        MailPort = 587,
        //        MailUsername = "user@example.com",
        //        MailPassword = "password",
        //        PurchaseCodeCondition = "Valid",
        //        PurchaseCode = "PC123456",
        //        //MachineId = "M123456",
        //        //LastUsed = DateTime.Now,
        //        //IsInUse = true,
        //        //Logo = ConvertBitmapToByteArray(Properties.Resources.logitech_options),
        //        IsLockScreen = false,
        //        LockScreenImg = ConvertBitmapToByteArray(Properties.Resources.password_monochromatic),
        //        Company = "Sky Studio",
        //        Description = "Experienced web and desktop developers bringing your digital vision to life.",
        //        Title = "One Stop Solution",
        //        SubTitle = "Sky Soft is the one stop solution for all your issues from start to finish.",
        //        Email = "support@skystudio-agency.com",
        //        Website = "https://skystudio-agency.com",
        //        Address = "1234 Main St",
        //        Tel = "123-456-7890",
        //        Fax = "123-456-7891",
        //        Compte = "Account",
        //        Rib = "RIB",
        //        Nis = "NIS",
        //        Rc = "RC",
        //        Ai = "AI",
        //        IdFiscal = "ID Fiscal",
        //        IsSoundAdded = true,
        //        IsSoundDeleted = true,
        //        IsSoundSelected = true,
        //        IsSoundDenied = true,
        //        IsSoundWrong = true,
        //        PrinterReciept = "Receipt Printer",
        //        PrinterRecieptId = 1,
        //        PrinterDocument = "Document Printer",
        //        PrinterDocumentId = 2,
        //        //TaxId = 1,
        //        //ReminderCaseStatus = "Active",
        //        //DriveStatus = "Enabled",
        //        //DriveAppName = "Drive App",
        //        //DriveRootFolderId = "root",
        //        //DriveCredentialsPath = "/path/to/credentials",
        //        Lang = "en",
        //        //RowVersion = new byte[] { },
        //        UpdatedAt = DateTime.Now
        //    }
        //    };

        //    foreach (var setting in settings)
        //    {
        //        context.Settings.Add(setting);
        //    }
        //    // Seed Business Locations
        //    var businessLocations = new BusinessLocation[]
        //    {
        //    new BusinessLocation
        //    {
        //        Name = "Main Office",
        //        LocationId = "LOC001",
        //        Landmark = "Near Central Park",
        //        City = "New York",
        //        ZipCode = "10001",
        //        State = "NY",
        //        Country = "USA",
        //        Mobile = "123-456-7890",
        //        AlternateContactNumber = "098-765-4321",
        //        Email = "info@mainoffice.com",
        //        Website = "http://www.mainoffice.com",
        //        IsDefault = "Yes",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //    },
        //    new BusinessLocation
        //    {
        //        Name = "Branch Office",
        //        LocationId = "LOC002",
        //        Landmark = "Near City Hall",
        //        City = "San Francisco",
        //        ZipCode = "94102",
        //        State = "CA",
        //        Country = "USA",
        //        Mobile = "321-654-0987",
        //        AlternateContactNumber = "789-123-4560",
        //        Email = "info@branchoffice.com",
        //        Website = "http://www.branchoffice.com",
        //        IsDefault = "No",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //    }
        //    };

        //    foreach (var businessLocation in businessLocations)
        //    {
        //        context.BusinessLocations.Add(businessLocation);
        //    }

        //    // Seed Basic Law Categories
        //    var basicLawCategories = new BasicLawCategory[]
        //    {
        //    new BasicLawCategory
        //    {
        //        Name = "Criminal Law",
        //        Status = "Active",
        //        Code = "CRIM001",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new BasicLawCategory
        //    {
        //        Name = "Civil Law",
        //        Status = "Active",
        //        Code = "CIV001",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    };

        //    foreach (var category in basicLawCategories)
        //    {
        //        context.BasicLawCategories.Add(category);
        //    }

        //    // Seed Case Types
        //    var caseTypes = new CaseType[]
        //    {
        //    new CaseType
        //    {
        //        Name = "Type1",
        //        Status = "Active",
        //        Description = "Description for Type1",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new CaseType
        //    {
        //        Name = "Type2",
        //        Status = "Inactive",
        //        Description = "Description for Type2",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    };

        //    foreach (var caseType in caseTypes)
        //    {
        //        context.CaseTypes.Add(caseType);
        //    }

        //    // Seed Countries
        //    var countries = new Country[]
        //    {
        //        new Country
        //        {
        //            Name = "Algeria",
        //            CreatedAt = DateTime.Now,
        //            UpdatedAt = DateTime.Now,
        //            RowVersion = new byte[] { }
        //        }
        //    };

        //    foreach (var country in countries)
        //    {
        //        context.Countries.Add(country);
        //    }
        //    context.SaveChanges();

        //    // Seed States
        //    var states = new State[]
        //    {
        //    new State
        //    {
        //        Name = "Algiers",
        //        CountryId = countries.First().Id,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new State
        //    {
        //        Name = "Oran",
        //        CountryId = countries.First().Id,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    // Add more states as needed
        //    };

        //    foreach (var state in states)
        //    {
        //        context.States.Add(state);
        //    }
        //    context.SaveChanges();

        //    // Seed Cities with Algerian data
        //    var cities = new City[]
        //    {
        //    new City
        //    {
        //        Name = "Algiers",
        //        StateId = 1,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new City
        //    {
        //        Name = "Oran",
        //        StateId = 2,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new City
        //    {
        //        Name = "Constantine",
        //        StateId = 1,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new City
        //    {
        //        Name = "Annaba",
        //        StateId = 1,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new City
        //    {
        //        Name = "Blida",
        //        StateId = 2,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    // Add more cities as needed
        //    };

        //    foreach (var city in cities)
        //    {
        //        context.Cities.Add(city);
        //    }

        //    // Seed Currencies
        //    var currencies = new Currency[]
        //    {
        //    new Currency
        //    {
        //        Name = "Algerian Dinar",
        //        Code = "DZD",
        //        ExchangeRate = 0.0074m,
        //        Direction = true,
        //        IsActive = true,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Currency
        //    {
        //        Name = "US Dollar",
        //        Code = "USD",
        //        ExchangeRate = 1.00m,
        //        Direction = true,
        //        IsActive = true,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Currency
        //    {
        //        Name = "Euro",
        //        Code = "EUR",
        //        ExchangeRate = 1.10m,
        //        Direction = true,
        //        IsActive = true,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    // Add more currencies as needed
        //    };

        //    foreach (var currency in currencies)
        //    {
        //        context.Currencies.Add(currency);
        //    }

        //    // Seed CustomerCharacters
        //    var customerCharacters = new CustomerCharacter[]
        //    {
        //    new CustomerCharacter
        //    {
        //        Name = "Plaintiff",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new CustomerCharacter
        //    {
        //        Name = "Defendant",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new CustomerCharacter
        //    {
        //        Name = "Witness",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new CustomerCharacter
        //    {
        //        Name = "Attorney",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    // Add more characters as needed
        //    };

        //    foreach (var character in customerCharacters)
        //    {
        //        context.CustomerCharacters.Add(character);
        //    }

        //    // Seed Departments
        //    var departments = new Department[]
        //    {
        //    new Department
        //    {
        //        DepartmentName = "Human Resources",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Department
        //    {
        //        DepartmentName = "Finance",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Department
        //    {
        //        DepartmentName = "IT",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Department
        //    {
        //        DepartmentName = "Marketing",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    // Add more departments as needed
        //    };

        //    foreach (var department in departments)
        //    {
        //        context.Departments.Add(department);
        //    }

        //    // Add initial data to the database
        //    var judicialAuthorities = new JudicialAuthority[]
        //    {
        //        new JudicialAuthority { Name = "Supreme Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Constitutional Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Court of Appeals", Status = "Active" },
        //        new JudicialAuthority { Name = "District Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Family Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Juvenile Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Criminal Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Civil Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Small Claims Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Bankruptcy Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Tax Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Military Court", Status = "Active" },
        //        new JudicialAuthority { Name = "Administrative Court", Status = "Active" },
        //    };

        //    foreach (var authority in judicialAuthorities)
        //    {
        //        context.JudicialAuthorities.Add(authority);
        //    }

        //    context.SaveChanges(); // Save changes to the database

        //    // Seed Sections
        //    var sections = new Section[]
        //    {
        //    new Section
        //    {
        //        Name = "Civil Division",
        //        Description = "Handles civil cases",
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { },
        //        JudicialAuthorityId = 1 // Assuming 1 is a valid JudicialAuthorityId
        //    },
        //    new Section
        //    {
        //        Name = "Criminal Division",
        //        Description = "Handles criminal cases",
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { },
        //        JudicialAuthorityId = 1 // Assuming 1 is a valid JudicialAuthorityId
        //    },
        //    new Section
        //    {
        //        Name = "Family Division",
        //        Description = "Handles family-related cases",
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { },
        //        JudicialAuthorityId = 1 // Assuming 1 is a valid JudicialAuthorityId
        //    }
        //    // Add more sections as needed
        //    };

        //    foreach (var section in sections)
        //    {
        //        context.Sections.Add(section);
        //    }

        //    // Seed SessionLocations
        //    var sessionLocations = new SessionLocation[]
        //    {
        //    new SessionLocation
        //    {
        //        Name = "Supreme Court",
        //        Address = "123 Supreme Court St, Capital City",
        //        Email = "supreme@example.com",
        //        PhoneOne = "123-456-7890",
        //        PhoneTwo = "098-765-4321",
        //        Status = "Active",
        //        Description = "Handles the highest level of judicial matters.",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { },
        //        JudicialAuthorityId = 1 // Assuming 1 is a valid JudicialAuthorityId
        //    },
        //    new SessionLocation
        //    {
        //        Name = "Court of Appeals",
        //        Address = "456 Appeal Ave, Big City",
        //        Email = "appeals@example.com",
        //        PhoneOne = "234-567-8901",
        //        PhoneTwo = "876-543-2109",
        //        Status = "Active",
        //        Description = "Handles appeals from lower courts.",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { },
        //        JudicialAuthorityId = 2 // Assuming 2 is a valid JudicialAuthorityId
        //    }
        //    // Add more session locations as needed
        //    };

        //    foreach (var location in sessionLocations)
        //    {
        //        context.SessionLocations.Add(location);
        //    }

        //    // Seed Services
        //    var services = new Service[]
        //    {
        //    new Service
        //    {
        //        Name = "Legal Consultation",
        //        Description = "Providing legal advice and consultation services.",
        //        Price = 100.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Representation in Court",
        //        Description = "Representing clients in court proceedings.",
        //        Price = 500.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Document Drafting",
        //        Description = "Drafting legal documents such as contracts, wills, and deeds.",
        //        Price = 200.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Negotiation Services",
        //        Description = "Assisting clients in negotiating settlements and agreements.",
        //        Price = 150.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Mediation Services",
        //        Description = "Facilitating mediation sessions to resolve disputes.",
        //        Price = 300.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Arbitration Services",
        //        Description = "Conducting arbitration proceedings to settle disputes.",
        //        Price = 400.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Legal Research",
        //        Description = "Conducting research on legal issues and precedents.",
        //        Price = 80.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Contract Review",
        //        Description = "Reviewing and providing feedback on legal contracts.",
        //        Price = 120.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Corporate Compliance",
        //        Description = "Ensuring corporate compliance with legal regulations.",
        //        Price = 250.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Intellectual Property Protection",
        //        Description = "Providing services to protect intellectual property rights.",
        //        Price = 350.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Employment Law Services",
        //        Description = "Advising on employment law and employee rights.",
        //        Price = 200.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Family Law Services",
        //        Description = "Handling cases related to family law, such as divorce and custody.",
        //        Price = 180.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Real Estate Law Services",
        //        Description = "Advising on real estate transactions and disputes.",
        //        Price = 220.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Tax Law Services",
        //        Description = "Providing advice on tax law and tax planning.",
        //        Price = 300.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Bankruptcy Services",
        //        Description = "Handling bankruptcy cases and providing related advice.",
        //        Price = 400.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Criminal Defense",
        //        Description = "Representing clients in criminal defense cases.",
        //        Price = 600.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Immigration Law Services",
        //        Description = "Providing services related to immigration law.",
        //        Price = 250.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Environmental Law Services",
        //        Description = "Advising on environmental law and regulations.",
        //        Price = 270.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Consumer Protection Services",
        //        Description = "Providing services to protect consumer rights.",
        //        Price = 150.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new Service
        //    {
        //        Name = "Health Law Services",
        //        Description = "Advising on health law and medical regulations.",
        //        Price = 200.00m,
        //        Status = "Active",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    };

        //    foreach (var service in services)
        //    {
        //        context.Services.Add(service);
        //    }

        //    // Seed TemplateCategories
        //    var templateCategories = new TemplateCategory[]
        //    {
        //    new TemplateCategory
        //    {
        //        Name = "Complaints",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new TemplateCategory
        //    {
        //        Name = "Answers",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new TemplateCategory
        //    {
        //        Name = "Counterclaims",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new TemplateCategory
        //    {
        //        Name = "Replies",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new TemplateCategory
        //    {
        //        Name = "Motion to Dismiss",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    },
        //    new TemplateCategory
        //    {
        //        Name = "Motion for Summary Judgment",
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        RowVersion = new byte[] { }
        //    }
        //    };

        //    foreach (var category in templateCategories)
        //    {
        //        context.TemplateCategories.Add(category);
        //    }

        //    // Seed Roles
        //    var roles = new Role[]
        //    {
        //                new Role { Name = "Judge", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Magistrate", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Justice", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Attorney", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Lawyer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Prosecutor", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Defense Attorney", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Public Defender", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Plaintiff", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Defendant", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Respondent", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Petitioner", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Claimant", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Bailiff", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Court Clerk", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Court Reporter", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Paralegal", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Legal Assistant", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Legal Secretary", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Jury Member", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Witness", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Expert Witness", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Forensic Analyst", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Guardian ad Litem", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Mediator", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Arbitrator", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Court Interpreter", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Probation Officer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Parole Officer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Court Administrator", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Sheriff", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Marshal", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Court Security Officer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Legal Counsel", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Solicitor", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Barrister", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "In-house Counsel", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Judicial Assistant", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Law Clerk", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Litigator", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Corporate Counsel", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Compliance Officer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Legal Advisor", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Legal Consultant", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Investigative Officer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Family Law Facilitator", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Patent Attorney", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Trademark Attorney", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Immigration Lawyer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Human Rights Lawyer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Civil Rights Lawyer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Environmental Lawyer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Tax Attorney", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Real Estate Attorney", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Bankruptcy Attorney", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Criminal Defense Lawyer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Civil Litigator", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Adjudicator", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        //                new Role { Name = "Hearing Officer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        //    };

        //    foreach (var role in roles)
        //    {
        //        context.Roles.Add(role);
        //    }

        //    // Seed Permissions
        //    var permissions = new Models.Permission[]
        //    {
        //        new Models.Permission { Name = "Add Customer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Customer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Customer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Customers", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add User", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit User", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete User", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Users", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Case", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Case", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Case", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Case Type", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Case Type", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Case Type", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Case Types", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Judicial Authority", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Judicial Authority", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Judicial Authority", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Judicial Authorities", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Section", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Section", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Section", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Sections", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Location Session", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Location Session", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Location Session", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Location Sessions", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Office Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Statistics", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Permission", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Permission", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Permission", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Permissions", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Role", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Role", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Role", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Roles", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Settings", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Reminder Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Today Summary", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Alert Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Lock Screen", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Location", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Location", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Location", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Locations", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Basic Laws", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Templates", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Files", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Printer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Printer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Printer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Printers", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Expense", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Expense", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Expense", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Expenses", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Expense Category", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Expense Category", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Expense Category", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Expense Categories", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Dashboard", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Currency", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Currency", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Currency", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Currencies", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Business Location", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Business Location", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Business Location", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Business Locations", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Service", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Service", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Service", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Services", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Fee Payment", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "View Customer Details", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Send emails to all customers", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Customer badge printing", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Template Category", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Template Category", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete All Templates", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Template", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Upload Template", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Select a template", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Case Quick View", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Payments Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Latest Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Closest Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Favorite Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Winning Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Blacklisted Cases", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Cases Of Withdrawn Files", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Basic Law Category", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Basic Law Category", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Tasks", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Task", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Task", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Task", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "List Taxes", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Add Tax", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Edit Tax", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Delete Tax", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //        new Models.Permission { Name = "Audit Trail", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
        //    };

        //    foreach (var permission in permissions)
        //    {
        //        context.Permissions.Add(permission);
        //    }

        //    context.SaveChanges();
        }

    }
}
