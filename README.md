## BibtexLibrary
A .Net library for working with bibtex files. Contains custom parser for reading bibtex files.

### Build status

master: [![Build status](https://ci.appveyor.com/api/projects/status/25texnbx5r6g4wi1/branch/master?svg=true)](https://ci.appveyor.com/project/MaikelH/bibtexlibrary/branch/master)
release:[![Build status](https://ci.appveyor.com/api/projects/status/3elfat52waky5yah/branch/release?svg=true)](https://ci.appveyor.com/project/MaikelH/bibtexlibrary-flgmq/branch/release)


### Usage

Currently only loading from a string is supported.

```C#
BibtexFile file = BibtexLibrary.BibtexImporter.FromString(@"@book{ aaker:1981a,
                                                                      author = {David A. Aaker},
                                                                      title = {Multivariate Analysis in Marketing},
                                                                      edition = {2},
                                                                      publisher = {The Scientific Press},
                                                                      year = {1981},
                                                                      address = {Palo Alto},
                                                                      topic = {multivariate-statistics;market-research;}
                                                                     }");

foreach (BibtexEntry entry in file.Entries)
{
    Console.WriteLine(entry.Tags["author"]);
}
```

The above code will print:
```
David A. Aaker
```


### Grammar

The grammar that is used by the parser is as follows. It is not in perfect EBNF, but it should be usable.

```
BibtexFile 		= 	{ ([junk] @ entry) }
entry 			=	type openingbrace key comma {(tag[comma])} closingbrace 
tag				= 	text "=" valuestart text valuestop


type			=	text
key				= 	text

valuestart      =   openingbrace | valuequote
valuestop       =   closingbrace | valuequote

comma 			=	","
openingbrace	=	"{"
closingbrace	=	"}"
valuequote      =   """
text			= 	{ ([A-z0-9:.\s-()/\?&\\] | Comma) }
junk			=   .*
```
