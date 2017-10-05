/*
* Licensed to the Apache Software Foundation (ASF) under one
* or more contributor license agreements.  See the NOTICE file
* distributed with this work for additional information
* regarding copyright ownership.  The ASF licenses this file
* to you under the Apache License, Version 2.0 (the
* "License"); you may not use this file except in compliance
* with the License.  You may obtain a copy of the License at
*
*   http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing,
* software distributed under the License is distributed on an
* "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
* KIND, either express or implied.  See the License for the
* specific language governing permissions and limitations
* under the License.    
*/

namespace NVelocity.Runtime.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class ParserTokenManager : ParserConstants
    {
        private void InitBlock()
        {
            debugStream = Console.Out;
        }
  
        public TextWriter DebugStream
        {
            set
            {
                debugStream = value;
            }

        }
        public Token NextToken
        {
            get
            {
                int kind;
                Token specialToken = null;
                Token matchedToken;
                int curPos = 0;

                for (; ; )
                {
                    try
                    {
                        curChar = input_stream.BeginToken();
                    }
                    catch (IOException e)
                    {
                        jjmatchedKind = 0;
                        matchedToken = jjFillToken();
                        matchedToken.SpecialToken = specialToken;
                        return matchedToken;
                    }
                    image = null;
                    jjimageLen = 0;

                    for (; ; )
                    {
                        switch (curLexState)
                        {

                            case 0:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_0();
                                break;

                            case 1:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_1();
                                if (jjmatchedPos == 0 && jjmatchedKind > 66)
                                {
                                    jjmatchedKind = 66;
                                }
                                break;

                            case 2:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_2();
                                if (jjmatchedPos == 0 && jjmatchedKind > 66)
                                {
                                    jjmatchedKind = 66;
                                }
                                break;

                            case 3:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_3();
                                break;

                            case 4:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_4();
                                if (jjmatchedPos == 0 && jjmatchedKind > 66)
                                {
                                    jjmatchedKind = 66;
                                }
                                break;

                            case 5:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_5();
                                if (jjmatchedPos == 0 && jjmatchedKind > 67)
                                {
                                    jjmatchedKind = 67;
                                }
                                break;

                            case 6:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_6();
                                if (jjmatchedPos == 0 && jjmatchedKind > 25)
                                {
                                    jjmatchedKind = 25;
                                }
                                break;

                            case 7:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_7();
                                if (jjmatchedPos == 0 && jjmatchedKind > 25)
                                {
                                    jjmatchedKind = 25;
                                }
                                break;

                            case 8:
                                jjmatchedKind = 0x7fffffff;
                                jjmatchedPos = 0;
                                curPos = jjMoveStringLiteralDfa0_8();
                                if (jjmatchedPos == 0 && jjmatchedKind > 25)
                                {
                                    jjmatchedKind = 25;
                                }
                                break;
                        }
                        if (jjmatchedKind != 0x7fffffff)
                        {
                            if (jjmatchedPos + 1 < curPos)
                                input_stream.Backup(curPos - jjmatchedPos - 1);
                            if ((jjtoToken[jjmatchedKind >> 6] & (ulong)(1L << (jjmatchedKind & 63))) != 0L)
                            {
                                matchedToken = jjFillToken();
                                matchedToken.SpecialToken = specialToken;
                                TokenLexicalActions(matchedToken);
                                if (jjnewLexState[jjmatchedKind] != -1)
                                    curLexState = jjnewLexState[jjmatchedKind];
                                return matchedToken;
                            }
                            else if ((jjtoSkip[jjmatchedKind >> 6] & (1L << (jjmatchedKind & 63))) != 0L)
                            {
                                if ((jjtoSpecial[jjmatchedKind >> 6] & (1L << (jjmatchedKind & 63))) != 0L)
                                {
                                    matchedToken = jjFillToken();
                                    if (specialToken == null)
                                        specialToken = matchedToken;
                                    else
                                    {
                                        matchedToken.SpecialToken = specialToken;
                                        specialToken = (specialToken.Next = matchedToken);
                                    }
                                    SkipLexicalActions(matchedToken);
                                }
                                else
                                    SkipLexicalActions(null);
                                if (jjnewLexState[jjmatchedKind] != -1)
                                    curLexState = jjnewLexState[jjmatchedKind];
                             
                                goto EOFLoop;
                            }
                            MoreLexicalActions();
                            if (jjnewLexState[jjmatchedKind] != -1)
                                curLexState = jjnewLexState[jjmatchedKind];
                            curPos = 0;
                            jjmatchedKind = 0x7fffffff;
                            try
                            {
                                curChar = input_stream.ReadChar();
                                continue;
                            }
                            catch (System.IO.IOException e1)
                            {
                            }
                        }
                        int error_line = input_stream.EndLine;
                        int error_column = input_stream.EndColumn;
                        System.String error_after = null;
                        bool EOFSeen = false;
                        try
                        {
                            input_stream.ReadChar(); input_stream.Backup(1);
                        }
                        catch (System.IO.IOException e1)
                        {
                            EOFSeen = true;
                            error_after = curPos <= 1 ? "" : input_stream.GetImage();
                            if (curChar == '\n' || curChar == '\r')
                            {
                                error_line++;
                                error_column = 0;
                            }
                            else
                                error_column++;
                        }
                        if (!EOFSeen)
                        {
                            input_stream.Backup(1);
                            error_after = curPos <= 1 ? "" : input_stream.GetImage();
                        }
                        throw new TokenMgrError(EOFSeen, curLexState, error_line, error_column, error_after, curChar, TokenMgrError.LEXICAL_ERROR);
                    }

                EOFLoop: ;
                }
            }

        }
        private int fileDepth = 0;

        private int lparen = 0;
        private int rparen = 0;

        internal Stack<Dictionary<string, int>> stateStack = new Stack<Dictionary<string, int>>();
        public bool debugPrint = false;

        private bool inReference;
        public bool inDirective;
        private bool inComment;
        public bool inSet;

        /// <summary>  pushes the current state onto the 'state stack',
        /// and maintains the parens counts
        /// public because we need it in PD & VM handling
        /// 
        /// </summary>
        /// <returns> boolean : success.  It can fail if the state machine
        /// gets messed up (do don't mess it up :)
        /// </returns>
        public virtual bool stateStackPop()
        {
            Dictionary<string, int> h;

            try
            {
                h = stateStack.Pop();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                lparen = 0;
                SwitchTo(ParserConstants.DEFAULT);
                return false;
            }

            if (debugPrint)
                System.Console.Out.WriteLine(" stack pop (" + stateStack.Count + ") : lparen=" + ((System.Int32)h["lparen"]) + " newstate=" + ((System.Int32)h["lexstate"]));

            lparen = ((System.Int32)h["lparen"]);
            rparen = ((System.Int32)h["rparen"]);

            SwitchTo(((System.Int32)h["lexstate"]));

            return true;
        }

        /// <summary>  pops a state off the stack, and restores paren counts
        /// 
        /// </summary>
        /// <returns> boolean : success of operation
        /// </returns>
        public virtual bool stateStackPush()
        {
            if (debugPrint)
                System.Console.Out.WriteLine(" (" + stateStack.Count + ") pushing cur state : " + curLexState);

            Dictionary<string, int> h = new Dictionary<string, int>();

            h["lexstate"] = curLexState;
            h["lparen"] = lparen;
            h["rparen"] = rparen;

            lparen = 0;

            stateStack.Push(h);

            return true;
        }

        /// <summary>  Clears all state variables, resets to
        /// start values, clears stateStack.  Call
        /// before parsing.
        /// </summary>
        /// <returns> void
        /// </returns>
        public virtual void clearStateVars()
        {
            stateStack.Clear();

            lparen = 0;
            rparen = 0;
            inReference = false;
            inDirective = false;
            inComment = false;
            inSet = false;

            return;
        }

        /// <summary>  handles the dropdown logic when encountering a RPAREN</summary>
        private void RPARENHandler()
        {
            /*
            *  Ultimately, we want to drop down to the state below
            *  the one that has an open (if we hit bottom (DEFAULT),
            *  that's fine. It's just text schmoo.
            */

            bool closed = false;

            if (inComment)
                closed = true;

            while (!closed)
            {
                /*
                * look at current state.  If we haven't seen a lparen
                * in this state then we drop a state, because this
                * lparen clearly closes our state
                */

                if (lparen > 0)
                {
                    /*
                    *  if rparen + 1 == lparen, then this state is closed.
                    * Otherwise, increment and keep parsing
                    */

                    if (lparen == rparen + 1)
                    {
                        stateStackPop();
                    }
                    else
                    {
                        rparen++;
                    }

                    closed = true;
                }
                else
                {
                    /*
                    * now, drop a state
                    */

                    if (!stateStackPop())
                        break;
                }
            }
        }
    
        public TextWriter debugStream;
        private int jjStopStringLiteralDfa_0(int pos, long active0)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x10L) != 0L)
                        return 58;
                    if ((active0 & 0x80000000L) != 0L)
                        return 101;
                    if ((active0 & 0x40L) != 0L)
                        return 65;
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 57;
                        return 63;
                    }
                    if ((active0 & 0x200000000000L) != 0L)
                        return 50;
                    if ((active0 & 0x70000L) != 0L)
                        return 7;
                    return -1;

                case 1:
                    if ((active0 & 0x10000L) != 0L)
                        return 5;
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 57;
                        jjmatchedPos = 1;
                        return 63;
                    }
                    return -1;

                case 2:
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 57;
                        jjmatchedPos = 2;
                        return 63;
                    }
                    return -1;

                case 3:
                    if ((active0 & 0x10000000L) != 0L)
                        return 63;
                    if ((active0 & 0x20000000L) != 0L)
                    {
                        jjmatchedKind = 57;
                        jjmatchedPos = 3;
                        return 63;
                    }
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_0(int pos, long active0)
        {
            return jjMoveNfa_0(jjStopStringLiteralDfa_0(pos, active0), pos + 1);
        }
        private int jjStopAtPos(int pos, int kind)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            return pos + 1;
        }
        private int jjStartNfaWithStates_0(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_0(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_0()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_0(0x50000L);

                case (char)(37):
                    return jjStopAtPos(0, 35);

                case (char)(40):
                    return jjStopAtPos(0, 8);

                case (char)(42):
                    return jjStopAtPos(0, 33);

                case (char)(43):
                    return jjStopAtPos(0, 32);

                case (char)(44):
                    return jjStopAtPos(0, 3);

                case (char)(45):
                    return jjStartNfaWithStates_0(0, 31, 101);

                case (char)(46):
                    return jjMoveStringLiteralDfa1_0(0x10L);

                case (char)(47):
                    return jjStopAtPos(0, 34);

                case (char)(58):
                    return jjStopAtPos(0, 5);

                case (char)(61):
                    return jjStartNfaWithStates_0(0, 45, 50);

                case (char)(91):
                    return jjStopAtPos(0, 1);

                case (char)(93):
                    return jjStopAtPos(0, 2);

                case (char)(102):
                    return jjMoveStringLiteralDfa1_0(0x20000000L);

                case (char)(116):
                    return jjMoveStringLiteralDfa1_0(0x10000000L);

                case (char)(123):
                    return jjStartNfaWithStates_0(0, 6, 65);

                case (char)(125):
                    return jjStopAtPos(0, 7);

                default:
                    return jjMoveNfa_0(0, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_0(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_0(0, active0);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_0(1, 16, 5);
                    break;

                case (char)(46):
                    if ((active0 & 0x10L) != 0L)
                        return jjStopAtPos(1, 4);
                    break;

                case (char)(97):
                    return jjMoveStringLiteralDfa2_0(active0, 0x20000000L);

                case (char)(114):
                    return jjMoveStringLiteralDfa2_0(active0, 0x10000000L);

                default:
                    break;

            }
            return jjStartNfa_0(0, active0);
        }
        private int jjMoveStringLiteralDfa2_0(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_0(0, old0);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_0(1, active0);
                return 2;
            }
            switch (curChar)
            {

                case (char)(108):
                    return jjMoveStringLiteralDfa3_0(active0, 0x20000000L);

                case (char)(117):
                    return jjMoveStringLiteralDfa3_0(active0, 0x10000000L);

                default:
                    break;

            }
            return jjStartNfa_0(1, active0);
        }
        private int jjMoveStringLiteralDfa3_0(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_0(1, old0);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_0(2, active0);
                return 3;
            }
            switch (curChar)
            {

                case (char)(101):
                    if ((active0 & 0x10000000L) != 0L)
                        return jjStartNfaWithStates_0(3, 28, 63);
                    break;

                case (char)(115):
                    return jjMoveStringLiteralDfa4_0(active0, 0x20000000L);

                default:
                    break;

            }
            return jjStartNfa_0(2, active0);
        }
        private int jjMoveStringLiteralDfa4_0(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_0(2, old0);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_0(3, active0);
                return 4;
            }
            switch (curChar)
            {

                case (char)(101):
                    if ((active0 & 0x20000000L) != 0L)
                        return jjStartNfaWithStates_0(4, 29, 63);
                    break;

                default:
                    break;

            }
            return jjStartNfa_0(3, active0);
        }
        private void jjCheckNAdd(int state)
        {
            if (jjrounds[state] != jjround)
            {
                jjstateSet[jjnewStateCnt++] = (uint)state;
                jjrounds[state] = jjround;
            }
        }
        private void jjAddStates(int start, int end)
        {
            do
            {
                jjstateSet[jjnewStateCnt++] = (uint)jjnextStates[start];
            }
            while (start++ != end);
        }
        private void jjCheckNAddTwoStates(int state1, int state2)
        {
            jjCheckNAdd(state1);
            jjCheckNAdd(state2);
        }
        private void jjCheckNAddStates(int start, int end)
        {
            do
            {
                jjCheckNAdd(jjnextStates[start]);
            }
            while (start++ != end);
        }
        private void jjCheckNAddStates(int start)
        {
            jjCheckNAdd(jjnextStates[start]);
            jjCheckNAdd(jjnextStates[start + 1]);
        }
        internal static readonly ulong[] jjbitVec0 = { 0xfffffffffffffffeL, 0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL };
        internal static readonly ulong[] jjbitVec2 = { 0x0L, 0x0L, 0xffffffffffffffffL, 0xffffffffffffffffL };
        private int jjMoveNfa_0(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 101;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 0:
                                if ((0x3ff000000000000L & l) != 0L)
                                {
                                    if (kind > 52)
                                        kind = 52;
                                    jjCheckNAddStates(0, 5);
                                }
                                else if ((0x100002600L & l) != 0L)
                                {
                                    if (kind > 26)
                                        kind = 26;
                                    jjCheckNAdd(9);
                                }
                                else if (curChar == 45)
                                    jjCheckNAddStates(6, 9);
                                else if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(73, 74);
                                }
                                else if (curChar == 46)
                                    jjCheckNAdd(58);
                                else if (curChar == 33)
                                {
                                    if (kind > 44)
                                        kind = 44;
                                }
                                else if (curChar == 61)
                                    jjstateSet[jjnewStateCnt++] = 50;
                                else if (curChar == 62)
                                    jjstateSet[jjnewStateCnt++] = 48;
                                else if (curChar == 60)
                                    jjstateSet[jjnewStateCnt++] = 45;
                                else if (curChar == 38)
                                    jjstateSet[jjnewStateCnt++] = 35;
                                else if (curChar == 39)
                                    jjCheckNAddStates(10, 12);
                                else if (curChar == 34)
                                    jjCheckNAddStates(13, 15);
                                else if (curChar == 35)
                                    jjstateSet[jjnewStateCnt++] = 7;
                                else if (curChar == 41)
                                {
                                    if (kind > 9)
                                        kind = 9;
                                    jjCheckNAddStates(16, 18);
                                }
                                if ((0x2400L & l) != 0L)
                                {
                                    if (kind > 30)
                                        kind = 30;
                                }
                                else if (curChar == 33)
                                    jjstateSet[jjnewStateCnt++] = 54;
                                else if (curChar == 62)
                                {
                                    if (kind > 40)
                                        kind = 40;
                                }
                                else if (curChar == 60)
                                {
                                    if (kind > 38)
                                        kind = 38;
                                }
                                if (curChar == 13)
                                    jjstateSet[jjnewStateCnt++] = 33;
                                break;

                            case 101:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddTwoStates(96, 97);
                                else if (curChar == 46)
                                    jjCheckNAdd(58);
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddTwoStates(90, 91);
                                if ((0x3ff000000000000L & l) != 0L)
                                {
                                    if (kind > 52)
                                        kind = 52;
                                    jjCheckNAddTwoStates(87, 89);
                                }
                                break;

                            case 1:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddStates(16, 18);
                                break;

                            case 2:
                                if ((0x2400L & l) != 0L && kind > 9)
                                    kind = 9;
                                break;

                            case 3:
                                if (curChar == 10 && kind > 9)
                                    kind = 9;
                                break;

                            case 4:
                                if (curChar == 13)
                                    jjstateSet[jjnewStateCnt++] = 3;
                                break;

                            case 5:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 6;
                                break;

                            case 6:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            case 7:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 8:
                                if (curChar == 35)
                                    jjstateSet[jjnewStateCnt++] = 7;
                                break;

                            case 9:
                                if ((0x100002600L & l) == 0L)
                                    break;
                                if (kind > 26)
                                    kind = 26;
                                jjCheckNAdd(9);
                                break;

                            case 10:
                                if (curChar == 34)
                                    jjCheckNAddStates(13, 15);
                                break;

                            case 11:
                                if ((0xfffffffbffffffffUL & (ulong)l) != 0L)
                                    jjCheckNAddStates(13, 15);
                                break;

                            case 12:
                                if (curChar == 34 && kind > 27)
                                    kind = 27;
                                break;

                            case 14:
                                if ((0x8400000000L & l) != 0L)
                                    jjCheckNAddStates(13, 15);
                                break;

                            case 15:
                                if ((0xff000000000000L & l) != 0L)
                                    jjCheckNAddStates(19, 22);
                                break;

                            case 16:
                                if ((0xff000000000000L & l) != 0L)
                                    jjCheckNAddStates(13, 15);
                                break;

                            case 17:
                                if ((0xf000000000000L & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 18;
                                break;

                            case 18:
                                if ((0xff000000000000L & l) != 0L)
                                    jjCheckNAdd(16);
                                break;

                            case 20:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 21;
                                break;

                            case 21:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 22;
                                break;

                            case 22:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 23;
                                break;

                            case 23:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddStates(13, 15);
                                break;

                            case 24:
                                if (curChar == 32)
                                    jjAddStates(23, 24);
                                break;

                            case 25:
                                if (curChar == 10)
                                    jjCheckNAddStates(13, 15);
                                break;

                            case 26:
                                if (curChar == 39)
                                    jjCheckNAddStates(10, 12);
                                break;

                            case 27:
                                if ((0xffffff7fffffffffUL & (ulong)l) != 0L)
                                    jjCheckNAddStates(10, 12);
                                break;

                            case 29:
                                if (curChar == 32)
                                    jjAddStates(25, 26);
                                break;

                            case 30:
                                if (curChar == 10)
                                    jjCheckNAddStates(10, 12);
                                break;

                            case 31:
                                if (curChar == 39 && kind > 27)
                                    kind = 27;
                                break;

                            case 32:
                                if ((0x2400L & l) != 0L && kind > 30)
                                    kind = 30;
                                break;

                            case 33:
                                if (curChar == 10 && kind > 30)
                                    kind = 30;
                                break;

                            case 34:
                                if (curChar == 13)
                                    jjstateSet[jjnewStateCnt++] = 33;
                                break;

                            case 35:
                                if (curChar == 38 && kind > 36)
                                    kind = 36;
                                break;

                            case 36:
                                if (curChar == 38)
                                    jjstateSet[jjnewStateCnt++] = 35;
                                break;

                            case 44:
                                if (curChar == 60 && kind > 38)
                                    kind = 38;
                                break;

                            case 45:
                                if (curChar == 61 && kind > 39)
                                    kind = 39;
                                break;

                            case 46:
                                if (curChar == 60)
                                    jjstateSet[jjnewStateCnt++] = 45;
                                break;

                            case 47:
                                if (curChar == 62 && kind > 40)
                                    kind = 40;
                                break;

                            case 48:
                                if (curChar == 61 && kind > 41)
                                    kind = 41;
                                break;

                            case 49:
                                if (curChar == 62)
                                    jjstateSet[jjnewStateCnt++] = 48;
                                break;

                            case 50:
                                if (curChar == 61 && kind > 42)
                                    kind = 42;
                                break;

                            case 51:
                                if (curChar == 61)
                                    jjstateSet[jjnewStateCnt++] = 50;
                                break;

                            case 54:
                                if (curChar == 61 && kind > 43)
                                    kind = 43;
                                break;

                            case 55:
                                if (curChar == 33)
                                    jjstateSet[jjnewStateCnt++] = 54;
                                break;

                            case 56:
                                if (curChar == 33 && kind > 44)
                                    kind = 44;
                                break;

                            case 57:
                                if (curChar == 46)
                                    jjCheckNAdd(58);
                                break;

                            case 58:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(58, 59);
                                break;

                            case 60:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(61);
                                break;

                            case 61:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(61);
                                break;

                            case 63:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 57)
                                    kind = 57;
                                jjstateSet[jjnewStateCnt++] = 63;
                                break;

                            case 66:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjAddStates(27, 28);
                                break;

                            case 70:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 72:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(73, 74);
                                break;

                            case 74:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 75:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(73, 74);
                                break;

                            case 86:
                                if (curChar == 45)
                                    jjCheckNAddStates(6, 9);
                                break;

                            case 87:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 52)
                                    kind = 52;
                                jjCheckNAddTwoStates(87, 89);
                                break;

                            case 88:
                                if (curChar == 46 && kind > 52)
                                    kind = 52;
                                break;

                            case 89:
                                if (curChar == 46)
                                    jjstateSet[jjnewStateCnt++] = 88;
                                break;

                            case 90:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddTwoStates(90, 91);
                                break;

                            case 91:
                                if (curChar != 46)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(92, 93);
                                break;

                            case 92:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(92, 93);
                                break;

                            case 94:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(95);
                                break;

                            case 95:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(95);
                                break;

                            case 96:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddTwoStates(96, 97);
                                break;

                            case 98:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(99);
                                break;

                            case 99:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(99);
                                break;

                            case 100:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 52)
                                    kind = 52;
                                jjCheckNAddStates(0, 5);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 0:
                                if ((0x7fffffe87fffffeL & l) != 0L)
                                {
                                    if (kind > 57)
                                        kind = 57;
                                    jjCheckNAdd(63);
                                }
                                else if (curChar == 92)
                                    jjCheckNAddStates(29, 32);
                                else if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 65;
                                else if (curChar == 124)
                                    jjstateSet[jjnewStateCnt++] = 40;
                                if (curChar == 110)
                                    jjAddStates(33, 34);
                                else if (curChar == 103)
                                    jjAddStates(35, 36);
                                else if (curChar == 108)
                                    jjAddStates(37, 38);
                                else if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 52;
                                else if (curChar == 111)
                                    jjstateSet[jjnewStateCnt++] = 42;
                                else if (curChar == 97)
                                    jjstateSet[jjnewStateCnt++] = 38;
                                break;

                            case 6:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            case 11:
                                jjCheckNAddStates(13, 15);
                                break;

                            case 13:
                                if (curChar == 92)
                                    jjAddStates(39, 44);
                                break;

                            case 14:
                                if ((0x14404410000000L & l) != 0L)
                                    jjCheckNAddStates(13, 15);
                                break;

                            case 19:
                                if (curChar == 117)
                                    jjstateSet[jjnewStateCnt++] = 20;
                                break;

                            case 20:
                                if ((0x7e0000007eL & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 21;
                                break;

                            case 21:
                                if ((0x7e0000007eL & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 22;
                                break;

                            case 22:
                                if ((0x7e0000007eL & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 23;
                                break;

                            case 23:
                                if ((0x7e0000007eL & l) != 0L)
                                    jjCheckNAddStates(13, 15);
                                break;

                            case 27:
                                jjAddStates(10, 12);
                                break;

                            case 28:
                                if (curChar == 92)
                                    jjAddStates(25, 26);
                                break;

                            case 37:
                                if (curChar == 100 && kind > 36)
                                    kind = 36;
                                break;

                            case 38:
                                if (curChar == 110)
                                    jjstateSet[jjnewStateCnt++] = 37;
                                break;

                            case 39:
                                if (curChar == 97)
                                    jjstateSet[jjnewStateCnt++] = 38;
                                break;

                            case 40:
                                if (curChar == 124 && kind > 37)
                                    kind = 37;
                                break;

                            case 41:
                                if (curChar == 124)
                                    jjstateSet[jjnewStateCnt++] = 40;
                                break;

                            case 42:
                                if (curChar == 114 && kind > 37)
                                    kind = 37;
                                break;

                            case 43:
                                if (curChar == 111)
                                    jjstateSet[jjnewStateCnt++] = 42;
                                break;

                            case 52:
                                if (curChar == 113 && kind > 42)
                                    kind = 42;
                                break;

                            case 53:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 52;
                                break;

                            case 59:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(45, 46);
                                break;

                            case 62:
                            case 63:
                                if ((0x7fffffe87fffffeL & l) == 0L)
                                    break;
                                if (kind > 57)
                                    kind = 57;
                                jjCheckNAdd(63);
                                break;

                            case 64:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 65;
                                break;

                            case 65:
                            case 66:
                                if ((0x7fffffe87fffffeL & l) != 0L)
                                    jjCheckNAddTwoStates(66, 67);
                                break;

                            case 67:
                                if (curChar == 125 && kind > 58)
                                    kind = 58;
                                break;

                            case 68:
                                if (curChar == 92)
                                    jjCheckNAddStates(29, 32);
                                break;

                            case 69:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(69, 70);
                                break;

                            case 71:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(71, 72);
                                break;

                            case 73:
                                if (curChar == 92)
                                    jjAddStates(47, 48);
                                break;

                            case 76:
                                if (curChar == 108)
                                    jjAddStates(37, 38);
                                break;

                            case 77:
                                if (curChar == 116 && kind > 38)
                                    kind = 38;
                                break;

                            case 78:
                                if (curChar == 101 && kind > 39)
                                    kind = 39;
                                break;

                            case 79:
                                if (curChar == 103)
                                    jjAddStates(35, 36);
                                break;

                            case 80:
                                if (curChar == 116 && kind > 40)
                                    kind = 40;
                                break;

                            case 81:
                                if (curChar == 101 && kind > 41)
                                    kind = 41;
                                break;

                            case 82:
                                if (curChar == 110)
                                    jjAddStates(33, 34);
                                break;

                            case 83:
                                if (curChar == 101 && kind > 43)
                                    kind = 43;
                                break;

                            case 84:
                                if (curChar == 116 && kind > 44)
                                    kind = 44;
                                break;

                            case 85:
                                if (curChar == 111)
                                    jjstateSet[jjnewStateCnt++] = 84;
                                break;

                            case 93:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(49, 50);
                                break;

                            case 97:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(51, 52);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 6:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            case 11:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2))
                                    jjAddStates(13, 15);
                                break;

                            case 27:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2))
                                    jjAddStates(10, 12);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 101 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        private int jjStopStringLiteralDfa_6(int pos, long active0)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x70000L) != 0L)
                        return 2;
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_6(int pos, long active0)
        {
            return jjMoveNfa_6(jjStopStringLiteralDfa_6(pos, active0), pos + 1);
        }
        private int jjStartNfaWithStates_6(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_6(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_6()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_6(0x50000L);

                case (char)(42):
                    return jjMoveStringLiteralDfa1_6(0x1000000L);

                default:
                    return jjMoveNfa_6(3, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_6(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_6(0, active0);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    else if ((active0 & 0x1000000L) != 0L)
                        return jjStopAtPos(1, 24);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_6(1, 16, 0);
                    break;

                default:
                    break;

            }
            return jjStartNfa_6(0, active0);
        }
        private int jjMoveNfa_6(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 12;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 3:
                                if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(9, 10);
                                }
                                else if (curChar == 35)
                                    jjstateSet[jjnewStateCnt++] = 2;
                                break;

                            case 0:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 1;
                                break;

                            case 1:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            case 2:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 0;
                                break;

                            case 6:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 8:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(9, 10);
                                break;

                            case 10:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 11:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(9, 10);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 3:
                                if (curChar == 92)
                                    jjCheckNAddStates(53, 56);
                                break;

                            case 1:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            case 5:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(5, 6);
                                break;

                            case 7:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(7, 8);
                                break;

                            case 9:
                                if (curChar == 92)
                                    jjAddStates(57, 58);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 1:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 12 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        private int jjStopStringLiteralDfa_5(int pos, long active0)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x70000L) != 0L)
                        return 2;
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_5(int pos, long active0)
        {
            return jjMoveNfa_5(jjStopStringLiteralDfa_5(pos, active0), pos + 1);
        }
        private int jjStartNfaWithStates_5(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_5(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_5()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_5(0x50000L);

                default:
                    return jjMoveNfa_5(3, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_5(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_5(0, active0);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_5(1, 16, 0);
                    break;

                default:
                    break;

            }
            return jjStartNfa_5(0, active0);
        }
        private int jjMoveNfa_5(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 92;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 3:
                                if ((0x3ff000000000000L & l) != 0L)
                                {
                                    if (kind > 52)
                                        kind = 52;
                                    jjCheckNAddStates(59, 64);
                                }
                                else if (curChar == 45)
                                    jjCheckNAddStates(65, 68);
                                else if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(26, 27);
                                }
                                else if (curChar == 46)
                                    jjCheckNAdd(11);
                                else if (curChar == 35)
                                    jjstateSet[jjnewStateCnt++] = 2;
                                break;

                            case 0:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 1;
                                break;

                            case 1:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            case 2:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 0;
                                break;

                            case 10:
                                if (curChar == 46)
                                    jjCheckNAdd(11);
                                break;

                            case 11:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(11, 12);
                                break;

                            case 13:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(14);
                                break;

                            case 14:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(14);
                                break;

                            case 16:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 57)
                                    kind = 57;
                                jjstateSet[jjnewStateCnt++] = 16;
                                break;

                            case 19:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjAddStates(69, 70);
                                break;

                            case 23:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 25:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(26, 27);
                                break;

                            case 27:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 28:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(26, 27);
                                break;

                            case 31:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddStates(71, 73);
                                break;

                            case 32:
                                if ((0x2400L & l) != 0L && kind > 46)
                                    kind = 46;
                                break;

                            case 33:
                                if (curChar == 10 && kind > 46)
                                    kind = 46;
                                break;

                            case 34:
                            case 51:
                                if (curChar == 13)
                                    jjCheckNAdd(33);
                                break;

                            case 42:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddStates(74, 76);
                                break;

                            case 43:
                                if ((0x2400L & l) != 0L && kind > 49)
                                    kind = 49;
                                break;

                            case 44:
                                if (curChar == 10 && kind > 49)
                                    kind = 49;
                                break;

                            case 45:
                            case 67:
                                if (curChar == 13)
                                    jjCheckNAdd(44);
                                break;

                            case 50:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddStates(77, 79);
                                break;

                            case 66:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddStates(80, 82);
                                break;

                            case 77:
                                if (curChar == 45)
                                    jjCheckNAddStates(65, 68);
                                break;

                            case 78:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 52)
                                    kind = 52;
                                jjCheckNAddTwoStates(78, 80);
                                break;

                            case 79:
                                if (curChar == 46 && kind > 52)
                                    kind = 52;
                                break;

                            case 80:
                                if (curChar == 46)
                                    jjstateSet[jjnewStateCnt++] = 79;
                                break;

                            case 81:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddTwoStates(81, 82);
                                break;

                            case 82:
                                if (curChar != 46)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(83, 84);
                                break;

                            case 83:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(83, 84);
                                break;

                            case 85:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(86);
                                break;

                            case 86:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(86);
                                break;

                            case 87:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddTwoStates(87, 88);
                                break;

                            case 89:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(90);
                                break;

                            case 90:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(90);
                                break;

                            case 91:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 52)
                                    kind = 52;
                                jjCheckNAddStates(59, 64);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 3:
                                if ((0x7fffffe87fffffeL & l) != 0L)
                                {
                                    if (kind > 57)
                                        kind = 57;
                                    jjCheckNAdd(16);
                                }
                                else if (curChar == 123)
                                    jjAddStates(83, 87);
                                else if (curChar == 92)
                                    jjCheckNAddStates(88, 91);
                                if (curChar == 101)
                                    jjAddStates(92, 94);
                                else if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 18;
                                else if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 8;
                                else if (curChar == 105)
                                    jjstateSet[jjnewStateCnt++] = 4;
                                break;

                            case 1:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            case 4:
                                if (curChar == 102 && kind > 47)
                                    kind = 47;
                                break;

                            case 5:
                                if (curChar == 105)
                                    jjstateSet[jjnewStateCnt++] = 4;
                                break;

                            case 6:
                                if (curChar == 112 && kind > 50)
                                    kind = 50;
                                break;

                            case 7:
                                if (curChar == 111)
                                    jjstateSet[jjnewStateCnt++] = 6;
                                break;

                            case 8:
                                if (curChar == 116)
                                    jjstateSet[jjnewStateCnt++] = 7;
                                break;

                            case 9:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 8;
                                break;

                            case 12:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(95, 96);
                                break;

                            case 15:
                            case 16:
                                if ((0x7fffffe87fffffeL & l) == 0L)
                                    break;
                                if (kind > 57)
                                    kind = 57;
                                jjCheckNAdd(16);
                                break;

                            case 17:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 18;
                                break;

                            case 18:
                            case 19:
                                if ((0x7fffffe87fffffeL & l) != 0L)
                                    jjCheckNAddTwoStates(19, 20);
                                break;

                            case 20:
                                if (curChar == 125 && kind > 58)
                                    kind = 58;
                                break;

                            case 21:
                                if (curChar == 92)
                                    jjCheckNAddStates(88, 91);
                                break;

                            case 22:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(22, 23);
                                break;

                            case 24:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(24, 25);
                                break;

                            case 26:
                                if (curChar == 92)
                                    jjAddStates(97, 98);
                                break;

                            case 29:
                                if (curChar == 101)
                                    jjAddStates(92, 94);
                                break;

                            case 30:
                                if (curChar != 100)
                                    break;
                                if (kind > 46)
                                    kind = 46;
                                jjCheckNAddStates(71, 73);
                                break;

                            case 35:
                                if (curChar == 110)
                                    jjstateSet[jjnewStateCnt++] = 30;
                                break;

                            case 36:
                                if (curChar == 102 && kind > 48)
                                    kind = 48;
                                break;

                            case 37:
                                if (curChar == 105)
                                    jjstateSet[jjnewStateCnt++] = 36;
                                break;

                            case 38:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 37;
                                break;

                            case 39:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 38;
                                break;

                            case 40:
                                if (curChar == 108)
                                    jjstateSet[jjnewStateCnt++] = 39;
                                break;

                            case 41:
                                if (curChar != 101)
                                    break;
                                if (kind > 49)
                                    kind = 49;
                                jjCheckNAddStates(74, 76);
                                break;

                            case 46:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 41;
                                break;

                            case 47:
                                if (curChar == 108)
                                    jjstateSet[jjnewStateCnt++] = 46;
                                break;

                            case 48:
                                if (curChar == 123)
                                    jjAddStates(83, 87);
                                break;

                            case 49:
                                if (curChar != 125)
                                    break;
                                if (kind > 46)
                                    kind = 46;
                                jjCheckNAddStates(77, 79);
                                break;

                            case 52:
                                if (curChar == 100)
                                    jjstateSet[jjnewStateCnt++] = 49;
                                break;

                            case 53:
                                if (curChar == 110)
                                    jjstateSet[jjnewStateCnt++] = 52;
                                break;

                            case 54:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 53;
                                break;

                            case 55:
                                if (curChar == 125 && kind > 47)
                                    kind = 47;
                                break;

                            case 56:
                                if (curChar == 102)
                                    jjstateSet[jjnewStateCnt++] = 55;
                                break;

                            case 57:
                                if (curChar == 105)
                                    jjstateSet[jjnewStateCnt++] = 56;
                                break;

                            case 58:
                                if (curChar == 125 && kind > 48)
                                    kind = 48;
                                break;

                            case 59:
                                if (curChar == 102)
                                    jjstateSet[jjnewStateCnt++] = 58;
                                break;

                            case 60:
                                if (curChar == 105)
                                    jjstateSet[jjnewStateCnt++] = 59;
                                break;

                            case 61:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 60;
                                break;

                            case 62:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 61;
                                break;

                            case 63:
                                if (curChar == 108)
                                    jjstateSet[jjnewStateCnt++] = 62;
                                break;

                            case 64:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 63;
                                break;

                            case 65:
                                if (curChar != 125)
                                    break;
                                if (kind > 49)
                                    kind = 49;
                                jjCheckNAddStates(80, 82);
                                break;

                            case 68:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 65;
                                break;

                            case 69:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 68;
                                break;

                            case 70:
                                if (curChar == 108)
                                    jjstateSet[jjnewStateCnt++] = 69;
                                break;

                            case 71:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 70;
                                break;

                            case 72:
                                if (curChar == 125 && kind > 50)
                                    kind = 50;
                                break;

                            case 73:
                                if (curChar == 112)
                                    jjstateSet[jjnewStateCnt++] = 72;
                                break;

                            case 74:
                                if (curChar == 111)
                                    jjstateSet[jjnewStateCnt++] = 73;
                                break;

                            case 75:
                                if (curChar == 116)
                                    jjstateSet[jjnewStateCnt++] = 74;
                                break;

                            case 76:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 75;
                                break;

                            case 84:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(99, 100);
                                break;

                            case 88:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(101, 102);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 1:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 92 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        private int jjStopStringLiteralDfa_3(int pos, long active0)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x180000L) != 0L)
                        return 14;
                    if ((active0 & 0x70000L) != 0L)
                        return 33;
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_3(int pos, long active0)
        {
            return jjMoveNfa_3(jjStopStringLiteralDfa_3(pos, active0), pos + 1);
        }
        private int jjStartNfaWithStates_3(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_3(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_3()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_3(0x50000L);

                case (char)(92):
                    jjmatchedKind = 20;
                    return jjMoveStringLiteralDfa1_3(0x80000L);

                default:
                    return jjMoveNfa_3(22, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_3(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_3(0, active0);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_3(1, 16, 31);
                    break;

                case (char)(92):
                    if ((active0 & 0x80000L) != 0L)
                        return jjStartNfaWithStates_3(1, 19, 34);
                    break;

                default:
                    break;

            }
            return jjStartNfa_3(0, active0);
        }
        private int jjMoveNfa_3(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 34;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 22:
                                if ((0xffffffe7ffffffffUL & (ulong)l) != 0L)
                                {
                                    if (kind > 21)
                                        kind = 21;
                                    jjCheckNAdd(12);
                                }
                                else if (curChar == 35)
                                    jjCheckNAddStates(103, 105);
                                else if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(27, 28);
                                }
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddTwoStates(0, 1);
                                break;

                            case 14:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(27, 28);
                                else if (curChar == 35)
                                    jjAddStates(106, 107);
                                if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                }
                                break;

                            case 34:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(27, 28);
                                if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                }
                                break;

                            case 33:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 31;
                                break;

                            case 0:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddTwoStates(0, 1);
                                break;

                            case 1:
                                if (curChar == 35)
                                    jjCheckNAddTwoStates(6, 11);
                                break;

                            case 3:
                                if (curChar == 32)
                                    jjAddStates(108, 109);
                                break;

                            case 4:
                                if (curChar == 40 && kind > 12)
                                    kind = 12;
                                break;

                            case 12:
                                if ((0xffffffe7ffffffffUL & (ulong)l) == 0L)
                                    break;
                                if (kind > 21)
                                    kind = 21;
                                jjCheckNAdd(12);
                                break;

                            case 15:
                                if (curChar == 35)
                                    jjAddStates(106, 107);
                                break;

                            case 17:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 11)
                                    kind = 11;
                                jjstateSet[jjnewStateCnt++] = 17;
                                break;

                            case 20:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjAddStates(110, 111);
                                break;

                            case 24:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 26:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(27, 28);
                                break;

                            case 28:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 29:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(27, 28);
                                break;

                            case 30:
                                if (curChar == 35)
                                    jjCheckNAddStates(103, 105);
                                break;

                            case 31:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 32;
                                break;

                            case 32:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 22:
                                if ((0xffffffffefffffffUL & (ulong)l) != 0L)
                                {
                                    if (kind > 21)
                                        kind = 21;
                                    jjCheckNAdd(12);
                                }
                                else if (curChar == 92)
                                    jjCheckNAddStates(112, 115);
                                if (curChar == 92)
                                    jjAddStates(116, 117);
                                break;

                            case 14:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(25, 26);
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(23, 24);
                                if (curChar == 92)
                                    jjstateSet[jjnewStateCnt++] = 13;
                                break;

                            case 34:
                                if (curChar == 92)
                                    jjAddStates(116, 117);
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(25, 26);
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(23, 24);
                                break;

                            case 33:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 10;
                                else if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 2:
                                if (curChar == 116)
                                    jjCheckNAddTwoStates(3, 4);
                                break;

                            case 5:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 2;
                                break;

                            case 6:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 7:
                                if (curChar == 125)
                                    jjCheckNAddTwoStates(3, 4);
                                break;

                            case 8:
                                if (curChar == 116)
                                    jjstateSet[jjnewStateCnt++] = 7;
                                break;

                            case 9:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 8;
                                break;

                            case 10:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 9;
                                break;

                            case 11:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 10;
                                break;

                            case 12:
                                if ((0xffffffffefffffffUL & (ulong)l) == 0L)
                                    break;
                                if (kind > 21)
                                    kind = 21;
                                jjCheckNAdd(12);
                                break;

                            case 13:
                                if (curChar == 92)
                                    jjAddStates(116, 117);
                                break;

                            case 16:
                            case 17:
                                if ((0x7fffffe87fffffeL & l) == 0L)
                                    break;
                                if (kind > 11)
                                    kind = 11;
                                jjCheckNAdd(17);
                                break;

                            case 18:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 19;
                                break;

                            case 19:
                            case 20:
                                if ((0x7fffffe87fffffeL & l) != 0L)
                                    jjCheckNAddTwoStates(20, 21);
                                break;

                            case 21:
                                if (curChar == 125 && kind > 11)
                                    kind = 11;
                                break;

                            case 23:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(23, 24);
                                break;

                            case 25:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(25, 26);
                                break;

                            case 27:
                                if (curChar == 92)
                                    jjAddStates(118, 119);
                                break;

                            case 32:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 22:
                            case 12:
                                if (!jjCanMove_0(hiByte, i1, i2, l1, l2))
                                    break;
                                if (kind > 21)
                                    kind = 21;
                                jjCheckNAdd(12);
                                break;

                            case 32:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 34 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        private int jjStopStringLiteralDfa_7(int pos, long active0)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x70000L) != 0L)
                        return 2;
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_7(int pos, long active0)
        {
            return jjMoveNfa_7(jjStopStringLiteralDfa_7(pos, active0), pos + 1);
        }
        private int jjStartNfaWithStates_7(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_7(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_7()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_7(0x50000L);

                case (char)(42):
                    return jjMoveStringLiteralDfa1_7(0x800000L);

                default:
                    return jjMoveNfa_7(3, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_7(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_7(0, active0);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    else if ((active0 & 0x800000L) != 0L)
                        return jjStopAtPos(1, 23);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_7(1, 16, 0);
                    break;

                default:
                    break;

            }
            return jjStartNfa_7(0, active0);
        }
        private int jjMoveNfa_7(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 12;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 3:
                                if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(9, 10);
                                }
                                else if (curChar == 35)
                                    jjstateSet[jjnewStateCnt++] = 2;
                                break;

                            case 0:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 1;
                                break;

                            case 1:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            case 2:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 0;
                                break;

                            case 6:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 8:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(9, 10);
                                break;

                            case 10:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 11:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(9, 10);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 3:
                                if (curChar == 92)
                                    jjCheckNAddStates(53, 56);
                                break;

                            case 1:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            case 5:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(5, 6);
                                break;

                            case 7:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(7, 8);
                                break;

                            case 9:
                                if (curChar == 92)
                                    jjAddStates(57, 58);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 1:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 12 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        private int jjStopStringLiteralDfa_8(int pos, long active0)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x70000L) != 0L)
                        return 2;
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_8(int pos, long active0)
        {
            return jjMoveNfa_8(jjStopStringLiteralDfa_8(pos, active0), pos + 1);
        }
        private int jjStartNfaWithStates_8(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_8(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_8()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_8(0x50000L);

                default:
                    return jjMoveNfa_8(3, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_8(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_8(0, active0);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_8(1, 16, 0);
                    break;

                default:
                    break;

            }
            return jjStartNfa_8(0, active0);
        }
        private int jjMoveNfa_8(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 15;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 3:
                                if ((0x2400L & l) != 0L)
                                {
                                    if (kind > 22)
                                        kind = 22;
                                }
                                else if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(12, 13);
                                }
                                else if (curChar == 35)
                                    jjstateSet[jjnewStateCnt++] = 2;
                                if (curChar == 13)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 0:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 1;
                                break;

                            case 1:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            case 2:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 0;
                                break;

                            case 4:
                                if ((0x2400L & l) != 0L && kind > 22)
                                    kind = 22;
                                break;

                            case 5:
                                if (curChar == 10 && kind > 22)
                                    kind = 22;
                                break;

                            case 6:
                                if (curChar == 13)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 9:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 11:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(12, 13);
                                break;

                            case 13:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 14:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(12, 13);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 3:
                                if (curChar == 92)
                                    jjCheckNAddStates(120, 123);
                                break;

                            case 1:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            case 8:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(8, 9);
                                break;

                            case 10:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(10, 11);
                                break;

                            case 12:
                                if (curChar == 92)
                                    jjAddStates(124, 125);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 1:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 15 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        private int jjStopStringLiteralDfa_4(int pos, long active0, long active1)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x70000L) != 0L)
                        return 27;
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        return 13;
                    }
                    return -1;

                case 1:
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 1;
                        return 13;
                    }
                    if ((active0 & 0x10000L) != 0L)
                        return 25;
                    return -1;

                case 2:
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 2;
                        return 13;
                    }
                    return -1;

                case 3:
                    if ((active0 & 0x10000000L) != 0L)
                        return 13;
                    if ((active0 & 0x20000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 3;
                        return 13;
                    }
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_4(int pos, long active0, long active1)
        {
            return jjMoveNfa_4(jjStopStringLiteralDfa_4(pos, active0, active1), pos + 1);
        }
        private int jjStartNfaWithStates_4(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_4(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_4()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_4(0x50000L);

                case (char)(102):
                    return jjMoveStringLiteralDfa1_4(0x20000000L);

                case (char)(116):
                    return jjMoveStringLiteralDfa1_4(0x10000000L);

                case (char)(123):
                    return jjStopAtPos(0, 64);

                case (char)(125):
                    return jjStopAtPos(0, 65);

                default:
                    return jjMoveNfa_4(12, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_4(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_4(0, active0, 0L);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_4(1, 16, 25);
                    break;

                case (char)(97):
                    return jjMoveStringLiteralDfa2_4(active0, 0x20000000L);

                case (char)(114):
                    return jjMoveStringLiteralDfa2_4(active0, 0x10000000L);

                default:
                    break;

            }
            return jjStartNfa_4(0, active0, 0L);
        }
        private int jjMoveStringLiteralDfa2_4(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_4(0, old0, 0L);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_4(1, active0, 0L);
                return 2;
            }
            switch (curChar)
            {

                case (char)(108):
                    return jjMoveStringLiteralDfa3_4(active0, 0x20000000L);

                case (char)(117):
                    return jjMoveStringLiteralDfa3_4(active0, 0x10000000L);

                default:
                    break;

            }
            return jjStartNfa_4(1, active0, 0L);
        }
        private int jjMoveStringLiteralDfa3_4(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_4(1, old0, 0L);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_4(2, active0, 0L);
                return 3;
            }
            switch (curChar)
            {

                case (char)(101):
                    if ((active0 & 0x10000000L) != 0L)
                        return jjStartNfaWithStates_4(3, 28, 13);
                    break;

                case (char)(115):
                    return jjMoveStringLiteralDfa4_4(active0, 0x20000000L);

                default:
                    break;

            }
            return jjStartNfa_4(2, active0, 0L);
        }
        private int jjMoveStringLiteralDfa4_4(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_4(2, old0, 0L);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_4(3, active0, 0L);
                return 4;
            }
            switch (curChar)
            {

                case (char)(101):
                    if ((active0 & 0x20000000L) != 0L)
                        return jjStartNfaWithStates_4(4, 29, 13);
                    break;

                default:
                    break;

            }
            return jjStartNfa_4(3, active0, 0L);
        }
        private int jjMoveNfa_4(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 28;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 12:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddTwoStates(0, 1);
                                else if (curChar == 35)
                                    jjCheckNAddStates(126, 128);
                                else if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(21, 22);
                                }
                                else if (curChar == 46)
                                    jjstateSet[jjnewStateCnt++] = 15;
                                break;

                            case 27:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 25;
                                break;

                            case 0:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddTwoStates(0, 1);
                                break;

                            case 1:
                                if (curChar == 35)
                                    jjCheckNAddTwoStates(6, 11);
                                break;

                            case 3:
                                if (curChar == 32)
                                    jjAddStates(108, 109);
                                break;

                            case 4:
                                if (curChar == 40 && kind > 12)
                                    kind = 12;
                                break;

                            case 13:
                                if ((0x3ff200000000000L & l) == 0L)
                                    break;
                                if (kind > 62)
                                    kind = 62;
                                jjstateSet[jjnewStateCnt++] = 13;
                                break;

                            case 14:
                                if (curChar == 46)
                                    jjstateSet[jjnewStateCnt++] = 15;
                                break;

                            case 18:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 20:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(21, 22);
                                break;

                            case 22:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 23:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(21, 22);
                                break;

                            case 24:
                                if (curChar == 35)
                                    jjCheckNAddStates(126, 128);
                                break;

                            case 25:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 26;
                                break;

                            case 26:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 12:
                                if ((0x7fffffe87fffffeL & l) != 0L)
                                {
                                    if (kind > 62)
                                        kind = 62;
                                    jjCheckNAdd(13);
                                }
                                else if (curChar == 92)
                                    jjCheckNAddStates(129, 132);
                                break;

                            case 27:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 10;
                                else if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 2:
                                if (curChar == 116)
                                    jjCheckNAddTwoStates(3, 4);
                                break;

                            case 5:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 2;
                                break;

                            case 6:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 7:
                                if (curChar == 125)
                                    jjCheckNAddTwoStates(3, 4);
                                break;

                            case 8:
                                if (curChar == 116)
                                    jjstateSet[jjnewStateCnt++] = 7;
                                break;

                            case 9:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 8;
                                break;

                            case 10:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 9;
                                break;

                            case 11:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 10;
                                break;

                            case 13:
                                if ((0x7fffffe87fffffeL & l) == 0L)
                                    break;
                                if (kind > 62)
                                    kind = 62;
                                jjCheckNAdd(13);
                                break;

                            case 15:
                                if ((0x7fffffe07fffffeL & l) != 0L && kind > 63)
                                    kind = 63;
                                break;

                            case 16:
                                if (curChar == 92)
                                    jjCheckNAddStates(129, 132);
                                break;

                            case 17:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(17, 18);
                                break;

                            case 19:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(19, 20);
                                break;

                            case 21:
                                if (curChar == 92)
                                    jjAddStates(133, 134);
                                break;

                            case 26:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 26:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 28 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        private int jjStopStringLiteralDfa_1(int pos, long active0)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x70000L) != 0L)
                        return 48;
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        return 36;
                    }
                    if ((active0 & 0x10L) != 0L)
                        return 70;
                    return -1;

                case 1:
                    if ((active0 & 0x10000L) != 0L)
                        return 46;
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 1;
                        return 36;
                    }
                    return -1;

                case 2:
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 2;
                        return 36;
                    }
                    return -1;

                case 3:
                    if ((active0 & 0x10000000L) != 0L)
                        return 36;
                    if ((active0 & 0x20000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 3;
                        return 36;
                    }
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_1(int pos, long active0)
        {
            return jjMoveNfa_1(jjStopStringLiteralDfa_1(pos, active0), pos + 1);
        }
        private int jjStartNfaWithStates_1(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_1(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_1()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_1(0x50000L);

                case (char)(41):
                    return jjStopAtPos(0, 10);

                case (char)(44):
                    return jjStopAtPos(0, 3);

                case (char)(46):
                    return jjMoveStringLiteralDfa1_1(0x10L);

                case (char)(58):
                    return jjStopAtPos(0, 5);

                case (char)(91):
                    return jjStopAtPos(0, 1);

                case (char)(93):
                    return jjStopAtPos(0, 2);

                case (char)(102):
                    return jjMoveStringLiteralDfa1_1(0x20000000L);

                case (char)(116):
                    return jjMoveStringLiteralDfa1_1(0x10000000L);

                case (char)(123):
                    return jjStopAtPos(0, 6);

                case (char)(125):
                    return jjStopAtPos(0, 7);

                default:
                    return jjMoveNfa_1(13, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_1(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_1(0, active0);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_1(1, 16, 46);
                    break;

                case (char)(46):
                    if ((active0 & 0x10L) != 0L)
                        return jjStopAtPos(1, 4);
                    break;

                case (char)(97):
                    return jjMoveStringLiteralDfa2_1(active0, 0x20000000L);

                case (char)(114):
                    return jjMoveStringLiteralDfa2_1(active0, 0x10000000L);

                default:
                    break;

            }
            return jjStartNfa_1(0, active0);
        }
        private int jjMoveStringLiteralDfa2_1(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_1(0, old0);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_1(1, active0);
                return 2;
            }
            switch (curChar)
            {

                case (char)(108):
                    return jjMoveStringLiteralDfa3_1(active0, 0x20000000L);

                case (char)(117):
                    return jjMoveStringLiteralDfa3_1(active0, 0x10000000L);

                default:
                    break;

            }
            return jjStartNfa_1(1, active0);
        }
        private int jjMoveStringLiteralDfa3_1(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_1(1, old0);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_1(2, active0);
                return 3;
            }
            switch (curChar)
            {

                case (char)(101):
                    if ((active0 & 0x10000000L) != 0L)
                        return jjStartNfaWithStates_1(3, 28, 36);
                    break;

                case (char)(115):
                    return jjMoveStringLiteralDfa4_1(active0, 0x20000000L);

                default:
                    break;

            }
            return jjStartNfa_1(2, active0);
        }
        private int jjMoveStringLiteralDfa4_1(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_1(2, old0);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_1(3, active0);
                return 4;
            }
            switch (curChar)
            {

                case (char)(101):
                    if ((active0 & 0x20000000L) != 0L)
                        return jjStartNfaWithStates_1(4, 29, 36);
                    break;

                default:
                    break;

            }
            return jjStartNfa_1(3, active0);
        }
        private int jjMoveNfa_1(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 71;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 13:
                                if ((0x3ff000000000000L & l) != 0L)
                                {
                                    if (kind > 52)
                                        kind = 52;
                                    jjCheckNAddStates(135, 140);
                                }
                                else if ((0x100002600L & l) != 0L)
                                {
                                    if (kind > 26)
                                        kind = 26;
                                    jjCheckNAdd(12);
                                }
                                else if (curChar == 46)
                                    jjCheckNAddTwoStates(60, 70);
                                else if (curChar == 45)
                                    jjCheckNAddStates(141, 144);
                                else if (curChar == 35)
                                    jjCheckNAddStates(145, 147);
                                else if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(42, 43);
                                }
                                else if (curChar == 39)
                                    jjCheckNAddStates(148, 150);
                                else if (curChar == 34)
                                    jjCheckNAddStates(151, 153);
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddTwoStates(0, 1);
                                break;

                            case 70:
                            case 60:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(60, 61);
                                break;

                            case 48:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 46;
                                break;

                            case 0:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddTwoStates(0, 1);
                                break;

                            case 1:
                                if (curChar == 35)
                                    jjCheckNAddTwoStates(6, 11);
                                break;

                            case 3:
                                if (curChar == 32)
                                    jjAddStates(108, 109);
                                break;

                            case 4:
                                if (curChar == 40 && kind > 12)
                                    kind = 12;
                                break;

                            case 12:
                                if ((0x100002600L & l) == 0L)
                                    break;
                                if (kind > 26)
                                    kind = 26;
                                jjCheckNAdd(12);
                                break;

                            case 14:
                                if ((0xfffffffbffffffffUL & (ulong)l) != 0L)
                                    jjCheckNAddStates(151, 153);
                                break;

                            case 15:
                                if (curChar == 34 && kind > 27)
                                    kind = 27;
                                break;

                            case 17:
                                if ((0x8400000000L & l) != 0L)
                                    jjCheckNAddStates(151, 153);
                                break;

                            case 18:
                                if ((0xff000000000000L & l) != 0L)
                                    jjCheckNAddStates(154, 157);
                                break;

                            case 19:
                                if ((0xff000000000000L & l) != 0L)
                                    jjCheckNAddStates(151, 153);
                                break;

                            case 20:
                                if ((0xf000000000000L & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 21;
                                break;

                            case 21:
                                if ((0xff000000000000L & l) != 0L)
                                    jjCheckNAdd(19);
                                break;

                            case 23:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 24;
                                break;

                            case 24:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 25;
                                break;

                            case 25:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 26;
                                break;

                            case 26:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddStates(151, 153);
                                break;

                            case 27:
                                if (curChar == 32)
                                    jjAddStates(118, 119);
                                break;

                            case 28:
                                if (curChar == 10)
                                    jjCheckNAddStates(151, 153);
                                break;

                            case 29:
                                if (curChar == 39)
                                    jjCheckNAddStates(148, 150);
                                break;

                            case 30:

                                if ((0xffffff7fffffffffUL & (ulong)l) != 0L)
                                    jjCheckNAddStates(148, 150);
                                break;

                            case 32:
                                if (curChar == 32)
                                    jjAddStates(158, 159);
                                break;

                            case 33:
                                if (curChar == 10)
                                    jjCheckNAddStates(148, 150);
                                break;

                            case 34:
                                if (curChar == 39 && kind > 27)
                                    kind = 27;
                                break;

                            case 36:
                                if ((0x3ff200000000000L & l) == 0L)
                                    break;
                                if (kind > 62)
                                    kind = 62;
                                jjstateSet[jjnewStateCnt++] = 36;
                                break;

                            case 39:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 41:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(42, 43);
                                break;

                            case 43:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 44:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(42, 43);
                                break;

                            case 45:
                                if (curChar == 35)
                                    jjCheckNAddStates(145, 147);
                                break;

                            case 46:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 47;
                                break;

                            case 47:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            case 49:
                                if (curChar == 45)
                                    jjCheckNAddStates(141, 144);
                                break;

                            case 50:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 52)
                                    kind = 52;
                                jjCheckNAddTwoStates(50, 52);
                                break;

                            case 51:
                                if (curChar == 46 && kind > 52)
                                    kind = 52;
                                break;

                            case 52:
                                if (curChar == 46)
                                    jjstateSet[jjnewStateCnt++] = 51;
                                break;

                            case 53:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddTwoStates(53, 54);
                                break;

                            case 54:
                                if (curChar != 46)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(55, 56);
                                break;

                            case 55:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAddTwoStates(55, 56);
                                break;

                            case 57:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(58);
                                break;

                            case 58:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(58);
                                break;

                            case 59:
                                if (curChar == 46)
                                    jjCheckNAdd(60);
                                break;

                            case 62:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(63);
                                break;

                            case 63:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(63);
                                break;

                            case 64:
                                if ((0x3ff000000000000L & l) != 0L)
                                    jjCheckNAddTwoStates(64, 65);
                                break;

                            case 66:
                                if ((0x280000000000L & l) != 0L)
                                    jjCheckNAdd(67);
                                break;

                            case 67:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 53)
                                    kind = 53;
                                jjCheckNAdd(67);
                                break;

                            case 68:
                                if ((0x3ff000000000000L & l) == 0L)
                                    break;
                                if (kind > 52)
                                    kind = 52;
                                jjCheckNAddStates(135, 140);
                                break;

                            case 69:
                                if (curChar == 46)
                                    jjCheckNAddTwoStates(60, 70);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 13:
                                if ((0x7fffffe87fffffeL & l) != 0L)
                                {
                                    if (kind > 62)
                                        kind = 62;
                                    jjCheckNAdd(36);
                                }
                                else if (curChar == 92)
                                    jjCheckNAddStates(160, 163);
                                break;

                            case 70:
                                if ((0x7fffffe07fffffeL & l) != 0L && kind > 63)
                                    kind = 63;
                                break;

                            case 48:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 10;
                                else if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 2:
                                if (curChar == 116)
                                    jjCheckNAddTwoStates(3, 4);
                                break;

                            case 5:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 2;
                                break;

                            case 6:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 7:
                                if (curChar == 125)
                                    jjCheckNAddTwoStates(3, 4);
                                break;

                            case 8:
                                if (curChar == 116)
                                    jjstateSet[jjnewStateCnt++] = 7;
                                break;

                            case 9:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 8;
                                break;

                            case 10:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 9;
                                break;

                            case 11:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 10;
                                break;

                            case 14:
                                jjCheckNAddStates(151, 153);
                                break;

                            case 16:
                                if (curChar == 92)
                                    jjAddStates(164, 169);
                                break;

                            case 17:
                                if ((0x14404410000000L & l) != 0L)
                                    jjCheckNAddStates(151, 153);
                                break;

                            case 22:
                                if (curChar == 117)
                                    jjstateSet[jjnewStateCnt++] = 23;
                                break;

                            case 23:
                                if ((0x7e0000007eL & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 24;
                                break;

                            case 24:
                                if ((0x7e0000007eL & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 25;
                                break;

                            case 25:
                                if ((0x7e0000007eL & l) != 0L)
                                    jjstateSet[jjnewStateCnt++] = 26;
                                break;

                            case 26:
                                if ((0x7e0000007eL & l) != 0L)
                                    jjCheckNAddStates(151, 153);
                                break;

                            case 30:
                                jjAddStates(148, 150);
                                break;

                            case 31:
                                if (curChar == 92)
                                    jjAddStates(158, 159);
                                break;

                            case 35:
                            case 36:
                                if ((0x7fffffe87fffffeL & l) == 0L)
                                    break;
                                if (kind > 62)
                                    kind = 62;
                                jjCheckNAdd(36);
                                break;

                            case 37:
                                if (curChar == 92)
                                    jjCheckNAddStates(160, 163);
                                break;

                            case 38:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(38, 39);
                                break;

                            case 40:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(40, 41);
                                break;

                            case 42:
                                if (curChar == 92)
                                    jjAddStates(170, 171);
                                break;

                            case 47:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            case 56:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(172, 173);
                                break;

                            case 61:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(174, 175);
                                break;

                            case 65:
                                if ((0x2000000020L & l) != 0L)
                                    jjAddStates(27, 28);
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 14:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2))
                                    jjAddStates(151, 153);
                                break;

                            case 30:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2))
                                    jjAddStates(148, 150);
                                break;

                            case 47:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 71 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        private int jjStopStringLiteralDfa_2(int pos, long active0, long active1)
        {
            switch (pos)
            {

                case 0:
                    if ((active0 & 0x70000L) != 0L)
                        return 27;
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        return 13;
                    }
                    return -1;

                case 1:
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 1;
                        return 13;
                    }
                    if ((active0 & 0x10000L) != 0L)
                        return 25;
                    return -1;

                case 2:
                    if ((active0 & 0x30000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 2;
                        return 13;
                    }
                    return -1;

                case 3:
                    if ((active0 & 0x10000000L) != 0L)
                        return 13;
                    if ((active0 & 0x20000000L) != 0L)
                    {
                        jjmatchedKind = 62;
                        jjmatchedPos = 3;
                        return 13;
                    }
                    return -1;

                default:
                    return -1;

            }
        }
        private int jjStartNfa_2(int pos, long active0, long active1)
        {
            return jjMoveNfa_2(jjStopStringLiteralDfa_2(pos, active0, active1), pos + 1);
        }
        private int jjStartNfaWithStates_2(int pos, int kind, int state)
        {
            jjmatchedKind = kind;
            jjmatchedPos = pos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                return pos + 1;
            }
            return jjMoveNfa_2(state, pos + 1);
        }
        private int jjMoveStringLiteralDfa0_2()
        {
            switch (curChar)
            {

                case (char)(35):
                    jjmatchedKind = 17;
                    return jjMoveStringLiteralDfa1_2(0x50000L);

                case (char)(40):
                    return jjStopAtPos(0, 8);

                case (char)(102):
                    return jjMoveStringLiteralDfa1_2(0x20000000L);

                case (char)(116):
                    return jjMoveStringLiteralDfa1_2(0x10000000L);

                case (char)(123):
                    return jjStopAtPos(0, 64);

                case (char)(125):
                    return jjStopAtPos(0, 65);

                default:
                    return jjMoveNfa_2(12, 0);

            }
        }
        private int jjMoveStringLiteralDfa1_2(long active0)
        {
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_2(0, active0, 0L);
                return 1;
            }
            switch (curChar)
            {

                case (char)(35):
                    if ((active0 & 0x40000L) != 0L)
                        return jjStopAtPos(1, 18);
                    break;

                case (char)(42):
                    if ((active0 & 0x10000L) != 0L)
                        return jjStartNfaWithStates_2(1, 16, 25);
                    break;

                case (char)(97):
                    return jjMoveStringLiteralDfa2_2(active0, 0x20000000L);

                case (char)(114):
                    return jjMoveStringLiteralDfa2_2(active0, 0x10000000L);

                default:
                    break;

            }
            return jjStartNfa_2(0, active0, 0L);
        }
        private int jjMoveStringLiteralDfa2_2(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_2(0, old0, 0L);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_2(1, active0, 0L);
                return 2;
            }
            switch (curChar)
            {

                case (char)(108):
                    return jjMoveStringLiteralDfa3_2(active0, 0x20000000L);

                case (char)(117):
                    return jjMoveStringLiteralDfa3_2(active0, 0x10000000L);

                default:
                    break;

            }
            return jjStartNfa_2(1, active0, 0L);
        }
        private int jjMoveStringLiteralDfa3_2(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_2(1, old0, 0L);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_2(2, active0, 0L);
                return 3;
            }
            switch (curChar)
            {

                case (char)(101):
                    if ((active0 & 0x10000000L) != 0L)
                        return jjStartNfaWithStates_2(3, 28, 13);
                    break;

                case (char)(115):
                    return jjMoveStringLiteralDfa4_2(active0, 0x20000000L);

                default:
                    break;

            }
            return jjStartNfa_2(2, active0, 0L);
        }
        private int jjMoveStringLiteralDfa4_2(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return jjStartNfa_2(2, old0, 0L);
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (System.IO.IOException e)
            {
                jjStopStringLiteralDfa_2(3, active0, 0L);
                return 4;
            }
            switch (curChar)
            {

                case (char)(101):
                    if ((active0 & 0x20000000L) != 0L)
                        return jjStartNfaWithStates_2(4, 29, 13);
                    break;

                default:
                    break;

            }
            return jjStartNfa_2(3, active0, 0L);
        }
        private int jjMoveNfa_2(int startState, int curPos)
        {
            int[] nextStates;
            int startsAt = 0;
            jjnewStateCnt = 28;
            int i = 1;
            jjstateSet[0] = (uint)startState;
            int j, kind = 0x7fffffff;
            for (; ; )
            {
                if (++jjround == 0x7fffffff)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << (int)curChar;

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 12:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddTwoStates(0, 1);
                                else if (curChar == 35)
                                    jjCheckNAddStates(126, 128);
                                else if (curChar == 36)
                                {
                                    if (kind > 13)
                                        kind = 13;
                                    jjCheckNAddTwoStates(21, 22);
                                }
                                else if (curChar == 46)
                                    jjstateSet[jjnewStateCnt++] = 15;
                                break;

                            case 27:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 25;
                                break;

                            case 0:
                                if ((0x100000200L & l) != 0L)
                                    jjCheckNAddTwoStates(0, 1);
                                break;

                            case 1:
                                if (curChar == 35)
                                    jjCheckNAddTwoStates(6, 11);
                                break;

                            case 3:
                                if (curChar == 32)
                                    jjAddStates(108, 109);
                                break;

                            case 4:
                                if (curChar == 40 && kind > 12)
                                    kind = 12;
                                break;

                            case 13:
                                if ((0x3ff200000000000L & l) == 0L)
                                    break;
                                if (kind > 62)
                                    kind = 62;
                                jjstateSet[jjnewStateCnt++] = 13;
                                break;

                            case 14:
                                if (curChar == 46)
                                    jjstateSet[jjnewStateCnt++] = 15;
                                break;

                            case 18:
                                if (curChar == 36 && kind > 13)
                                    kind = 13;
                                break;

                            case 20:
                                if (curChar == 36)
                                    jjCheckNAddTwoStates(21, 22);
                                break;

                            case 22:
                                if (curChar == 33 && kind > 14)
                                    kind = 14;
                                break;

                            case 23:
                                if (curChar != 36)
                                    break;
                                if (kind > 13)
                                    kind = 13;
                                jjCheckNAddTwoStates(21, 22);
                                break;

                            case 24:
                                if (curChar == 35)
                                    jjCheckNAddStates(126, 128);
                                break;

                            case 25:
                                if (curChar == 42)
                                    jjstateSet[jjnewStateCnt++] = 26;
                                break;

                            case 26:
                                if ((0xfffffff7ffffffffUL & (ulong)l) != 0L && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 12:
                                if ((0x7fffffe87fffffeL & l) != 0L)
                                {
                                    if (kind > 62)
                                        kind = 62;
                                    jjCheckNAdd(13);
                                }
                                else if (curChar == 92)
                                    jjCheckNAddStates(129, 132);
                                break;

                            case 27:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 10;
                                else if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 2:
                                if (curChar == 116)
                                    jjCheckNAddTwoStates(3, 4);
                                break;

                            case 5:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 2;
                                break;

                            case 6:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 5;
                                break;

                            case 7:
                                if (curChar == 125)
                                    jjCheckNAddTwoStates(3, 4);
                                break;

                            case 8:
                                if (curChar == 116)
                                    jjstateSet[jjnewStateCnt++] = 7;
                                break;

                            case 9:
                                if (curChar == 101)
                                    jjstateSet[jjnewStateCnt++] = 8;
                                break;

                            case 10:
                                if (curChar == 115)
                                    jjstateSet[jjnewStateCnt++] = 9;
                                break;

                            case 11:
                                if (curChar == 123)
                                    jjstateSet[jjnewStateCnt++] = 10;
                                break;

                            case 13:
                                if ((0x7fffffe87fffffeL & l) == 0L)
                                    break;
                                if (kind > 62)
                                    kind = 62;
                                jjCheckNAdd(13);
                                break;

                            case 15:
                                if ((0x7fffffe07fffffeL & l) != 0L && kind > 63)
                                    kind = 63;
                                break;

                            case 16:
                                if (curChar == 92)
                                    jjCheckNAddStates(129, 132);
                                break;

                            case 17:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(17, 18);
                                break;

                            case 19:
                                if (curChar == 92)
                                    jjCheckNAddTwoStates(19, 20);
                                break;

                            case 21:
                                if (curChar == 92)
                                    jjAddStates(133, 134);
                                break;

                            case 26:
                                if (kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                else
                {
                    int hiByte = (int)(curChar >> 8);
                    int i1 = hiByte >> 6;
                    long l1 = 1L << (hiByte & 63);
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);

                MatchLoop1:
                    do
                    {
                        switch (jjstateSet[--i])
                        {

                            case 26:
                                if (jjCanMove_0(hiByte, i1, i2, l1, l2) && kind > 15)
                                    kind = 15;
                                break;

                            default: break;

                        }
                    }
                    while (i != startsAt);
                }
                if (kind != 0x7fffffff)
                {
                    jjmatchedKind = kind;
                    jjmatchedPos = curPos;
                    kind = 0x7fffffff;
                }
                ++curPos;
                if ((i = jjnewStateCnt) == (startsAt = 28 - (jjnewStateCnt = startsAt)))
                    return curPos;
                try
                {
                    curChar = input_stream.ReadChar();
                }
                catch (System.IO.IOException e)
                {
                    return curPos;
                }
            }
        }
        //UPGRADE_NOTE: Final �Ѵӡ�jjnextStates�����������Ƴ��� "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly int[] jjnextStates = new int[] { 87, 89, 90, 91, 96, 97, 87, 90, 57, 96, 27, 28, 31, 11, 12, 13, 1, 2, 4, 11, 16, 12, 13, 24, 25, 29, 30, 66, 67, 69, 70, 71, 72, 83, 85, 80, 81, 77, 78, 14, 15, 17, 19, 24, 25, 60, 61, 73, 74, 94, 95, 98, 99, 5, 6, 7, 8, 9, 10, 78, 80, 81, 82, 87, 88, 78, 81, 10, 87, 19, 20, 31, 32, 34, 42, 43, 45, 50, 32, 51, 66, 43, 67, 54, 57, 64, 71, 76, 22, 23, 24, 25, 35, 40, 47, 13, 14, 26, 27, 85, 86, 89, 90, 6, 11, 33, 16, 18, 3, 4, 20, 21, 23, 24, 25, 26, 14, 15, 27, 28, 8, 9, 10, 11, 12, 13, 6, 11, 27, 17, 18, 19, 20, 21, 22, 50, 52, 53, 54, 64, 65, 50, 53, 59, 64, 6, 11, 48, 30, 31, 34, 14, 15, 16, 14, 19, 15, 16, 32, 33, 38, 39, 40, 41, 17, 18, 20, 22, 27, 28, 42, 43, 57, 58, 62, 63 };
        private static bool jjCanMove_0(int hiByte, int i1, int i2, long l1, long l2)
        {
            switch (hiByte)
            {

                case 0:
                    return ((jjbitVec2[i2] & (ulong)l2) != 0L);

                default:
                    if ((jjbitVec0[i1] & (ulong)l1) != 0L)
                        return true;
                    return false;

            }
        }
        public static readonly string[] jjstrLiteralImages = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
        public static readonly string[] lexStateNames = { "DIRECTIVE", "REFMOD2", "REFMODIFIER", "DEFAULT", "REFERENCE", "PRE_DIRECTIVE", "IN_MULTI_LINE_COMMENT", "IN_FORMAL_COMMENT", "IN_SINGLE_LINE_COMMENT" };
        public static readonly int[] jjnewLexState = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        internal static readonly ulong[] jjtoToken = { 0xc637fffffdfc1fffL, 0x3L };
        internal static readonly long[] jjtoSkip = { 0x2000000L, 0xcL };
        internal static readonly long[] jjtoSpecial = { 0x0L, 0xcL };
        internal static readonly long[] jjtoMore = { 0x3e000L, 0x0L };
        protected internal ICharStream input_stream;
        private uint[] jjrounds = new uint[101];
        private uint[] jjstateSet = new uint[202];
        internal System.Text.StringBuilder image;
        internal int jjimageLen;
        internal int lengthOfMatch;
        protected internal char curChar;
        public ParserTokenManager(ICharStream stream)
        {
            InitBlock();
            input_stream = stream;
        }
        public ParserTokenManager(ICharStream stream, int lexState)
            : this(stream)
        {
            SwitchTo(lexState);
        }
        public virtual void ReInit(ICharStream stream)
        {
            jjmatchedPos = jjnewStateCnt = 0;
            curLexState = defaultLexState;
            input_stream = stream;
            ReInitRounds();
        }
        private void ReInitRounds()
        {
            int i;
            jjround = 0x80000001;
            for (i = 101; i-- > 0; )
                jjrounds[i] = 0x80000000;
        }
        public virtual void ReInit(ICharStream stream, int lexState)
        {
            ReInit(stream);
            SwitchTo(lexState);
        }
        public virtual void SwitchTo(int lexState)
        {
            if (lexState >= 9 || lexState < 0)
                throw new TokenMgrError("Error: Ignoring invalid lexical state : " + lexState + ". State unchanged.", TokenMgrError.INVALID_LEXICAL_STATE);
            else
                curLexState = lexState;
        }

        protected internal virtual Token jjFillToken()
        {
            Token t = Token.NewToken(jjmatchedKind);
            t.Kind = jjmatchedKind;
            System.String im = jjstrLiteralImages[jjmatchedKind];
            t.Image = (im == null) ? input_stream.GetImage() : im;
            t.BeginLine = input_stream.BeginLine;
            t.BeginColumn = input_stream.BeginColumn;
            t.EndLine = input_stream.EndLine;
            t.EndColumn = input_stream.EndColumn;
            return t;
        }

        internal int curLexState = 3;
        internal int defaultLexState = 3;
        internal int jjnewStateCnt;
        internal uint jjround;
        internal int jjmatchedPos;
        internal int jjmatchedKind;

        internal virtual void SkipLexicalActions(Token matchedToken)
        {
            switch (jjmatchedKind)
            {

                case 66:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    /*
                    * push every terminator character back into the stream
                    */

                    input_stream.Backup(1);

                    inReference = false;

                    if (debugPrint)
                        System.Console.Out.Write("REF_TERM :");

                    stateStackPop();
                    break;

                case 67:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    if (debugPrint)
                        System.Console.Out.Write("DIRECTIVE_TERM :");

                    input_stream.Backup(1);
                    inDirective = false;
                    stateStackPop();
                    break;

                default:
                    break;

            }
        }
        internal virtual void MoreLexicalActions()
        {
            jjimageLen += (lengthOfMatch = jjmatchedPos + 1);
            switch (jjmatchedKind)
            {

                case 13:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen)));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen));
                    jjimageLen = 0;
                    if (!inComment)
                    {
                        /*
                        * if we find ourselves in REFERENCE, we need to pop down
                        * to end the previous ref
                        */

                        if (curLexState == ParserConstants.REFERENCE)
                        {
                            inReference = false;
                            stateStackPop();
                        }

                        inReference = true;

                        if (debugPrint)
                            System.Console.Out.Write("$  : going to " + ParserConstants.REFERENCE);

                        stateStackPush();
                        SwitchTo(ParserConstants.REFERENCE);
                    }
                    break;

                case 14:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen)));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen));
                    jjimageLen = 0;
                    if (!inComment)
                    {
                        /*
                        * if we find ourselves in REFERENCE, we need to pop down
                        * to end the previous ref
                        */

                        if (curLexState == ParserConstants.REFERENCE)
                        {
                            inReference = false;
                            stateStackPop();
                        }

                        inReference = true;

                        if (debugPrint)
                            System.Console.Out.Write("$!  : going to " + ParserConstants.REFERENCE);

                        stateStackPush();
                        SwitchTo(ParserConstants.REFERENCE);
                    }
                    break;

                case 15:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen)));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen));
                    jjimageLen = 0;
                    if (!inComment)
                    {
                        input_stream.Backup(1);
                        inComment = true;
                        stateStackPush();
                        SwitchTo(ParserConstants.IN_FORMAL_COMMENT);
                    }
                    break;

                case 16:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen)));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen));
                    jjimageLen = 0;
                    if (!inComment)
                    {
                        inComment = true;
                        stateStackPush();
                        SwitchTo(ParserConstants.IN_MULTI_LINE_COMMENT);
                    }
                    break;

                case 17:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen)));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen));
                    jjimageLen = 0;
                    if (!inComment)
                    {
                        /*
                        * We can have the situation where #if($foo)$foo#end.
                        * We need to transition out of REFERENCE before going to DIRECTIVE.
                        * I don't really like this, but I can't think of a legal way
                        * you are going into DIRECTIVE while in REFERENCE.  -gmj
                        */

                        if (curLexState == ParserConstants.REFERENCE || curLexState == ParserConstants.REFMODIFIER)
                        {
                            inReference = false;
                            stateStackPop();
                        }

                        inDirective = true;

                        if (debugPrint)
                            System.Console.Out.Write("# :  going to " + ParserConstants.DIRECTIVE);

                        stateStackPush();
                        SwitchTo(ParserConstants.PRE_DIRECTIVE);
                    }
                    break;

                default:
                    break;

            }
        }
        internal virtual void TokenLexicalActions(Token matchedToken)
        {
            switch (jjmatchedKind)
            {

                case 8:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    if (!inComment)
                        lparen++;

                    /*
                    * If in REFERENCE and we have seen the dot, then move
                    * to REFMOD2 -> Modifier()
                    */

                    if (curLexState == ParserConstants.REFMODIFIER)
                        SwitchTo(ParserConstants.REFMOD2);
                    break;

                case 9:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    RPARENHandler();
                    break;

                case 10:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    /*
                    * need to simply switch back to REFERENCE, not drop down the stack
                    * because we can (infinitely) chain, ala
                    * $foo.bar().blargh().woogie().doogie()
                    */

                    SwitchTo(ParserConstants.REFERENCE);
                    break;

                case 12:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    if (!inComment)
                    {
                        inDirective = true;

                        if (debugPrint)
                            System.Console.Out.Write("#set :  going to " + ParserConstants.DIRECTIVE);

                        stateStackPush();
                        inSet = true;
                        SwitchTo(ParserConstants.DIRECTIVE);
                    }

                    /*
                    *  need the LPAREN action
                    */

                    if (!inComment)
                    {
                        lparen++;

                        /*
                        * If in REFERENCE and we have seen the dot, then move
                        * to REFMOD2 -> Modifier()
                        */

                        if (curLexState == ParserConstants.REFMODIFIER)
                            SwitchTo(ParserConstants.REFMOD2);
                    }
                    break;

                case 18:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    if (!inComment)
                    {
                        if (curLexState == ParserConstants.REFERENCE)
                        {
                            inReference = false;
                            stateStackPop();
                        }

                        inComment = true;
                        stateStackPush();
                        SwitchTo(ParserConstants.IN_SINGLE_LINE_COMMENT);
                    }
                    break;

                case 22:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    inComment = false;
                    stateStackPop();
                    break;

                case 23:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    inComment = false;
                    stateStackPop();
                    break;

                case 24:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    inComment = false;
                    stateStackPop();
                    break;

                case 27:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    /*
                    *  - if we are in DIRECTIVE and haven't seen ( yet, then also drop out.
                    *      don't forget to account for the beloved yet wierd #set
                    *  - finally, if we are in REFMOD2 (remember : $foo.bar( ) then " is ok!
                    */

                    if (curLexState == ParserConstants.DIRECTIVE && !inSet && lparen == 0)
                        stateStackPop();
                    break;

                case 30:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    if (debugPrint)
                        System.Console.Out.WriteLine(" NEWLINE :");

                    stateStackPop();

                    if (inSet)
                        inSet = false;

                    if (inDirective)
                        inDirective = false;
                    break;

                case 46:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    inDirective = false;
                    stateStackPop();
                    break;

                case 47:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    SwitchTo(ParserConstants.DIRECTIVE);
                    break;

                case 48:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    SwitchTo(ParserConstants.DIRECTIVE);
                    break;

                case 49:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    inDirective = false;
                    stateStackPop();
                    break;

                case 50:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    inDirective = false;
                    stateStackPop();
                    break;

                case 52:
                    if (image == null)
                        image = new StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    /*
                    * Remove the double period if it is there
                    */
                    if (matchedToken.Image.EndsWith(".."))
                    {
                        input_stream.Backup(2);
                        matchedToken.Image = matchedToken.Image.Substring(0, (matchedToken.Image.Length - 2) - (0));
                    }

                    /*
                    * check to see if we are in set
                    *    ex.  #set $foo = $foo + 3
                    *  because we want to handle the \n after
                    */

                    if (lparen == 0 && !inSet && curLexState != ParserConstants.REFMOD2)
                    {
                        stateStackPop();
                    }
                    break;

                case 53:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    /*
                    * check to see if we are in set
                    *    ex.  #set $foo = $foo + 3
                    *  because we want to handle the \n after
                    */

                    if (lparen == 0 && !inSet && curLexState != ParserConstants.REFMOD2)
                    {
                        stateStackPop();
                    }
                    break;

                case 63:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    /*
                    * push the alpha char back into the stream so the following identifier
                    * is complete
                    */

                    input_stream.Backup(1);

                    /*
                    * and munge the <DOT> so we just get a . when we have normal text that
                    * looks like a ref.ident
                    */

                    matchedToken.Image = ".";

                    if (debugPrint)
                        System.Console.Out.Write("DOT : switching to " + ParserConstants.REFMODIFIER);
                    SwitchTo(ParserConstants.REFMODIFIER);
                    break;

                case 65:
                    if (image == null)
                        image = new System.Text.StringBuilder(new System.String(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1))));
                    else
                        image.Append(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));
                    stateStackPop();
                    break;

                default:
                    break;

            }
        }
    }
}