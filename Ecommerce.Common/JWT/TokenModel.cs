namespace Ecommerce.Common.JWT
{
    public class TokenModel
    {
        #region Private Methods

        private static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return epoch.AddSeconds(unixTime);
        }

        #endregion Private Methods

        #region Constructors

        public TokenModel()
        {
        }

        public TokenModel(long id, AccountType type)
        {
            Id = id;
            Type = type;
        }

        public TokenModel(long id, long issuedAt, long expiresAt, long notValidBefore, AccountType type)
        {
            Id = id;
            IssuedAtEpoch = issuedAt;
            IssuedAt = FromUnixTime(IssuedAtEpoch);
            ExpiresAtEpoch = expiresAt;
            ExpiresAt = FromUnixTime(expiresAt);
            NotValidBeforeEpoch = notValidBefore;
            NotValidBefore = FromUnixTime(notValidBefore);
            Type = type;
        }

        #endregion Constructors

        #region Properties

        public AccountType Type { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime NotValidBefore { get; set; }
        public long ExpiresAtEpoch { get; set; }
        public long Id { get; set; }
        public long IssuedAtEpoch { get; set; }
        public long NotValidBeforeEpoch { get; set; }

        #endregion Properties
    }
}