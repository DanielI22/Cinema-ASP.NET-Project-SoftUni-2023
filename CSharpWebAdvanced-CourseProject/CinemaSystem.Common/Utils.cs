namespace CinemaSystem.Common
{
    public static class Utils
    {
        public static List<int> ParseCommaSeparatedString(string input)
        {
            List<int> result = new List<int>();
            string[] values = input.Split(',');

            foreach (string value in values)
            {
                if (int.TryParse(value, out int intValue))
                {
                    result.Add(intValue);
                }
               else
                {
                    throw new InvalidDataException("invalid data input");
                }
            }

            return result;
        }
    }
}
