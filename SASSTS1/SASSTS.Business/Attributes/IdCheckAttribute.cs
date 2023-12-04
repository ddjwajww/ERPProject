using ArxOne.MrAdvice.Advice;

namespace SASSTS.Business.Attributes
{
    public class IdCheckAttribute : Attribute, IMethodAdvice
    {
        private string _tableName;
        public IdCheckAttribute(string tableName) => _tableName = tableName;
        public void Advise(MethodAdviceContext context)
        {
            if (Convert.ToInt32(context.Arguments[0]) <= 0)
                throw new Exception($"Method: {_tableName}  ->  Girilen id değeri 0 dan küçük veya eşit olamaz");
        }
    }
}
