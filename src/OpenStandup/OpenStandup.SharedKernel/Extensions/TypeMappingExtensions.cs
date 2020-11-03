using System.Net;


namespace OpenStandup.SharedKernel.Extensions
{
    public static class TypeMappingExtensions
    {
        public static ResultStatus ToResultStatus(this HttpStatusCode @this)
        {
            return @this switch
            {
                HttpStatusCode.OK => ResultStatus.Success,
                HttpStatusCode.Unauthorized => ResultStatus.Unauthorized,
                _ => ResultStatus.Failed
            };
        }
    }
}
