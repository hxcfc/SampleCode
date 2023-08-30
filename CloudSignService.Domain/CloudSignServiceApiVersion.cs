namespace CloudSignService.Domain
{
    public static class CloudSignServiceApiVersion
    {
        public static string Name = "CloudSignService";
        public static string Version = $"{ReleaseVersion}.{MediqusVersion}.{MainVersion}.{MinorVersion}";
        private static readonly string MainVersion = "1";
        private static readonly string MediqusVersion = "1";
        private static readonly string MinorVersion = "1";
        private static readonly string ReleaseVersion = "1";
    }
}