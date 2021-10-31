grammar LabCalculator;

/*
 * Parser Rules
 */

compileUnit : expression EOF;
expression :
	MAX LPAREN ex1 = expression COMMA ex2 = expression RPAREN #MaxExpr
	| MIN LPAREN ex1 = expression COMMA ex2 = expression RPAREN #MinExpr
	| ex1 = expression MOD ex2 = expression #ModExpr
	| ex1 = expression DIV ex2 = expression #DivExpr
	| LPAREN expression RPAREN #ParenthesizedExpr
	| expression EXPONENT expression #ExponentialExpr
    | expression operatorToken=(MULTIPLY | DIVIDE) expression #MultiplicativeExpr
	| expression operatorToken=(ADD | SUBTRACT) expression #AdditiveExpr
	| ex1 = expression operatorToken=(LESS | LARGER) expression #ComparativeSevereExpr
	| ex1 = expression operatorToken=(LESS_OR_EQUAL | LARGER_OR_EQUAL) expression #ComparativeExpr
	| ex1 = expression operetorToken=(ASSIGNMENT | NOT_EQUAL) ex2 = expression #AssignmentExpr
	| NUMBER #NumberExpr
	| IDENTIFIER #IdentifierExpr
	; 

/*
 * Lexer Rules
 */

NUMBER : INT ('.' INT)?; 
IDENTIFIER : [a-zA-Z]+[1-9][0-9]+; 

INT : ('0'..'9')+;
COMMA: ',';

ADD : '+';
SUBTRACT : '-';
MULTIPLY : '*';
DIVIDE : '/';
MOD: 'mod';
DIV: 'div';
MAX: 'max';
MIN: 'min';
ASSIGNMENT: '=';
LESS: '<';
LARGER: '>';
LESS_OR_EQUAL: '<=';
MORE_OR_EQUAL: '>=';
NOT_EQUAL: '<>';


EXPONENT : '^';
LPAREN : '(';
RPAREN : ')';

WS : [ \t\r\n] -> channel(HIDDEN);
