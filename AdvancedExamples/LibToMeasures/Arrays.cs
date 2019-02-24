using System.Collections.Generic;

namespace LibToMeasures
{
    public static class Arrays
    {
        public static byte[] CalculateArrayBytes(int operations, int lenght)
        {
            var array = new byte[lenght];

            for (int o = 0; o < operations; o++)
            {

                for (int i = 0; i < lenght; i++)
                {
                    array[i] = 64;
                }

                for (int i = lenght - 1; i >= 0; i--)
                {
                    array[i] = 32;
                }
            }

            return array;
        }
        public static List<byte> CalculateListBytes(int operations, int lenght)
        {
            var list = new List<byte>();

            for (int o = 0; o < operations; o++)
            {

                for (int i = 0; i < lenght; i++)
                {
                    list.Add(64);
                }

                for (int i = lenght - 1; i >= 0; i--)
                {
                    list[i] = 32;
                }
            }

            return list;
        }
        public static List<byte> CalculateListBytesWithInitialCapacity(int operations, int lenght)
        {
            var list = new List<byte>(lenght);

            for (int o = 0; o < operations; o++)
            {

                for (int i = 0; i < lenght; i++)
                {
                    list.Add(64);
                }

                for (int i = lenght - 1; i >= 0; i--)
                {
                    list[i] = 32;
                }
            }

            return list;
        }
    }
}