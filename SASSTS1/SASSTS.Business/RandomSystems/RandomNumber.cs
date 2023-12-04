using SASSTS.Business.CustomExceptions;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Entities;

namespace SASSTS.Business.RandomSystems
{
    public class RandomNumber : IRandomNumber
    {
        private readonly IUnitWork _unitWork;
        public RandomNumber(IUnitWork unitWork) => _unitWork = unitWork;
        public string randomUret(int companyId, int departmentId)
        {
            var companyNo = _unitWork.GetRepository<Company>().GetAsync(x => x.Id == companyId).Result.CompanyNo;
            var departmentNo = _unitWork.GetRepository<Department>().GetAsync(a => a.Id == departmentId).Result.DepartmentNo;
            var no = companyNo + departmentNo;
            var s = "";

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 1)
                {
                    char d = 'A';
                    char f = 'Z';
                    var c = Convert.ToChar(random.Next(d, f));
                    s += c;
                }
                else
                    s += random.Next(0, 10);
            }
            return no == null ? "Random numara üretilemedi" : no + s;
        }

        public string randomUretCompany(int companyId)
        {
            var companyNo = _unitWork.GetRepository<Company>().GetAsync(c => c.Id == companyId).Result.CompanyNo;
            var s = "";

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 1)
                {
                    char d = 'A';
                    char f = 'Z';
                    var c = Convert.ToChar(random.Next(d, f));
                    s += c;
                }
                else
                    s += random.Next(0, 10);
            }
            return companyNo == null ? throw new BadRequestException("CompanyNo değeri çekilemedi") : companyNo + s;
        }
    }
}