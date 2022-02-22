namespace Komisija_Agregat.Data
{
    public class ValuesRepository : IValuesRepository
    {

        public static List<string> Values { get; set; } = new List<string>();
        public List<string> GetValue()
        {
            return (from e in Values                   
                    select e).ToList();
        }

        public void SetValue(string value)
        {

    
            Values.Add(value);
  

        }
    }
}
