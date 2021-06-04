// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using GCore.Source.Extensions;

namespace GCore.Source
{
    public class CodeWriter : TextWriter
    {
        private static readonly char[] NewLineCharacters = { '\r', '\n' };

        private readonly StringBuilder _builder;

        private int _absoluteIndex;
        private int _currentLineIndex;
        private int _currentLineCharacterIndex;

        private int _skippedIndents;

        private Stack<ICodeContext> _contextStacks = new Stack<ICodeContext>();

        public CodeWriter() : base( CultureInfo.InvariantCulture ) {
            base.NewLine = Environment.NewLine;
            _builder = new StringBuilder();
        }

        public int CurrentIndent { get; set; }

        public int Length => _builder.Length;

        public override Encoding Encoding { get => Encoding.Unicode; }

        public SourceLocation Location => new SourceLocation(_absoluteIndex, _currentLineIndex, _currentLineCharacterIndex);

        public ICodeContext? CurrentContext
        {
            get => _contextStacks.Count > 0 ? _contextStacks.Peek() : null;
        }

        public Stack<ICodeContext> ContextStack
        {
            get => _contextStacks;
        }

        public bool IsNewLine => Length == 0 || this[Length - 1] == '\n';

        public void PushContext(ICodeContext context) 
            => _contextStacks.Push(context);

        public ICodeContext PopContext() 
            => _contextStacks.Pop();

        public char this[int index] {
            get {
                if (index < 0 || index >= _builder.Length) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                return _builder[index];
            }
        }
        
        internal CodeWriter Indent(int size) {
            if (IsNewLine) {
                _builder.Append(' ', size);

                _currentLineCharacterIndex += size;
                _absoluteIndex += size;
            }

            return this;
        }

        public override void Write(char value)
        {
            Write(new string(value, 1), 0, 1);
        }

        public CodeWriter Write(string? value) {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            return Write(value, 0, value.Length);
        }

        public CodeWriter Write(string value, int startIndex, int count)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            if (count == 1)
                return WriteInternal(value, startIndex, count);

            var lines = value.Substring(startIndex, count).SplitNewLine();
            var lastIndex = lines.Length - 1;

            for (int i = 0; i < lines.Length; i++)
            {
                WriteInternal(lines[i], 0, lines[i].Length);

                if (i < lastIndex)
                    WriteLine();
            }

            return this;
        }

        internal CodeWriter WriteInternal(string value, int startIndex, int count) {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            if (startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (startIndex > value.Length - count) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (count == 0) {
                return this;
            }

            if(CurrentIndent > 0)
                Indent(CurrentIndent);
            else if (CurrentIndent < 0)
            {
                if (IsNewLine)
                {
                    while (count > 0 && _skippedIndents < -CurrentIndent && value[startIndex] == ' ')
                    {
                        startIndex++;
                        count--;
                        _skippedIndents++;
                    }
                }

                if (count == 0)
                    return this;
            }

            _builder.Append(value, startIndex, count);

            _absoluteIndex += count;

            // The data string might contain a partial newline where the previously
            // written string has part of the newline.
            var i = startIndex;
            int? trailingPartStart = null;

            if (
                // Check the last character of the previous write operation.
                _builder.Length - count - 1 >= 0 &&
                _builder[_builder.Length - count - 1] == '\r' &&

                // Check the first character of the current write operation.
                _builder[_builder.Length - count] == '\n') {
                // This is newline that's spread across two writes. Skip the first character of the
                // current write operation.
                //
                // We don't need to increment our newline counter because we already did that when we
                // saw the \r.
                i += 1;
                trailingPartStart = 1;
            }

            // Iterate the string, stopping at each occurrence of a newline character. This lets us count the
            // newline occurrences and keep the index of the last one.
            while ((i = value.IndexOfAny(NewLineCharacters, i)) >= 0) {
                // Newline found.
                _currentLineIndex++;
                _currentLineCharacterIndex = 0;
                _skippedIndents = 0;

                i++;

                // We might have stopped at a \r, so check if it's followed by \n and then advance the index to
                // start the next search after it.
                if (count > i &&
                    value[i - 1] == '\r' &&
                    value[i] == '\n') {
                    i++;
                }

                // The 'suffix' of the current line starts after this newline token.
                trailingPartStart = i;
            }

            if (trailingPartStart == null) {
                // No newlines, just add the length of the data buffer
                _currentLineCharacterIndex += count;
            } else {
                // Newlines found, add the trailing part of 'data'
                _currentLineCharacterIndex += (count - trailingPartStart.Value);
            }

            return this;
        }

        public CodeWriter WriteLine() {
            _builder.Append(NewLine);

            _currentLineIndex++;
            _currentLineCharacterIndex = 0;
            _absoluteIndex += NewLine.Length;
            _skippedIndents = 0;

            return this;
        }

        public CodeWriter WriteLine(string? value) {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            return Write(value).WriteLine();
        }

        public string GenerateCode() {
            return _builder.ToString();
        }

        public static string ToLiteral(string input) {
            StringBuilder literal = new StringBuilder(input.Length + 2);
            literal.Append("\"");
            foreach (var c in input) {
                switch (c) {
                    case '\'': literal.Append(@"\'"); break;
                    case '\"': literal.Append("\\\""); break;
                    case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\0"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        // ASCII printable character
                        if (c >= 0x20 && c <= 0x7e) {
                            literal.Append(c);
                            // As UTF16 escaped character
                        } else {
                            literal.Append(@"\u");
                            literal.Append(((int)c).ToString("x4"));
                        }
                        break;
                }
            }
            literal.Append("\"");
            return literal.ToString();
        }
    }
}