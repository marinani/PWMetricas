using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Enumerators
{
    public abstract class Enumerador : IComparable
    {
        private readonly int valor;
        private readonly string descricao;

        protected Enumerador()
        {
        }

        protected Enumerador(int valor, string descricao)
        {
            this.valor = valor;
            this.descricao = descricao;
        }

        public int? Valor => valor;

        public string? Descricao => descricao ?? string.Empty;

        public override string ToString() => Descricao ?? string.Empty;


        public static IEnumerable<T> BuscarTodos<T>() where T : Enumerador
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var locatedValue = info.GetValue(null) as T;

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Enumerador otherValue)
                return false;


            var typeMatches = GetType() == obj.GetType();
            var valueMatches = valor == otherValue.valor;

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => valor.GetHashCode();

        public static int AbsoluteDifference(Enumerador firstValue, Enumerador secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.valor - secondValue.valor);
            return absoluteDifference;
        }

        public static T BuscarPorValor<T>(int? value) where T : Enumerador?
        {
            if (!value.HasValue)
                return null;

            var matchingItem = BuscarTodos<T>().FirstOrDefault(item => item.Valor == value.Value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumerador
        {
            var matchingItem = BuscarTodos<T>().FirstOrDefault(item => item.Descricao == displayName);
            return matchingItem;
        }

        public int CompareTo(object? other) =>
                valor.CompareTo(((Enumerador)other).valor);
    }

}
