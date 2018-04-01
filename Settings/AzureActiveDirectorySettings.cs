/*
Copyright(c) <2018> <University of Washington>
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System.Collections.Specialized;
using System.Configuration;
using Dqdv.Internal.Contracts.Settings;

namespace Dqdv.Internal.Settings
{
    /// <summary>
    /// Azure AD configuration section.
    /// </summary>
    public class AzureActiveDirectorySettings : IAzureActiveDirectorySettings
    {
        ////////////////////////////////////////////////////////////
        // Constants, Enums and Class members
        ////////////////////////////////////////////////////////////

        private const string SectionName = "azure-active-directory";
        private const string ClientIdKeyName = "ClientId";
        private const string ClientSecretKeyName = "ClientSecret";
        private readonly NameValueCollection _section;

        ////////////////////////////////////////////////////////////
        // Constructors
        ////////////////////////////////////////////////////////////

        /// <summary>
        /// Initialize a new instance of <see cref="AzureActiveDirectorySettings"/>
        /// </summary>
        public AzureActiveDirectorySettings()
        {
            _section = (NameValueCollection)ConfigurationManager.GetSection(SectionName);
        }

        ////////////////////////////////////////////////////////////
        // Public Methods/Atributes
        ////////////////////////////////////////////////////////////

        /// <inheritdoc />
        /// <summary>
        /// Gets AD client identifier
        /// </summary>
        public string ClientId => _section[ClientIdKeyName];

        /// <inheritdoc />
        /// <summary>
        /// Gets AD client's secret
        /// </summary>
        public string ClientSecret => _section[ClientSecretKeyName];
    }
}
