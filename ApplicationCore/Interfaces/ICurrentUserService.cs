namespace ApplicationCore.Interfaces
{
    public interface ICurrentUserService
    {
        string? Id { get; }

        string? UserName { get; }

        string? FirstName { get; }

        string? LastName { get; }

        string? Email { get; }
    }
}
