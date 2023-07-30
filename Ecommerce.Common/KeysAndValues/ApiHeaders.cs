namespace Ecommerce.Common.KeysAndValues
{
    public static class ApiHeaders
    {
        #region Private Fields

        private static List<string> _publicHeaders;

        private static List<string> _publicHeadersWithSignature;

        private static List<string> _privateHeaders;

        private static List<string> _privateHeadersWithSignature;

        #endregion Private Fields

        #region Properties

        public static List<string> PublicHeaders
        {
            get
            {
                _publicHeaders = new List<string>
                {
                    TOKEN,
                    PORTAL
                };

                return _publicHeaders;
            }
        }

        public static List<string> PpublicHeadersWithSignature
        {
            get
            {
                _publicHeadersWithSignature = new List<string>
                {
                    TOKEN,
                    PORTAL,
                    NONCE,
                    CONTENT_HASH,
                    SIGNATURE
                };

                return _publicHeadersWithSignature;
            }
        }

        public static List<string> PrivateHeaders
        {
            get
            {
                _privateHeaders = new List<string>
                {
                    TOKEN,
                    PORTAL,
                    AUTHORIZATION
                };

                return _privateHeaders;
            }
        }

        public static List<string> PrivateHeadersWithSignature
        {
            get
            {
                _privateHeadersWithSignature = new List<string>
                {
                    TOKEN,
                    PORTAL,
                    NONCE,
                    CONTENT_HASH,
                    SIGNATURE,
                    AUTHORIZATION
                };

                return _privateHeadersWithSignature;
            }
        }

        #endregion Properties

        #region Fields

        /// <summary>
        /// Client Key for the portal
        /// </summary>
        public static readonly string PORTAL = "Api-X-Portal";

        /// <summary>
        /// Api access key
        /// </summary>
        public static readonly string TOKEN = "Api-X-Token";

        /// <summary>
        /// Api request content (body) hash (sha256)
        /// </summary>
        public static readonly string CONTENT_HASH = "Api-X-Content-Hash";

        /// <summary>
        /// Api request signature (hmacsha256)
        /// </summary>
        public static readonly string SIGNATURE = "Api-X-Signature";

        /// <summary>
        /// Api request nonce
        /// </summary>
        public static readonly string NONCE = "Api-X-Nonce ";

        /// <summary>
        /// Login user JWT token
        /// </summary>
        public static readonly string AUTHORIZATION = "Authorization";

        /// <summary>
        /// Product Price conversion header
        /// </summary>
        public static readonly string CONVERTIBLE = "Api-x-Convertible";

        /// <summary>
        /// User Agent
        /// </summary>
        public static readonly string USER_AGENT = "User-Agent";

        #endregion Fields
    }
}