﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSagas.Operations
{
    public class AccountInvoceRegistrationInput
    {
        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }
    }
}
