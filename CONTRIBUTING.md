Contributions are welcome. Please create a new issue or pick an existing 
issue to work on. Let me know on which issue you are going to work before 
raising a pull request.

You need Visual Studio 2019, ASP.NET 3.1 and SQL Server to run this app locally.
Create database `StudentDb` and table `Students` based on below model.

```c#
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
}
```
