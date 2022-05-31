using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> target, int count)
        {
            Random r = new Random();

            return target.OrderBy(x => (r.Next())).Take(count);
        }
    }

    public static class CharExtension
    {
        public static char Next(this char instancia)
        {
            int numero = instancia;

            numero++;

            return (char)numero;
        }
    }
}
