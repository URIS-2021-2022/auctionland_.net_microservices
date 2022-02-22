namespace Komisija_Agregat.Data
{
    public interface IValuesRepository
    {

        public List<string> GetValue();

        public void SetValue(string value);
    }
}
