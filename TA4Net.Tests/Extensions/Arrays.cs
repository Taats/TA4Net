namespace TA4Net.Test.Extensions
{
    public static class Arrays
    {
        public static void fill(decimal[] theArray, decimal theValue)
        {
            for(int i =0; i < theArray.Length; i++)
            {
                theArray[i] = theValue;
            }
        }
    }
}
