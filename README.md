## BibtexLibrary
A .Net library for working with bibtex files. Contains custom parser for reading bibtex files.

### Build status

master : [![Build status](https://ci.appveyor.com/api/projects/status/25texnbx5r6g4wi1/branch/master?svg=true)](https://ci.appveyor.com/project/MaikelH/bibtexlibrary/branch/master)

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
text			= 	{ ([A-z0-9:.\s-()] | Comma) }
junk			=   .*
```
