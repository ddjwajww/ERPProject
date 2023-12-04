namespace SatinAlmaStokYonetim.Web.Code
{
    public class Repo
    {
        public class Session
        {
            public static string? UserId
            {
                get
                {
                    string userId = new HttpContextAccessor().HttpContext.Session.GetString("UserId");
                    return userId;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("UserId", value ?? "");
                }
            }
            public static string? Name
            {
                get
                {
                    string name = new HttpContextAccessor().HttpContext.Session.GetString("Name");
                    return name;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Name", value ?? "");
                }
            }
            public static string? Image
            {
                get
                {
                    string image = new HttpContextAccessor().HttpContext.Session.GetString("Image");
                    return image;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Image", value ?? "");
                }
            }

            public static string? Surname
            {
                get
                {
                    string surname = new HttpContextAccessor().HttpContext.Session.GetString("Surname");
                    return surname;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Surname", value ?? "");
                }
            }
            public static string? Token
            {
                get
                {
                    string token = new HttpContextAccessor().HttpContext.Session.GetString("Token");
                    return token;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Token", value ?? "");
                }
            }
            public static string? Company
            {
                get
                {
                    string company = new HttpContextAccessor().HttpContext.Session.GetString("Company");
                    return company;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Company", value ?? "");
                }
            }
            public static string? CompanyName
            {
                get
                {
                    string companyName = new HttpContextAccessor().HttpContext.Session.GetString("CompanyName");
                    return companyName;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("CompanyName", value ?? "");
                }
            }
            public static string? CompanyId
            {
                get
                {
                    string companyId = new HttpContextAccessor().HttpContext.Session.GetString("CompanyId");
                    return companyId;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("CompanyId", value ?? "");
                }
            }
            public static string? Authority
            {
                get
                {
                    string authority = new HttpContextAccessor().HttpContext.Session.GetString("Authority");
                    return authority;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Authority", value ?? "");
                }
            }

            public static string? AuthorityID
            {
                get
                {
                    string authorityId = new HttpContextAccessor().HttpContext.Session.GetString("AuthorityId");
                    return authorityId;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("AuthorityId", value ?? "");
                }
            }
            public static string? Department
            {
                get
                {
                    string department = new HttpContextAccessor().HttpContext.Session.GetString("Department");
                    return department;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Department", value ?? "");
                }
            }
            public static string? DepartmentName
            {
                get
                {
                    string departmentName = new HttpContextAccessor().HttpContext.Session.GetString("DepartmentName");
                    return departmentName;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("DepartmentName", value ?? "");
                }
            }
            public static string? DepartmentId
            {
                get
                {
                    string departmentId = new HttpContextAccessor().HttpContext.Session.GetString("DepartmentId");
                    return departmentId;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("DepartmentId", value ?? "");
                }
            }


            public static string? Role
            {
                get
                {
                    string role = new HttpContextAccessor().HttpContext.Session.GetString("Role");
                    return role;
                }
                set
                {
                    new HttpContextAccessor().HttpContext.Session.SetString("Role", value ?? "");
                }
            }

        }

        public class Exchange
        {
            public static string? Dolar { get; set; }
            public static string? Euro { get; set; }
            public static string? Sterlin { get; set; }
        }
    }
}
