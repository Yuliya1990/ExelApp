using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExelApp
{
    class LabCalculatorVisitor: LabCalculatorBaseVisitor<double>
    {
        Dictionary<string, double> tableIdentifier = new Dictionary<string, double>();
        public override double VisitCompileUnit(LabCalculatorParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitNumberExpr(LabCalculatorParser.NumberExprContext context)
        {
            var result = double.Parse(context.GetText());
            Debug.WriteLine(result);
            return result;
        }
        //IdentifierExpr
        public override double VisitIdentifierExpr(LabCalculatorParser.IdentifierExprContext context)
        {
            var result = context.GetText();
            double value;
            //видобути значення змінної з таблиці
            if (tableIdentifier.TryGetValue(result.ToString(), out value))
            {
                return value;
            }
            else
            {
                return 0.0;
            }
        }
        public override double VisitParenthesizedExpr(LabCalculatorParser.ParenthesizedExprContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitExponentialExpr(LabCalculatorParser.ExponentialExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            Debug.WriteLine("{0} ^ {1}", left, right);
            return System.Math.Pow(left, right);
        }

        public override double VisitAdditiveExpr(LabCalculatorParser.AdditiveExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == LabCalculatorLexer.ADD)
            {
                Debug.WriteLine("{0} + {1}", left, right);
                return left + right;
            }
            else //LabCalculatorLexer.SUBTRACT
            {
                Debug.WriteLine("{0} - {1}", left, right);
                return left - right;
            }
        }

        public override double VisitMultiplicativeExpr(LabCalculatorParser.MultiplicativeExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == LabCalculatorLexer.MULTIPLY)
            {
                Debug.WriteLine("{0} * {1}", left, right);
                return left * right;
            }
            else //LabCalculatorLexer.DIVIDE
            {
                Debug.WriteLine("{0} / {1}", left, right);
                return left / right;
            }
        }

        public override double VisitMaxExpr([NotNull] LabCalculatorParser.MaxExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            Debug.WriteLine("max({0},{1})", left, right);
            return Math.Max(left, right);
        }
        public override double VisitMinExpr([NotNull] LabCalculatorParser.MinExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            Debug.WriteLine("min{0},{1}", left, right);
            return Math.Min(left, right);
        }

        public override double VisitModExpr([NotNull] LabCalculatorParser.ModExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            return left % right;
        }
        public override double VisitDivExpr([NotNull] LabCalculatorParser.DivExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            return (int)(left/right);
        }

        public override double VisitComparativeSevereExpr([NotNull] LabCalculatorParser.ComparativeSevereExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if(context.operatorToken.Type==LabCalculatorLexer.LESS)
            {
                Debug.WriteLine("{0}<{1}", left, right);
                if (left < right) return 1;
                else return 0;
            }
            else
            {
                Debug.WriteLine("{0}>{1}", left, right);
                if (left > right) return 1;
                else return 0;
            }
        }
        public override double VisitComparativeExpr([NotNull] LabCalculatorParser.ComparativeExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            
            if(context.operatorToken.Type==LabCalculatorLexer.LESS_OR_EQUAL)
            {
                Debug.WriteLine("{0}<={1}", left, right);
                if (left <= right) return 1;
                else return 0;
            }
            else
            {
                Debug.WriteLine("{0}>={1}", left, right);
                if (left >= right) return 1;
                else return 0;
            }
        }

        public override double VisitAssignmentExpr([NotNull] LabCalculatorParser.AssignmentExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (context.operetorToken.Type==LabCalculatorLexer.ASSIGNMENT)
            {
                Debug.WriteLine("{0}={1}", left, right);
                if (left == right) return 1;
                else return 0;
            }
            else
            {
                Debug.WriteLine("{0}<>{1}", left, right);
                if (left != right) return 1;
                else return 0;
            }
            return base.VisitAssignmentExpr(context);
        }

        private double WalkLeft(LabCalculatorParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<LabCalculatorParser.ExpressionContext>(0));
        }
        private double WalkRight(LabCalculatorParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<LabCalculatorParser.ExpressionContext>(1));
        }
    }
}
