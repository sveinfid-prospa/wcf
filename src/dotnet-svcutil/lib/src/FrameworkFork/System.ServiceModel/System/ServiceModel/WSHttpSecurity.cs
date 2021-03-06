// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.ServiceModel
{
    using System.Runtime;
    using System.ServiceModel.Channels;
    using System.ComponentModel;

    public sealed class WSHttpSecurity
    {
        internal const SecurityMode DefaultMode = SecurityMode.Message;

        private SecurityMode _mode;
        private HttpTransportSecurity _transportSecurity;
        private NonDualMessageSecurityOverHttp _messageSecurity;

        public WSHttpSecurity()
            : this(DefaultMode, GetDefaultHttpTransportSecurity(), new NonDualMessageSecurityOverHttp())
        {
        }

        internal WSHttpSecurity(SecurityMode mode, HttpTransportSecurity transportSecurity, NonDualMessageSecurityOverHttp messageSecurity)
        {
            _mode = mode;
            _transportSecurity = transportSecurity == null ? GetDefaultHttpTransportSecurity() : transportSecurity;
            _messageSecurity = messageSecurity == null ? new NonDualMessageSecurityOverHttp() : messageSecurity;
        }

        internal static HttpTransportSecurity GetDefaultHttpTransportSecurity()
        {
            HttpTransportSecurity transportSecurity = new HttpTransportSecurity();
            transportSecurity.ClientCredentialType = HttpClientCredentialType.Windows;
            return transportSecurity;
        }

        public SecurityMode Mode
        {
            get { return _mode; }
            set
            {
                if (!SecurityModeHelper.IsDefined(value))
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
                }
                _mode = value;
            }
        }

        public HttpTransportSecurity Transport
        {
            get { return _transportSecurity; }
            set
            {
                if (value == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
                }
                _transportSecurity = value;
            }
        }

        public NonDualMessageSecurityOverHttp Message
        {
            get { return _messageSecurity; }
            set
            {
                if (value == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
                }
                _messageSecurity = value;
            }
        }

        internal void ApplyTransportSecurity(HttpsTransportBindingElement https)
        {
            if (_mode == SecurityMode.TransportWithMessageCredential)
            {
                _transportSecurity.ConfigureTransportProtectionOnly(https);
            }
            else
            {
                _transportSecurity.ConfigureTransportProtectionAndAuthentication(https);
            }
        }

        internal static void ApplyTransportSecurity(HttpsTransportBindingElement transport, HttpTransportSecurity transportSecurity)
        {
            HttpTransportSecurity.ConfigureTransportProtectionAndAuthentication(transport, transportSecurity);
        }

        internal SecurityBindingElement CreateMessageSecurity(bool isReliableSessionEnabled, MessageSecurityVersion version)
        {
            if (_mode == SecurityMode.Message || _mode == SecurityMode.TransportWithMessageCredential)
            {
                return _messageSecurity.CreateSecurityBindingElement(this.Mode == SecurityMode.TransportWithMessageCredential, isReliableSessionEnabled, version);
            }
            else
            {
                return null;
            }
        }

        internal static bool TryCreate(SecurityBindingElement sbe, UnifiedSecurityMode mode, HttpTransportSecurity transportSecurity, bool isReliableSessionEnabled, out WSHttpSecurity security)
        {
            security = null;
            NonDualMessageSecurityOverHttp messageSecurity = null;
            SecurityMode securityMode = SecurityMode.None;
            if (sbe != null)
            {
                mode &= UnifiedSecurityMode.Message | UnifiedSecurityMode.TransportWithMessageCredential;
                securityMode = SecurityModeHelper.ToSecurityMode(mode);
                Fx.Assert(SecurityModeHelper.IsDefined(securityMode), string.Format("Invalid SecurityMode value: {0}.", mode.ToString()));
                if (!MessageSecurityOverHttp.TryCreate(sbe, securityMode == SecurityMode.TransportWithMessageCredential, isReliableSessionEnabled, out messageSecurity))
                {
                    return false;
                }
            }
            else
            {
                mode &= ~(UnifiedSecurityMode.Message | UnifiedSecurityMode.TransportWithMessageCredential);
                securityMode = SecurityModeHelper.ToSecurityMode(mode);
            }
            Fx.Assert(SecurityModeHelper.IsDefined(securityMode), string.Format("Invalid SecurityMode value: {0}.", securityMode.ToString()));
            security = new WSHttpSecurity(securityMode, transportSecurity, messageSecurity);
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeMode()
        {
            return this.Mode != DefaultMode;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeMessage()
        {
            return this.Message.InternalShouldSerialize();
        }
    }
}
