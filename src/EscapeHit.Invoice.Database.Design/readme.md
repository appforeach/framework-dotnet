Add-Migration -StartupProject EscapeHit.Invoice.Database.Design -Project EscapeHit.Invoice.Database <Name>

Update-Database -StartupProject EscapeHit.Invoice.Database.Design -Project EscapeHit.Invoice.Database

dotnet ef migrations add <Name> --project EscapeHit.Invoice.Database --startup-project EscapeHit.Invoice.Database.Design
dotnet ef database update  --project EscapeHit.Invoice.Database --startup-project EscapeHit.Invoice.Database.Design