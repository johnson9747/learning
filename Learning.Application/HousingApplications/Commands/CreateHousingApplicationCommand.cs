namespace Learning.Application.HousingApplications.Commands
{
    public record CreateHousingApplicationCommand(int LocationId, string FirstName, string LastName, string Email);
}
