namespace Validot.Errors.Args
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public sealed class GuidArg : IArg<Guid>
    {
        private const string FormatParameter = "format";

        private const string DefaultFormat = "D";

        private const string CaseParameter = "case";

        private const string UpperCaseParameterValue = "upper";

        private const string LowerCaseParameterValue = "lower";

        public GuidArg(string name, Guid value)
        {
            ThrowHelper.NullArgument(name, nameof(name));

            Name = name;
            Value = value;
        }

        public string Name { get; }

        public Guid Value { get; }

        public IReadOnlyCollection<string> AllowedParameters { get; } = new[]
        {
            FormatParameter,
            CaseParameter
        };

        public string ToString(IReadOnlyDictionary<string, string> parameters)
        {
            var caseParameter = parameters?.ContainsKey(CaseParameter) == true
                ? parameters[CaseParameter]
                : null;

            if (caseParameter != null &&
                caseParameter != UpperCaseParameterValue &&
                caseParameter != LowerCaseParameterValue)
            {
                caseParameter = null;
            }

            var format = parameters?.ContainsKey(FormatParameter) == true
                ? parameters[FormatParameter]
                : null;

            if (format == null)
            {
                format = DefaultFormat;
            }

            var stringifiedGuid = Value.ToString(format, CultureInfo.InvariantCulture);

            if (caseParameter == UpperCaseParameterValue)
            {
                return stringifiedGuid.ToUpper(CultureInfo.InvariantCulture);
            }

            if (caseParameter == LowerCaseParameterValue)
            {
                return stringifiedGuid.ToLower(CultureInfo.InvariantCulture);
            }

            return stringifiedGuid;
        }
    }
}
