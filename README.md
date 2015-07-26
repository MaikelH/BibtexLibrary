## BibtexLibrary
A .Net library for working with bibtex files. Contains custom parser for reading bibtex files.

### Grammar

The grammar that is used by the parser is as follows. It does is not in perfect EBNF, but it should be usable.

```
BibtexFile 		= 	{ ([junk] @ entry) }
entry 			=	type openingbrace key comma {(tag[comma])} closingbrace 
tag				= 	text "=" openingbrace text closingbrace


type			=	text
key				= 	text

comma 			=	","
openingbrace	=	"{"
closingbrace	=	"}"
text			= 	{ ([A-z0-9:.\s-] | Comma) }
junk			=   .*
```
