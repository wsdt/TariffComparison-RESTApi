using System;

namespace Wavect.TariffComparison.GlobalConstants
{
    /// <summary>
    /// General api configuration. Mainly used to avoid pure strings (less type errors, ..)
    /// ... and honestly I just don't like strings in my code :-P
    /// 
    /// Nevertheless, I think this seemingly over-engineered class is actually very useful in the long run. 
    /// Just thing about many different devs adding a lot of new features for ..
    /// ... different customers
    /// ... in different branches/versions
    /// etc.
    /// 
    /// This might result in adding arbitrary api versions to routes & controllers and by using simply constants 
    /// we are able to sensibilize other devs to versioning their routes powerfully. 
    /// 
    /// All values in here must be constants (e.g. ApiVersion accepts no dynamic strings, ...), therefore
    /// using the IConfiguration interface doesn't work here. I found this to be a good trade-off.
    /// 
    /// QUICK NOTE: I'm implementing this kind of use-case in every project differently, as I'm never truly happy
    /// with this solution.
    /// </summary>
    public static class ApiConfiguration
    {
        #region constants_string
        /// <summary>
        /// Current/Latest api version
        /// </summary>
        public const string LATEST_API_VERSION = "1.0";

        #region active_api_versions
        // List here new api-versions
        public const string API_VERSION_v1_0 = LATEST_API_VERSION; // always change this to the newest version
        #endregion
        #endregion

        #region properties
        public static int LatestMajorApiVersion { get => getVersionPart(0); }
        public static int LatestMinorApiVersion { get => getVersionPart(1); }
        #endregion

        #region methods_private
        /// <summary>
        /// A little bit too much I know. 
        /// But just to ensure, that Devs cannot put an arbitrary string into the LATEST_API_VERSION constant 
        /// as it is simply used in too many places. 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private static int getVersionPart(short index)
        {
            string[] versionParts = LATEST_API_VERSION.Split('.');
            if (index >= versionParts.Length)
            {
                throw new ArgumentOutOfRangeException("Index out of Range. Api Version should only consist of major and minor version.");
            }

            bool parseSuccessful = int.TryParse(versionParts[index], out int versionPart);
            if (!parseSuccessful)
            {
                throw new ArgumentException($"Latest Api Version does not correspond to supported format: {LATEST_API_VERSION}");
            }

            return versionPart;
        }
        #endregion
    }
}
