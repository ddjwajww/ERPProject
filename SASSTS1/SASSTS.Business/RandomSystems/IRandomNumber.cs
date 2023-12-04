namespace SASSTS.Business.RandomSystems
{
    public interface IRandomNumber
    {
        string randomUret(int companyId, int departmentId);
        string randomUretCompany(int companyId);
    }
}
