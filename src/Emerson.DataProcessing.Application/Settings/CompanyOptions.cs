namespace Emerson.DataProcessing.Application.Settings
{
    public class CompanyOptions
    {
        public static readonly string CompanySectionName = "Company";

        public string Foo1_Json {get; set;} = string.Empty;

        public string Foo2_Json {get; set;} = string.Empty;

        public string Summarize_Json { get; set;} = string.Empty;
    }
}