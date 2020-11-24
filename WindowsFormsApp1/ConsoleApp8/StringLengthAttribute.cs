using System;
using System.Collections.Generic;
using System.Text;

[AttributeUsage(AttributeTargets.Property)]
public class StringLengthAttribute : Attribute
{
    private int _maximumLength;
    public StringLengthAttribute(int maximumLength)
    {
        _maximumLength = maximumLength;
    }

    public int MaximumLength
    {
        get { return _maximumLength; }
    }
}
