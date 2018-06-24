using BibtexLibrary.Parser.Nodes;
using BibtexLibrary.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibtexLibrary.Parser
{
    public class BibtexParser
    {
        private readonly Tokenizer.Tokenizer _tokenizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BibtexParser"/> class.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        public BibtexParser(Tokenizer.Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        /// <summary>
        /// Parses this instance.
        /// </summary>
        /// <returns></returns>
        public BibtexFile Parse()
        {
            ParseNode node = ParseInput(_tokenizer);
            BibtexFile fileObject = convertParseNode(node);

            return fileObject;
        }

        /// <summary>
        /// Converts the parse node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private BibtexFile convertParseNode(ParseNode node)
        {
            Nodes.BibtexFile parseFile = (Nodes.BibtexFile)node;

            BibtexFile bibtex = new BibtexFile();

            foreach (Entry entry in parseFile.Entries)
            {
                if (entry.Type == "String")
                {
                    bibtex.StringDefinitions.Add(entry.Tags.First().Key, entry.Tags.First().Value);
                }
                else
                {
                    BibtexEntry bibtexEntry = new BibtexEntry { Key = entry.Key, Type = entry.Type };

                    entry.Tags.ToList().ForEach(x => bibtexEntry.Tags.Add(x.Key, x.Value));

                    bibtex.Entries.Add(bibtexEntry);
                }
            }

            return bibtex;
        }

        /// <summary>
        /// Parses the input.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns></returns>
        private ParseNode ParseInput(Tokenizer.Tokenizer tokenizer)
        {
            Nodes.BibtexFile file = new Nodes.BibtexFile();

            while (!tokenizer.EndOfInput)
            {
                AbstractToken token = tokenizer.NextToken();

                if (token.GetType() == typeof(At))
                {
                    file.Entries.Add(Entry(tokenizer));
                }
            }

            return file;
        }

        /// <summary>
        /// Entries the specified tokenizer.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns></returns>
        private Entry Entry(Tokenizer.Tokenizer tokenizer)
        {
            Entry entry = new Entry();

            entry.Type = Text(tokenizer);
            OpeningBrace(tokenizer);

            if (!entry.Type.Equals("String", StringComparison.OrdinalIgnoreCase))
            {
                entry.Key = Text(tokenizer);
                Comma(tokenizer);
            }

            entry.Tags = Tags(tokenizer);
            ClosingBrace(tokenizer);

            return entry;
        }

        /// <summary>
        /// Texts the specified tokenizer.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns></returns>
        /// <exception cref="ParseException">Expected type Text but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25)</exception>
        private String Text(Tokenizer.Tokenizer tokenizer)
        {
            AbstractToken token = tokenizer.NextToken();

            if (token.GetType() == typeof(Comma) || token.GetType() == typeof(Text))
            {
                return token.GetValue();
            }

            throw new ParseException("Expected type Text but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25));
        }

        /// <summary>
        /// Openings the brace.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <exception cref="ParseException">Expected type OpeningBrace but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25)</exception>
        private void OpeningBrace(Tokenizer.Tokenizer tokenizer)
        {
            AbstractToken token = tokenizer.NextToken();

            if (token.GetType() == typeof(OpeningBrace))
            {
                return;
            }

            throw new ParseException("Expected type OpeningBrace but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25));
        }

        /// <summary>
        /// Closings the brace.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <exception cref="ParseException">Expected type ClosingBrace but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25)</exception>
        private void ClosingBrace(Tokenizer.Tokenizer tokenizer)
        {
            AbstractToken token = tokenizer.NextToken();

            if (token.GetType() == typeof(ClosingBrace))
            {
                return;
            }

            throw new ParseException("Expected type ClosingBrace but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25));
        }

        /// <summary>
        /// Commas the specified tokenizer.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <param name="optional">if set to <c>true</c> [optional].</param>
        /// <exception cref="ParseException">Expected type Comma but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25)</exception>
        private void Comma(Tokenizer.Tokenizer tokenizer, Boolean optional = false)
        {
            AbstractToken token = tokenizer.Peek();

            if (token.GetType() == typeof(Comma))
            {
                tokenizer.NextToken();
                return;
            }

            if (optional)
            {
                return;
            }

            throw new ParseException("Expected type Comma but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25));
        }

        public static Boolean MUTE_PEEK_EXCEPTIONS = true;
        public static Int32 PARSER_EXCEPTIONS_LIMIT = 0;

        public List<Exception> ParsingExceptions { get; set; } = new List<Exception>();


        /// <summary>
        /// Retrieves the tag values from the input.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns></returns>
        private ICollection<Tag> Tags(Tokenizer.Tokenizer tokenizer)
        {
            // This function needs some refactoring.
            List<Tag> tags = new List<Tag>();

            while (tokenizer.Peek().GetType() != typeof(ClosingBrace))
            {
                Tag tag = new Tag { Key = Text(tokenizer) };
                Equals(tokenizer);
                AbstractToken startToken = ValueStart(tokenizer);

                List<AbstractToken> tokens = new List<AbstractToken>();

                bool keepProcessing = true;
                int balance = 1;

                while (keepProcessing)
                {
                    Type nextTokenType = null;

                    // ------------------------------------ format failure protection
                    if (!MUTE_PEEK_EXCEPTIONS)
                    {
                        nextTokenType = tokenizer.Peek().GetType();
                    }
                    else
                    {
                        try
                        {
                            nextTokenType = tokenizer.Peek().GetType();
                        }
                        catch (Exception ex)
                        {
                            ParsingExceptions.Add(ex);
                            if (ParsingExceptions.Count() > PARSER_EXCEPTIONS_LIMIT)
                            {
                                throw;
                            }
                        }
                    }

                    if (nextTokenType == null)
                    {
                        tokenizer.NextToken();

                        if (ParsingExceptions.Count() > PARSER_EXCEPTIONS_LIMIT)
                        {
                            keepProcessing = false;

                        }

                        continue;

                    }

                    // ------------------------------------ format failure protection



                    if (nextTokenType == typeof(OpeningBrace))
                    {
                        balance++;
                    }

                    if ((startToken.GetType() == typeof(OpeningBrace) && nextTokenType == typeof(ClosingBrace)))
                    {
                        if (balance == 1)
                        {
                            keepProcessing = false;
                            ValueStop(tokenizer);
                        }
                    }

                    if (nextTokenType == typeof(ClosingBrace))
                    {
                        if (balance > 1)
                        {
                            balance--;
                        }
                    }

                    // Double quotes are much more difficult to handle then the braces. The problem is that there is no distinction between 
                    // start and stop quotes. This means we need to look forward to see what is behind the quote to see if it is a quote @ the end
                    // or the start of a new quote.
                    if (nextTokenType == typeof(ValueQuote))
                    {
                        AbstractToken quote = tokenizer.NextToken();

                        Type nextType = tokenizer.Peek().GetType();
                        if ((nextType == typeof(ClosingBrace) && balance == 1) ||
                             nextType == typeof(Comma))
                        {
                            // end of line found.
                            keepProcessing = false;
                        }
                        else
                        {
                            tokens.Add(quote);
                            continue;
                        }
                    }

                    if (keepProcessing)
                    {
                        tokens.Add(tokenizer.NextToken());
                    }
                }

                tag.Value = tokens.Aggregate("", (s, token) => s + token.RawValue);

                Comma(tokenizer, true);
                NewLine(tokenizer, true);

                tags.Add(tag);
            }

            return tags;
        }

        private void Equals(Tokenizer.Tokenizer tokenizer)
        {
            AbstractToken token = tokenizer.NextToken();

            if (token.GetType() == typeof(Equals))
            {
                return;
            }

            throw new ParseException("Expected type Equals but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25));
        }

        private AbstractToken ValueStart(Tokenizer.Tokenizer tokenizer)
        {
            AbstractToken token = tokenizer.NextToken();

            if (token.GetType() == typeof(OpeningBrace) || token.GetType() == typeof(ValueQuote))
            {
                return token;
            }

            throw new ParseException("Expected type Openingbrace or ValueQuote but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25));
        }

        private AbstractToken ValueStop(Tokenizer.Tokenizer tokenizer)
        {
            AbstractToken token = tokenizer.NextToken();

            if (token.GetType() == typeof(ClosingBrace) || token.GetType() == typeof(ValueQuote))
            {
                return token;
            }

            throw new ParseException("Expected type ClosingBrace or ValueQuote but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25));
        }

        private void NewLine(Tokenizer.Tokenizer tokenizer, Boolean optional = false)
        {
            AbstractToken token = tokenizer.Peek();

            if (token.GetType() == typeof(NewLine))
            {
                tokenizer.NextToken();
                return;
            }

            if (optional)
            {
                return;
            }

            throw new ParseException("Expected type Comma but found: " + token.GetType() + " after " + tokenizer.GetPreviousCharacters(25));
        }
    }
}
