﻿// Copyright © Honglin Aviation, Vincent Deng(邓守海)
// All rights reserved.
//
// Redistribution and use of this software in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//    * Neither the name of the author nor the names of the program's contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHORS OF THE SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VirtualRadar.Interface.Database;

namespace VirtualRadar.Database
{
    /// <summary>
    /// A helper class for database objects that implement <see cref="ITransactionable"/>.
    /// </summary>
    class TransactionHelper
    {
        /// <summary>
        /// Gets or sets the current transaction, or null if no transaction is in force.
        /// </summary>
        public IDbTransaction Transaction { get; set; }

        /// <summary>
        /// Gets or sets the number of times a transaction has been started but not finished.
        /// </summary>
        public int TransactionNestingLevel { get; set; }

        /// <summary>
        /// Starts a new transaction unless it's a nested call, in which case the nesting level is just incremented.
        /// </summary>
        public void StartTransaction(IDbConnection connection)
        {
            if(TransactionNestingLevel++ == 0) Transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Commits the transaction unless it's a nested commit, in which case the nesting level is decremented.
        /// </summary>
        public void EndTransaction()
        {
            if(TransactionNestingLevel > 0) {
                if(--TransactionNestingLevel == 0) {
                    try {
                        Transaction.Commit();
                        Transaction.Dispose();
                    } finally {
                        Transaction = null;
                    }
                }
            }
        }

        /// <summary>
        /// Rolls the transaction back. If its a nested rollback then it is still applied.
        /// </summary>
        public void RollbackTransaction()
        {
            if(TransactionNestingLevel > 0) {
                try {
                    TransactionNestingLevel = 0;
                    Transaction.Rollback();
                    Transaction.Dispose();
                } finally {
                    Transaction = null;
                }
            }
        }

        /// <summary>
        /// Abandons the transaction.
        /// </summary>
        public void Abandon()
        {
            if(Transaction != null) {
                try {
                    RollbackTransaction();
                } finally {
                    Transaction = null;
                }
            }
        }
    }
}
