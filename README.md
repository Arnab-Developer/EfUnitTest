# EfUnitTest
Unit test of Entity Framework Core data access layer.

An example of data access layer unit testing with Entity Framework Core. In the application Entity Framework Core is using Sql Server provider to save and read data. But in unit testing, Entity Framework Core is using memory provider to test the data access layer.

We can use Entity Framework Core db context for our data access layer. Data access layer can communicate with real external data source like SQL Server. But the question is how we can unit test the data access layer? Because during unit testing it is not possible to work with real database.


The process to unit test a data access layer which is using Entity Framework Core db context is using in-memory provider. Entity Framework Core has different providers. We can push provider to Entity Framework Core db context class in runtime. When the application is running we can push Entity Framework Core SQL Server provider to work with real SQL Server database and at the time of unit testing we can push in-memory provider to test with in memory data.

To show you a demo, I have created one small application.

It is a MVC application with ASP.NET Core and Entity Framework Core. In backend I have used SQL Server.

Model

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
    }
    
Db context class

    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }
 
        public DbSet<Student> Students { get; set; }
    }
    
I have added the db context class in startup for dependency injection. Please notice that I have pushed SQL Server provider in the db context here.

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        var conStr = Configuration.GetValue("ConStr");
        services.AddDbContext<StudentContext>(option => option.UseSqlServer(conStr));
    }
    
I have added the connection string in appsettings.json.

    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
         }
       },
       "AllowedHosts": "*",
       "ConStr": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDb;Integrated Security=True"
    }
    
Controller

    public class StudentController : Controller
    {
        private readonly StudentContext _studentContext;
 
        public StudentController(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }
 
        public IActionResult Index()
        {
            var students = _studentContext.Students
                .OrderBy(student => student.Id)
                .ToList();
            return View(students);
        }
    }
    
In case of unit testing for the controller I have pushed memory provider in db context.

    var options = new DbContextOptionsBuilder<StudentContext>()
        .UseInMemoryDatabase("ExamTestDatabase")
        .Options;
    var studentContext = new StudentContext(options);
    _studentController = new StudentController(studentContext);
