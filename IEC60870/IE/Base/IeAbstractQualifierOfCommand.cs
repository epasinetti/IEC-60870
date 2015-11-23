﻿using System;
using System.IO;

namespace IEC60870.IE.Base
{
    public abstract class IeAbstractQualifierOfCommand : InformationElement
    {
        protected int value;

        IeAbstractQualifierOfCommand(int qualifier, Boolean select)
        {
            if (qualifier < 0 || qualifier > 31)
            {
                throw new ArgumentException("Qualifier is out of bound: " + qualifier);
            }

            value = qualifier << 2;

            if (select)
            {
                value |= 0x80;
            }
        }

        IeAbstractQualifierOfCommand(BinaryReader reader)
        {
            value = reader.ReadByte();
        }

        public override int encode(byte[] buffer, int i)
        {
            buffer[i] = (byte)value;
            return 1;
        }

        public Boolean isSelect()
        {
            return (value & 0x80) == 0x80;
        }

        public int getQualifier()
        {
            return (value >> 2) & 0x1f;
        }
        
        public override String ToString()
        {
            return "selected: " + isSelect() + ", qualifier: " + getQualifier();
        }
    }
}