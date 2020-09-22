using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Light.IdentityServer
{
    public class Config
    {
        // clients that are allowed to access resources from the Auth server 
        public static IEnumerable<Client> GetClients()
        {
            // client credentials, list of clients
            return new List<Client>
            {
                new Client
                {
                    ClientId = "WebAppClientId",
                    AllowedGrantTypes = { GrantType.ClientCredentials, GrantType.ResourceOwnerPassword },
                    ClientSecrets =
                    {
                        new Secret("WebAppClientSecret".Sha256())
                    },
                    AllowedScopes = { "AuthApi", "BlogApi" }
                },
                // OpenID Connect隐式流客户端（MVC）
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("mvc1".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Implicit,//隐式方式
                    RequireConsent = false,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://10.181.24.30:5003/signin-oidc" },//指定允许令牌或授权码返回的地址（URL）
                    PostLogoutRedirectUris = { "http://10.181.24.30:5003/signout-callback-oidc" },//指定允许注销后返回的地址(URL)
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        // API that are allowed to access the Auth server
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("AuthApi", "Auth API-权限接口"),
                new ApiResource("BlogApi", "Blog API-博客接口")
            };
        }

        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
