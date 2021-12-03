﻿using System;
using AppForeach.Framework.DataType;

namespace EscapeHit
{
    public class EscapeHitTypeSpecification : BasePrimitiveTypeSpecification
    {
        public EscapeHitTypeSpecification()
        {
            ByDefaultNotRequired();

            Type<string>().MaxLength(50);

            Type<decimal>().Digits(10, 2);

            Type<DateTime>().MinValue(1900, 1, 1);
        }
    }
}
