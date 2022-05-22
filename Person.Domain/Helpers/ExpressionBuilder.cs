using Person.Domain.SeedWork;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Person.Domain.Helpers
{
    public sealed class ExpressionBuilder<T> where T : Entity
    {
        private readonly string[] _expressions;

        private readonly Dictionary<string, Func<Expression, Expression, BinaryExpression>> _operadores = new()
        {
            { "<", (left, right) => Expression.LessThan(left, right) },
            { ">", (left, right) => Expression.GreaterThan(left, right) },
            { "=", (left, right) => Expression.Equal(left, right) },
            { "!", (left, right) => Expression.NotEqual(left, right) }
        };

        public ExpressionBuilder(string[] expressions)
        {
            _expressions = expressions;
        }

        public Expression<Func<T, bool>> Build()
        {
            var expressionQueue = ToTuple(SeparateExpressions(_expressions));
            List<BinaryExpression> expressionTree = new();

            //Crea el parámetro de inicio de la expresión lambda EJ: "entity =>"
            var parameter = Expression.Parameter(typeof(T), "entity");

            //Recorre la cola de expresiones hasta que no tenga mas elementos
            while (!(expressionQueue.Count <= 0))
            {
                var expression = expressionQueue.Dequeue();

                //Establece la propiedad que posteriormente va a ser utilizada en la expresión lambda. EJ: "entity.Age"
                var property = Expression.Property(parameter, expression.property);

                //Establece el valor que posteriormente va a ser utilizado en la expresión lambda. EJ: "30"
                //TODO: Verificar si es necesario convertir el valor a tipo de dato de la propiedad (REFACTORIZAR.)
                var condition = _operadores[expression.@operator](
                    property,
                    Expression.Constant(
                        int.TryParse(expression.value, out var value) ? value : expression.value
                    )
                );

                expressionTree.Add(condition);
            }

            //Crea la expresión lambda con el parámetro de inicio y la lista de expresiones. EJ: "entity => entity.Age > 30 && entity.Age < 40"
            return Expression.Lambda<Func<T, bool>>(
                expressionTree.Aggregate((left, right) => Expression.And(left, right)),
                parameter
            );
        }

        private static string[] SeparateExpressions(string[] expressions)
        {
            return expressions.Select(str => str.Replace("filter=", "")).ToArray();
        }

        private static Queue<(string property, string @operator, string value)> ToTuple(
            string[] expressions
        )
        {
            Queue<(string, string, string)> queue = new();

            foreach (var item in expressions)
            {
                var sep = Regex.Split(item, @"(=|>|<|>=|<=|!)", RegexOptions.IgnoreCase);

                if (!(sep.Length is < 0 or > 3))
                    queue.Enqueue((sep[0], sep[1], sep[2]));
            }

            return queue;
        }
    }
}
