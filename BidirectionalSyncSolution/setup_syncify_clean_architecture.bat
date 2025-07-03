@echo off
cd /d %~dp0
echo.
echo ✅ Starting Clean Architecture Setup for Bidirectional Sync...

:: Create solution
dotnet new sln -n Syncify

:: Create core projects
dotnet new webapi -n Syncify.Api -o src\Api\Syncify.Api
dotnet new classlib -n Syncify.Application -o src\Application\Syncify.Application
dotnet new classlib -n Syncify.Domain -o src\Domain\Syncify.Domain
dotnet new classlib -n Syncify.Infrastructure -o src\Infrastructure\Syncify.Infrastructure
dotnet new classlib -n Syncify.Shared -o src\Shared\Syncify.Shared

:: Add to solution
dotnet sln add src\Api\Syncify.Api\Syncify.Api.csproj
dotnet sln add src\Application\Syncify.Application\Syncify.Application.csproj
dotnet sln add src\Domain\Syncify.Domain\Syncify.Domain.csproj
dotnet sln add src\Infrastructure\Syncify.Infrastructure\Syncify.Infrastructure.csproj
dotnet sln add src\Shared\Syncify.Shared\Syncify.Shared.csproj

:: Add project references
dotnet add src\Application\Syncify.Application\Syncify.Application.csproj reference src\Domain\Syncify.Domain\Syncify.Domain.csproj
dotnet add src\Infrastructure\Syncify.Infrastructure\Syncify.Infrastructure.csproj reference src\Application\Syncify.Application\Syncify.Application.csproj
dotnet add src\Infrastructure\Syncify.Infrastructure\Syncify.Infrastructure.csproj reference src\Domain\Syncify.Domain\Syncify.Domain.csproj
dotnet add src\Api\Syncify.Api\Syncify.Api.csproj reference src\Application\Syncify.Application\Syncify.Application.csproj
dotnet add src\Api\Syncify.Api\Syncify.Api.csproj reference src\Infrastructure\Syncify.Infrastructure\Syncify.Infrastructure.csproj

:: Create domain models
mkdir src\Domain\Syncify.Domain\Entities
echo namespace Syncify.Domain.Entities { public class Employee { public int EmployeeID { get; set; } public string Name { get; set; } public string Role { get; set; } } } > src\Domain\Syncify.Domain\Entities\Employee.cs
echo namespace Syncify.Domain.Entities { public class KafkaPayload<T> { public string Source { get; set; } public DateTime EventTimestamp { get; set; } public T Data { get; set; } } } > src\Domain\Syncify.Domain\Entities\KafkaPayload.cs

:: Create application interfaces
mkdir src\Application\Syncify.Application\Interfaces
echo namespace Syncify.Application.Interfaces { public interface IKafkaProducer { Task PublishEventAsync<T>(T data, string sourceSystem); } } > src\Application\Syncify.Application\Interfaces\IKafkaProducer.cs
echo using Syncify.Domain.Entities> src\Application\Syncify.Application\Interfaces\IDbService.cs
echo namespace Syncify.Application.Interfaces { public interface IDbService { Task UpsertEmployeeAsync(Employee emp); Task<DateTime?> GetLastUpdatedTimeAsync(int employeeId); } } >> src\Application\Syncify.Application\Interfaces\IDbService.cs

:: Create API folder structure
mkdir src\Api\Syncify.Api\Controllers
type nul > src\Api\Syncify.Api\Controllers\EmployeeController.cs

:: Create Infra folders
mkdir src\Infrastructure\Syncify.Infrastructure\Kafka
mkdir src\Infrastructure\Syncify.Infrastructure\Persistence
type nul > src\Infrastructure\Syncify.Infrastructure\Kafka\KafkaProducer.cs
type nul > src\Infrastructure\Syncify.Infrastructure\Persistence\SqlDbService.cs

:: Optional: Shared folder
mkdir src\Shared\Syncify.Shared\Helpers
type nul > src\Shared\Syncify.Shared\Helpers\Helpers.cs

:: Root readme
type nul > README.md

echo.
echo ✅ Clean project created! Open Syncify.sln to start coding your bidirectional sync logic.
pause
exit /b
